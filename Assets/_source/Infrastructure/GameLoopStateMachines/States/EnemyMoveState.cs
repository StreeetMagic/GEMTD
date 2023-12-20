using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.CurrentDataServices;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class EnemyMoveState : IGameLoopStateMachineState
    {
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Enter()
        {
            CurrentDataService.FieldModel.EnemySpawnerModel.Spawn();
        }

        public void Exit()
        {
        }
    }
}