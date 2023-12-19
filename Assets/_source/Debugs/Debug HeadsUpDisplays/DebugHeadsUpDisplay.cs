using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Debugs.Debug_HeadsUpDisplays
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
            if (GameLoopStateMachine == null)
                return;

            IExitableState state = GameLoopStateMachine.ActiveState;

            _gameLoopStateMachineActiveState.text = state switch
            {
                ChooseTowerState => "ChooseTowerState",
                EnemyMoveState => "EnemyMoveState",
                LoseState => "LoseState",
                PlaceWallsState => "PlaceWallsState",
                WinState => "WinState",
                _ => _gameLoopStateMachineActiveState.text
            };
        }

        private void InitFinishWallPlaceButton()
        {
            _placeWallsState = GameLoopStateMachine.Get<PlaceWallsState>();
            _finishPlacingWalls.interactable = false;

            _finishPlacingWalls.onClick.AddListener(() =>
            {
                _finishPlacingWalls.interactable = false;
            });
        }

        private void OnPlaceWallsStateEntered(IExitableState obj)
        {
            _finishPlacingWalls.interactable = true;
        }
    }
}