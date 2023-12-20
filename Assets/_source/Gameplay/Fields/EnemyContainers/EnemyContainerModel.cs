using System.Collections.Generic;
using Gameplay.Fields.Enemies;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;

namespace Gameplay.Fields.EnemyContainers
{
    public class EnemyContainerModel
    {
        private List<EnemyModel> Enemies { get; } = new List<EnemyModel>();
        
        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();

        public void AddEnemy(EnemyModel enemy)
        {
            Enemies.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied(EnemyModel enemy)
        {
            Enemies.Remove(enemy);
            enemy.Died -= OnEnemyDied;

            if (Enemies.Count == 0)
            {
                GameLoopStateMachine.Enter<PlaceWallsState>();
            }
        }
    }
}