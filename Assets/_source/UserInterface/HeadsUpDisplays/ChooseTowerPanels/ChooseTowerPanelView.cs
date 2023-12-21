using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.CurrentDataServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class ChooseTowerPanelView : MonoBehaviour
    {
        public List<Button> DowngradeButtons { get; private set; }
        public List<Button> PlacedTowerButtons { get; private set; }
        public List<Button> SingleMergeTowerButtons { get; private set; }
        public List<Button> DoubleMergeTowerButtons { get; private set; }

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        private void Awake()
        {
            DowngradeButtons = GetComponentInChildren<DowngradeButtonsView>().GetComponentsInChildren<Button>().ToList();
            PlacedTowerButtons = GetComponentInChildren<PlacedButtonsView>().GetComponentsInChildren<Button>().ToList();
            SingleMergeTowerButtons = GetComponentInChildren<SingleMergeButtonsView>().GetComponentsInChildren<Button>().ToList();
            DoubleMergeTowerButtons = GetComponentInChildren<DowngradeButtonsView>().GetComponentsInChildren<Button>().ToList();
        }

        public void OnChooseTowerStateEntered(List<Vector2Int> wallsCoordinates)
        {
            DisableMergeButtons();
            gameObject.SetActive(true);

            for (int i = 0; i < PlacedTowerButtons.Count; i++)
                InitButton(PlacedTowerButtons[i], CurrentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(wallsCoordinates[i]));

            CheckTowers(wallsCoordinates);
        }

        private void CheckTowers(List<Vector2Int> wallsCoordinates)
        {
            GetCellModels(wallsCoordinates, out CellModel[] cells, out Dictionary<TowerType, int> towerTypes);

            CheckDowngrades(wallsCoordinates, towerTypes);

            if (CheckSimilar(wallsCoordinates, towerTypes))
                return;

            CheckMerges(towerTypes, cells);
        }

        private void CheckMerges(Dictionary<TowerType, int> towerTypes, CellModel[] cells)
        {
            foreach (KeyValuePair<TowerType, int> pair in towerTypes)
            {
                if (pair.Value > 1)
                {
                    TowerType towerType = pair.Key;

                    for (int i = 0; i < cells.Length; i++)
                    {
                        CellModel cell = cells[i];

                        if (cell.TowerModel.Type != towerType)
                            continue;

                        TowerType singleMergeType = StaticDataService.Get<TowersConfig>().GetTowerValues(towerType).SingleMergeType;

                        if (singleMergeType == TowerType.None)
                            continue;

                        Button singleMergeTowerButton = SingleMergeTowerButtons[i];
                        singleMergeTowerButton.gameObject.SetActive(true);
                        singleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = singleMergeType.ToString();

                        singleMergeTowerButton.onClick.RemoveAllListeners();
                        singleMergeTowerButton.onClick.AddListener(() =>
                        {
                            int singleUpgradeLevel = cell.TowerModel.Level + 1;
                            cell.Upgrade(singleMergeType, singleUpgradeLevel);
                            GameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cell, () => GameLoopStateMachine.Enter<EnemyMoveState>());
                            gameObject.SetActive(false);
                        });
                    }
                }

                if (pair.Value <= 3)
                    continue;

                TowerType towerType2 = pair.Key;

                for (int i = 0; i < cells.Length; i++)
                {
                    CellModel cell = cells[i];

                    if (cell.TowerModel.Type != towerType2)
                        continue;

                    TowerType doubleMergeType = StaticDataService.Get<TowersConfig>().GetTowerValues(cell.TowerModel.Type).DoubleMergeType;

                    if (doubleMergeType == TowerType.None)
                        continue;

                    Button doubleMergeTowerButton = DoubleMergeTowerButtons[i];
                    doubleMergeTowerButton.gameObject.SetActive(true);
                    doubleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = doubleMergeType.ToString();

                    doubleMergeTowerButton.onClick.RemoveAllListeners();
                    doubleMergeTowerButton.onClick.AddListener(() =>
                    {
                        int doubleUpgradeLevel = cell.TowerModel.Level + 2;
                        cell.Upgrade(doubleMergeType, doubleUpgradeLevel);
                        GameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cell, () => GameLoopStateMachine.Enter<EnemyMoveState>());
                        gameObject.SetActive(false);
                    });
                }
            }
        }

        private void CheckDowngrades(List<Vector2Int> wallsCoordinates, Dictionary<TowerType, int> towerTypes)
        {
            throw new NotImplementedException();
        }

        private bool CheckSimilar(List<Vector2Int> wallsCoordinates, Dictionary<TowerType, int> towerTypes) =>
            towerTypes.Count == wallsCoordinates.Count;

        private void GetCellModels(List<Vector2Int> wallsCoordinates, out CellModel[] cells, out Dictionary<TowerType, int> towerTypes)
        {
            cells = new CellModel[wallsCoordinates.Count];
            towerTypes = new Dictionary<TowerType, int>();

            for (int i = 0; i < wallsCoordinates.Count; i++)
            {
                Vector2Int vector = wallsCoordinates[i];
                cells[i] = CurrentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(vector);

                TowerType towerType = cells[i].TowerModel.Type;

                if (towerTypes.ContainsKey(towerType))
                {
                    towerTypes[towerType]++;
                }
                else
                {
                    towerTypes.Add(towerType, 1);
                }
            }
        }

        private void InitButton(Button towerButton, CellModel cellModel)
        {
            towerButton.GetComponentInChildren<TextMeshProUGUI>().text = cellModel.TowerModel.Type.ToString();

            towerButton.onClick.RemoveAllListeners();
            towerButton.onClick.AddListener(() =>
            {
                GameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cellModel, () => GameLoopStateMachine.Enter<EnemyMoveState>());
                gameObject.SetActive(false);
            });
        }

        private void DisableMergeButtons()
        {
            for (int i = 0; i < SingleMergeTowerButtons.Count; i++)
            {
                DowngradeButtons[i].gameObject.SetActive(false);
                SingleMergeTowerButtons[i].gameObject.SetActive(false);
                DoubleMergeTowerButtons[i].gameObject.SetActive(false);
            }
        }
    }
}