using System;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public class SingleProjectileShooterView : MonoBehaviour
    {
        [field: SerializeField] public Transform ShootingPoint { get; set; }

        private IShooter _shooter;

        public void Init(IShooter shooter)
        {
            _shooter = shooter;
            _shooter.ShootingPoint = ShootingPoint;
        }

        private void Update()
        {
            _shooter?.Shoot();
        }
    }
}