using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; } = new ReactiveProperty<float>();

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;
            Debug.Log("Получил урон: " + damage);
            Debug.Log("Осталось здоровья: " + Health.Value);
        }
    }
}