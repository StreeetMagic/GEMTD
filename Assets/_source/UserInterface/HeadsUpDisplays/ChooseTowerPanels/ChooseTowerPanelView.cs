using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
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
            DoubleMergeTowerButtons = GetComponentInChildren<DoubleMergeButtonsView>().GetComponentsInChildren<Button>().ToList();
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
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (cells[i].TowerModel.Type != pair.Key)
                            continue;

                        if (StaticDataService.Get<TowersConfig>().GetTowerValues(pair.Key).SingleMergeType == TowerType.None)
                            continue;

                        Button singleMergeTowerButton = SingleMergeTowerButtons[i];
                        singleMergeTowerButton.gameObject.SetActive(true);
                        singleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = StaticDataService.Get<TowersConfig>().GetTowerValues(pair.Key).SingleMergeType.ToString();
                        singleMergeTowerButton.onClick.RemoveAllListeners();

                        singleMergeTowerButton.onClick.AddListener(() =>
                        {
                            cells[i].Upgrade(StaticDataService.Get<TowersConfig>().GetTowerValues(pair.Key).SingleMergeType, cells[i].TowerModel.Level + 1);
                            GameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cells[i], () => GameLoopStateMachine.Enter<EnemyMoveState>());
                            gameObject.SetActive(false);
                        });
                    }
                }

                if (pair.Value <= 3)
                    continue;

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].TowerModel.Type != pair.Key)
                        continue;

                    if (StaticDataService.Get<TowersConfig>().GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType == TowerType.None)
                        continue;

                    Button doubleMergeTowerButton = DoubleMergeTowerButtons[i];
                    doubleMergeTowerButton.gameObject.SetActive(true);
                    doubleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = StaticDataService.Get<TowersConfig>().GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType.ToString();
                    doubleMergeTowerButton.onClick.RemoveAllListeners();

                    doubleMergeTowerButton.onClick.AddListener(() =>
                    {
                        cells[i].Upgrade(StaticDataService.Get<TowersConfig>().GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType, cells[i].TowerModel.Level + 2);
                        GameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cells[i], () => GameLoopStateMachine.Enter<EnemyMoveState>());
                        gameObject.SetActive(false);
                    });
                }
            }
        }

        private void CheckDowngrades(List<Vector2Int> wallsCoordinates, Dictionary<TowerType, int> towerTypes)
        {
            //throw new NotImplementedException();
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