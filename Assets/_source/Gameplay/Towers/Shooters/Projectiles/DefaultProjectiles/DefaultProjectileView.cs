using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles
{
    class DefaultProjectileView : MonoBehaviour, IProjectileView
    {
        public IProjectileModel ProjectileModel { get; set; } = new DefaultProjectileModel();
    }
}