using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private Button _finishPlacingWalls;
        [SerializeField] private TextMeshProUGUI _gameLoopStateMachineActiveState;

        private PlaceWallsState _placeWallsState;
        // private CoroutineDecorator _coroutine;

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

        private void Awake()
        {
            InitFinishWallPlaceButton();
        }

        private void OnEnable()
        {
            _placeWallsState.Entered += OnPlaceWallsStateEntered;
        }

        private void OnDisable()
        {
            _placeWallsState.Entered -= OnPlaceWallsStateEntered;
        }

        private void Update()
        {
            if (GameLoopStateMachine != null)
            {
                var state = GameLoopStateMachine.ActiveState;

                switch (state)
                {
                    case ChooseTowerState:
                        _gameLoopStateMachineActiveState.text = "ChooseTowerState";
                        break;

                    case EnemyMoveState:
                        _gameLoopStateMachineActiveState.text = "EnemyMoveState";
                        break;

                    case LoseState:
                        _gameLoopStateMachineActiveState.text = "LoseState";
                        break;

                    case PlaceWallsState:
                        _gameLoopStateMachineActiveState.text = "PlaceWallsState";
                        break;

                    case WinState:
                        _gameLoopStateMachineActiveState.text = "WinState";
                        break;
                }
            }
        }

        private void InitFinishWallPlaceButton()
        {
            _placeWallsState = GameLoopStateMachine.Get<PlaceWallsState>();
            _finishPlacingWalls.interactable = false;

            _finishPlacingWalls.onClick.AddListener(() =>
            {
                _finishPlacingWalls.interactable = false;
                _placeWallsState.PlaceWalls();
            });
        }

        private void OnPlaceWallsStateEntered(IExitableState obj)
        {
            _finishPlacingWalls.interactable = true;
        }
    }
}