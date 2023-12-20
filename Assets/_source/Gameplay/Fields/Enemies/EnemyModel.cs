using System;
using System.Linq;
using Gameplay.Fields.Enemies.Movers;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; }
        public EnemyMoverModel MoverModel { get; set; }
        public Vector3 DamagePosition { get; set; }

        public event Action<EnemyModel> Died;

        public EnemyModel(Vector3 position, Vector2Int[] points, EnemyValues values)
        {
            MoverModel = new EnemyMoverModel(position, points.ToArray(), this);
            Health = new ReactiveProperty<float>(values.HealthPoints);
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