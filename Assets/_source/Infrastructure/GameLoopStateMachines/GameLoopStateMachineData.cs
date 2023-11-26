using InfastuctureCore.Services.StateMachineServices;

namespace Infrastructure.GameLoopStateMachines
{
    public class GameLoopStateMachineData
    {
        public void RegisterStates(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            gameLoopStateMachine.Register(new ChooseTowerState());
            gameLoopStateMachine.Register(new EnemyMoveState());
            gameLoopStateMachine.Register(new LoseState());
            gameLoopStateMachine.Register(new PauseState());
            gameLoopStateMachine.Register(new PlaceWallsState());
            gameLoopStateMachine.Register(new WinState());
        }
    }
}