using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles
{
    public interface IProjectileMoverModel
    {
        float Speed { get; set; }
        Transform Target { get; set; }
        Vector3 Position { get; }

        void Move(Vector3 position);
    }
}