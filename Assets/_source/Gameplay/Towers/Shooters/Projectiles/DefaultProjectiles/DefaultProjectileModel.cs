using Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using Gameplay.Towers.Shooters.Projectiles.Movers;
using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles
{
    class DefaultProjectileModel : IProjectileModel
    {
        public IProjectileMoverModel Mover { get; }

        public float Damage { get; set; } = 10f;

        public DefaultProjectileModel(Rigidbody rigidbody, Transform transform)
        {
            Mover = new DefaultProjectileMoverModel(rigidbody, transform);
        }
    }
}