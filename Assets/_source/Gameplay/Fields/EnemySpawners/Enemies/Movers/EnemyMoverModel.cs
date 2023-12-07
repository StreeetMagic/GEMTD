using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers
{
    public class EnemyMoverModel
    {
        public EnemyMoverModel(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; set; }
        public float Speed { get; set; } = 5f;

        public void Move(Vector3 position)
        {
            Position = position;
        }
    }
}