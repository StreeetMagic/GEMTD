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

        public EnemyModel(Vector3 position, Vector3[] points)
        {
            MoverModel = new EnemyMoverModel(position, points.ToArray());
        }

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;
        }
    }
}