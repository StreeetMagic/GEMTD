using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public interface IShooter
    {
        void Shoot(Transform target);
    }
}