using System;
using System.Linq;
using Gameplay.Fields.Enemies.Movers;
using Infrastructure.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Enemies
{
  public class EnemyModel
  {
    public EnemyModel(Vector3 position, Vector2Int[] points, EnemyValues values, EnemiesConfig config)
    {
      MoverModel = new EnemyMoverModel(position, points.ToArray(), this, values.MovementSpeed * config.MoverSpeedMultiplier * config.DotaMoveSpeedMultiplier);
      Health = new ReactiveProperty<float>(values.HealthPoints * config.HealthPointsMultiplier);
    }

    public ReactiveProperty<float> Health { get; }
    public EnemyMoverModel MoverModel { get; }
    public Vector3 DamagePosition { get; set; }

    public event Action<EnemyModel> Died;

    public void TakeDamage(float damage)
    {
      if (damage <= 0)
        return;

      Health.Value -= damage;

      if (Health.Value <= 0)
        Die();
    }

    public void Die()
    {
      Died?.Invoke(this);
    }
  }
}