using System.Collections.Generic;
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
    public class ChooseTowerPanel : MonoBehaviour
    {
        [field: SerializeField] public Button[] TowerButtons { get; private set; }
        [field: SerializeField] public Button[] SingleMergeTowerButtons { get; private set; }
        [field: SerializeField] public Button[] DoubleMergeTowerButtons { get; private set; }

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void OnChooseTowerStateEntered(List<Vector2Int> wallsCoordinates)
        {
            DisableMergeButtons();
            gameObject.SetActive(true);

            for (int i = 0; i < TowerButtons.Length; i++)
                InitButton(TowerButtons[i], CurrentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(wallsCoordinates[i]));

            FindMerges(wallsCoordinates);
        }

        private void FindMerges(List<Vector2Int> wallsCoordinates)
        {
            CellModel[] cells = new CellModel[wallsCoordinates.Count];
            Dictionary<TowerType, int> towerTypes = new Dictionary<TowerType, int>();

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

            if (towerTypes.Count == wallsCoordinates.Count)
            {
                return;
            }

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
            for (int i = 0; i < SingleMergeTowerButtons.Length; i++)
            {
                SingleMergeTowerButtons[i].gameObject.SetActive(false);
                DoubleMergeTowerButtons[i].gameObject.SetActive(false);
            }
        }
    }
}