using Infrastructure;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachineServices;
using Infrastructure.Services.StateMachineServices.States;
using TMPro;
using UnityEngine;

namespace Debugs.Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameLoopStateMachineActiveState;

        private PlaceWallsState _placeWallsState;

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

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
    }
}