using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.States;
using TMPro;
using UnityEngine;
using Zenject;

namespace Debugs.Debug_HeadsUpDisplays
{
  public class DebugHeadsUpDisplay : MonoBehaviour
  {
    private IStateMachine<IGameLoopState> _gameLoopStateMachine;
    [SerializeField] private TextMeshProUGUI _gameLoopStateMachineActiveState;

    private PlaceWallsState _placeWallsState;

    [Inject]
    public void Construct(IStateMachine<IGameLoopState> stateMachine)
    {
      _gameLoopStateMachine = stateMachine;
    }

    private void Update()
    {
      if (_gameLoopStateMachine == null)
        return;

      IExitableState state = _gameLoopStateMachine.ActiveState;

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