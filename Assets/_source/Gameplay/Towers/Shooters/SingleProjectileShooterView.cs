using System;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public class SingleProjectileShooterView : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private IShooter _shooter;

        public void Init(IShooter shooter)
        {
            _shooter = shooter;
        }

        private void Update()
        {
            _shooter?.Shoot();
        }
    }
}