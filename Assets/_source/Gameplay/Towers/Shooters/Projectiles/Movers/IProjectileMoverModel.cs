using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.Movers
{
    public interface IProjectileMoverModel
    {
        void Move();
        Transform Transform { get; set; }
        Transform Target { get; set; }
        Rigidbody Rigidbody { get; set; }
    }
}