using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
    class DefaultProjectileModel : IProjectileModel
    {
        public IProjectileMoverModel Mover { get; }

        public float Damage { get; set; } = 10f;

        public DefaultProjectileModel(Transform target)
        {
            Mover = new DefaultProjectileMoverModel(target);
        }
    }
}