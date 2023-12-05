using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public interface IShooter
    {
        List<Transform> Targets { get; set; }
        Transform ShootingPoint { get; set; }
        void Shoot();
        void AddTarget(Transform otherTransform);
        void RemoveTarget(Transform otherTransform);
    }
}