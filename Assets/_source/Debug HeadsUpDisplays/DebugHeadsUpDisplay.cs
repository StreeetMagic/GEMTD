using System;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using UnityEngine;
using UnityEngine.UI;

namespace Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private Button _finishPlacingWalls;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;
        private PlaceWallsState _placeWallsState;

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

        private void Awake()
        {
            _placeWallsState = GameLoopStateMachine.Get<PlaceWallsState>();
            _finishPlacingWalls.interactable = false;

            _finishPlacingWalls.onClick.AddListener(() =>
            {
                Debug.Log("Я НАЖАЛ НА КНОПКУ");
                _placeWallsState.PlaceWalls();
                _finishPlacingWalls.interactable = false;
            });
        }

        private void OnEnable()
        {
            _placeWallsState.Entered += OnPlaceWallsStateEntered;
        }

        private void OnDisable()
        {
            _placeWallsState.Entered -= OnPlaceWallsStateEntered;
        }

        private void OnPlaceWallsStateEntered(IExitableState obj)
        {
            Debug.Log("Я СЛУШАЮ ИНВОКИ");
            _finishPlacingWalls.interactable = true;
        }
    }
}