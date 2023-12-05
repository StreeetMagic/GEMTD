using UnityEngine;

namespace Gameplay.Fields.Cells.Towers.Shooters
{
    public interface IShooter
    {
        void Shoot(Transform target);
    }
}