using UnityEngine;

namespace Gameplay.Fields.Cells.Towers.Shooters
{
    class SingleProjectileShooter : IShooter
    {
        public void Shoot(Transform target)
        {
            Debug.Log("Pew");
        }
    }
}