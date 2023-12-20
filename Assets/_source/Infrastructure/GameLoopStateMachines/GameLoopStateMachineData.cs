using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameLoopStateMachines.States;

namespace Infrastructure.GameLoopStateMachines
{
    public class GameLoopStateMachineData
    {
        public void RegisterStates(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            TowerPlacer towerPlacer = new();

            gameLoopStateMachine.Register(new PlaceWallsState(gameLoopStateMachine, towerPlacer));
            gameLoopStateMachine.Register(new ChooseTowerState(towerPlacer, gameLoopStateMachine));
            gameLoopStateMachine.Register(new EnemyMoveState());
            gameLoopStateMachine.Register(new WinState());
            gameLoopStateMachine.Register(new LoseState());
        }
    }
}