using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    class SingleProjectileShooterModel : IShooter
    {
        public void Shoot(Transform target)
        {
            Debug.Log("Pew");
        }
    }
}