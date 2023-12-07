using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
    public class DefaultProjectileView : MonoBehaviour, IProjectileView
    {
        [field: SerializeField] public DefaultProjectileMoverView MoverView { get; set; }

        public IProjectileModel ProjectileModel { get; set; }

        public void Init(IProjectileModel projectileModel, Transform target)
        {
            ProjectileModel = projectileModel;
            MoverView.Init(ProjectileModel.Mover, target);
        }

        public void Destroy() =>
            Destroy(gameObject);
    }
}