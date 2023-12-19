using System;
using System.Linq;
using Gameplay.Fields.EnemySpawners.Enemies.Movers;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; } = new() { Value = 20 };
        public EnemyMoverModel MoverModel { get; set; }

        public event Action<EnemyModel> Died;

        public EnemyModel(Vector3 position, Vector2Int[] points)
        {
            MoverModel = new EnemyMoverModel(position, points.ToArray(), this);
        }

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;

            if (Health.Value <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Died?.Invoke(this);
        }
    }
}