using Gameplay.Fields.EnemySpawners.Enemies;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles
{
    public interface IProjectileMoverModel
    {
        float Speed { get; set; }
        EnemyModel Target { get; set; }
        Vector3 Position { get; }

        void Move(Vector3 position);
    }
}