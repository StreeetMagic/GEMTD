using System.Collections.Generic;
using Gameplay.Fields.Enemies;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;

namespace Gameplay.Fields.EnemyContainers
{
  public class EnemyContainerModel
  {
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;

    public EnemyContainerModel(IStateMachine<IGameLoopState> gameLoopStateMachine)
    {
      _gameLoopStateMachine = gameLoopStateMachine;
    }

    private List<EnemyModel> Enemies { get; } = new();

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
        _gameLoopStateMachine.Enter<PlaceWallsState>();
      }
    }
  }
}