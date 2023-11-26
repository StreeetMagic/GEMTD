using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class GameLoopState : IState
    {
        //  private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public GameLoopState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            // _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered GameLoop State");
            GameFactoryService.BlockGridFactory.CreateBlockGridData();
            GameFactoryService.LabyrinthFactory.CreateStartingLabyrinth();
        }

        public void Exit()
        {
            Debug.Log("Exited GameLoop State");
        }
    }
}