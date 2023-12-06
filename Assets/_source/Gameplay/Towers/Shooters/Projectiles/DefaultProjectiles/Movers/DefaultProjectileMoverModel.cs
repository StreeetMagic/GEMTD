using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    class DefaultProjectileMoverModel : IProjectileMoverModel
    {
        public float Speed { get; set; } = 10f;
        public Transform Target { get; set; }
        public Vector3 Position { get; private set; }

        public DefaultProjectileMoverModel(Transform target)
        {
            Target = target;
        }

        public void Move(Vector3 position)
        {
            Position = position;
        }
    }
}