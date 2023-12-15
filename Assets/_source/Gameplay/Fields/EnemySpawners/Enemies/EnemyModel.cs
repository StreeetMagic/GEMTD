using System;
using System.Linq;
using Gameplay.Fields.EnemySpawners.Enemies.Movers;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; } = new();
        public EnemyMoverModel MoverModel { get; set; }

        public event Action Died;

        public EnemyModel(Vector3 position, Vector2Int[] points)
        {
            MoverModel = new EnemyMoverModel(position, points.ToArray());
        }

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;

            if (Health.Value <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}