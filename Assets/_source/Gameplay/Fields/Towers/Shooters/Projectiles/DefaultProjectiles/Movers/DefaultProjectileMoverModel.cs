using Gameplay.Fields.Enemies;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    class DefaultProjectileMoverModel : IProjectileMoverModel
    {
        public float Speed { get; set; } = 10f;
        public EnemyModel Target { get; set; }
        public Vector3 Position { get; private set; }
        public IProjectileModel ProjectileModel { get; }

        public DefaultProjectileMoverModel(EnemyModel target, IProjectileModel projectileModel)
        {
            Target = target;
            ProjectileModel = projectileModel;
        }

        public void Move(Vector3 position)
        {
            Position = position;
        }
    }
}