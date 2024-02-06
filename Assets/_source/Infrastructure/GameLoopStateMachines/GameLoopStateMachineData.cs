using Gameplay.Fields.Walls.WallPlacers;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachineServices;

namespace Infrastructure.GameLoopStateMachines
{
    public class GameLoopStateMachineData
    {
        public void RegisterStates(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            TowerPlacer towerPlacer = new();

            gameLoopStateMachine.Register(new PlaceWallsState(gameLoopStateMachine, towerPlacer));
            gameLoopStateMachine.Register(new ChooseTowerState(towerPlacer));
            gameLoopStateMachine.Register(new EnemyMoveState());
            gameLoopStateMachine.Register(new WinState());
            gameLoopStateMachine.Register(new LoseState());
        }
    }
}