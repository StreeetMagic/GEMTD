using Gameplay.Towers.Shooters.Projectiles;
using UnityEngine;

namespace Gameplay.Enemies.TriggerAreas
{
    public class TriggerAreaModel
    {
        public TriggerAreaModel(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }

        public EnemyModel EnemyModel { get; set; }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IProjectileView projectile))
            {
                EnemyModel.TakeDamage(projectile.ProjectileModel.Damage);
            }
        }
    }
}