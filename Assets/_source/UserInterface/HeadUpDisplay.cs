using System;
using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
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
    public class HeadUpDisplay : MonoBehaviour
    {
        [field: SerializeField] public Button[] TowerButtons { get; private set; }

        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();

        private void Awake()
        {
            _gameLoopStateMachine = ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

            OnChooseTowerStateEntered();
        }

        private void OnChooseTowerStateEntered()
        {
            int roundNumber = CurrentDataService.FieldModel.RoundNumber;
            WallSettingsPerRound[] towerIndexes = WallPlacerConfig.WallSettingsPerRounds.ToArray();
            List<Vector2Int> wallsCoordinates = towerIndexes[roundNumber - 1].PlaceList;

            for (int i = 0; i < TowerButtons.Length; i++)
            {
                CellModel cellModel = CurrentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(wallsCoordinates[i]);
                Button towerButton = TowerButtons[i];

                towerButton.GetComponentInChildren<TextMeshProUGUI>().text = cellModel.TowerModel.Type.ToString() + cellModel.TowerModel.Level;
                
                towerButton.onClick.AddListener( () =>
                {
                    _gameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cellModel);
                });
            }
        }
    }
}