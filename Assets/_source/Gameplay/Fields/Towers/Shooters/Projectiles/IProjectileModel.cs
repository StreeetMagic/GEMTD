using System;

namespace Gameplay.Fields.Towers.Shooters.Projectiles
{
    public interface IProjectileModel
    {
        float Damage { get; set; }
        
        IProjectileMoverModel Mover { get; }
        
        void Die();
        event Action Died;
    }
}