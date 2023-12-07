using Gameplay.Fields.EnemySpawners.Enemies.Movers;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; } = new ReactiveProperty<float>();
        public EnemyMoverModel MoverModel { get; set; }

        public EnemyModel(Vector3 position)
        {
            MoverModel = new EnemyMoverModel(position);
        }

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;
            Debug.Log("Получил урон: " + damage);
            Debug.Log("Осталось здоровья: " + Health.Value);
        }
    }
}