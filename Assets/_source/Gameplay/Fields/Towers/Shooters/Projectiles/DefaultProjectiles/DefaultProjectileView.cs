using Gameplay.Fields.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
    public class DefaultProjectileView : MonoBehaviour, IProjectileView
    {
        [field: SerializeField] public DefaultProjectileMoverView MoverView { get; set; }

        public IProjectileModel ProjectileModel { get; set; }

        public void Init(IProjectileModel projectileModel)
        {
            ProjectileModel = projectileModel;
            MoverView.Init(ProjectileModel.Mover);
            ProjectileModel.Mover.Target.Died += OnTargetDied;
        }

        private void OnDestroy()
        {
            ProjectileModel.Mover.Target.Died -= OnTargetDied;
        }

        private void OnTargetDied(EnemyModel enemyModel)
        {
            gameObject.SetActive(false);
            Destroy();
        }

        public void Destroy() =>
            Destroy(gameObject);
    }
}