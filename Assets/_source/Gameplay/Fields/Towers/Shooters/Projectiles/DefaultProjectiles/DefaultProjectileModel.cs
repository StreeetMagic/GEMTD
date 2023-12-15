﻿using System;
using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
    class DefaultProjectileModel : IProjectileModel
    {
        public event Action Died;

        public IProjectileMoverModel Mover { get; }
        public float Damage { get; set; } = 5f;

        public DefaultProjectileModel(EnemyModel target)
        {
            Mover = new DefaultProjectileMoverModel(target, this);
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}