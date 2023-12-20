using System;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
    class DefaultProjectileModel : IProjectileModel
    {
        public event Action Died;

        public IProjectileMoverModel Mover { get; }
        public float Damage { get; set; } = 4f;

        public DefaultProjectileModel(EnemyModel target)
        {
            Mover = new DefaultProjectileMoverModel(target, this);
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}