using System.Collections.Generic;
using Gameplay.Fields.Towers.Shooters.Projectiles.ProjectileContainers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters
{
    public interface IShooter
    {
        Transform ShootingPoint { get; set; }
        ProjectileContainerModel ProjectileContainerModel { get; set; }
        List<Transform> Targets { get; set; }

        void Shoot();
        void AddTarget(Transform otherTransform);
        void RemoveTarget(Transform otherTransform);
    }
}