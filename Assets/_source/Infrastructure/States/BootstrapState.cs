using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.PoolServices;
using InfastuctureCore.Services.StateMachineServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        public BootstrapState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Bootstrap State");

            RegisterServices();
            EnterNextState();
        }

        public void Exit()
        {
            Debug.Log("Exited Bootstrap State");
        }

        private void RegisterServices()
        {
            var locator = ServiceLocator.Instance;

            locator.Register(_gameStateMachine);
            locator.Register<IAssetProviderService>(new AssetProviderService());
            locator.Register<IPoolRepositoryService>(new PoolRepositoryService());
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}