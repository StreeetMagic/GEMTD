using System.Collections.Generic;
using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.ProjectileContainers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters
{
    public interface IShooter
    {
        Transform ShootingPoint { get; set; }
        ProjectileContainerModel ProjectileContainerModel { get; set; }
        List<EnemyModel> Targets { get; set; }

        void Shoot();
        void AddTarget(EnemyModel target);
        void RemoveTarget(EnemyModel target);
    }
}