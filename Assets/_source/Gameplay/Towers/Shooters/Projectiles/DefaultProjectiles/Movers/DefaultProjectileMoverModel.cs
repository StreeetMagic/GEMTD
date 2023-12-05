using Gameplay.Towers.Shooters.Projectiles.Movers;
using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    class DefaultProjectileMoverModel : IProjectileMoverModel
    {
        public Transform Transform { get; set; }
        public Transform Target { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public float Speed { get; set; } = 10f;

        public DefaultProjectileMoverModel(Rigidbody rigidbody, Transform transform)
        {
            Rigidbody = rigidbody;
            Transform = transform;
        }

        public void Move()
        {
            if (Target == null)
            {
                return;
            }

            Rigidbody.MovePosition(Vector3.MoveTowards(Transform.position, Target.position, Speed * Time.fixedDeltaTime));
        }
    }
}