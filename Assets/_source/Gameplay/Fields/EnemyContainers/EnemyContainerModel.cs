using System.Collections.Generic;
using Gameplay.Fields.Enemies;
using Infrastructure;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachineServices;

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