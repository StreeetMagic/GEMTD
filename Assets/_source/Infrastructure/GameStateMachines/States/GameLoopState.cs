using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;
        
        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public GameLoopState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered GameLoop State");
            GameFactoryService.CreateBlockGrid();
        }

        public void Exit()
        {
            Debug.Log("Exited GameLoop State");
        }
    }
}