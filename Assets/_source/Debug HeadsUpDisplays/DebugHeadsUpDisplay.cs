using System.Collections;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using InfastuctureCore.Utilities;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using UnityEngine;
using UnityEngine.UI;

namespace Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private Button _finishPlacingWalls;

        [SerializeField] private Button _timeScaleButton;

        // [SerializeField] private Button _button3;
        private PlaceWallsState _placeWallsState;
        private CoroutineDecorator _coroutine;

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

        private void Awake()
        {
            _coroutine = new CoroutineDecorator(this, EnableButton);
            _placeWallsState = GameLoopStateMachine.Get<PlaceWallsState>();
            _finishPlacingWalls.interactable = false;

            _finishPlacingWalls.onClick.AddListener(() =>
            {
                Debug.Log("Я НАЖАЛ НА КНОПКУ");
                _finishPlacingWalls.interactable = false;
                _placeWallsState.PlaceWalls();
            });

            _timeScaleButton.onClick.AddListener(() => { Time.timeScale = 1; });
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
            _coroutine.Start();
        }

        private IEnumerator EnableButton()
        {
            yield return null;

            Debug.Log("Я СЛУШАЮ ИНВОКИ");
            _finishPlacingWalls.interactable = true;
        }
    }
}