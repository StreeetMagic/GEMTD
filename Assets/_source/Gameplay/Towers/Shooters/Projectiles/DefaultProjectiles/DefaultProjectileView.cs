using Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles
{
    public class DefaultProjectileView : MonoBehaviour, IProjectileView
    {
        [field: SerializeField] public DefaultProjectileMoverView MoverView { get; set; }

        public DefaultProjectileMoverView SEX => MoverView;

        public IProjectileModel ProjectileModel { get; set; }

        public void Init(IProjectileModel projectileModel, Transform target)
        {
            ProjectileModel = projectileModel;
            MoverView.Init(ProjectileModel.Mover, target);
        }
    }
}