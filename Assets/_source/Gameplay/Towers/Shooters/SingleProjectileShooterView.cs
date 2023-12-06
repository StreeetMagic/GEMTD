using Gameplay.Towers.Shooters.Projectiles.ProjectileContainers;
using Gameplay.Towers.Shooters.ShootingPoints;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public class SingleProjectileShooterView : MonoBehaviour
    {
        public ShootingPointView ShootingPoint { get; set; }
        public ProjectileContainerView ProjectileContainerView { get; set; }

        private IShooter _shooter;

        public void Init(IShooter shooter)
        {
            _shooter = shooter;
            _shooter.ShootingPoint = ShootingPoint.transform;
        }

        private void Awake()
        {
            ShootingPoint = GetComponentInChildren<ShootingPointView>();
            ProjectileContainerView = GetComponentInChildren<ProjectileContainerView>();
        }

        private void Update()
        {
            _shooter?.Shoot();
        }
    }
}