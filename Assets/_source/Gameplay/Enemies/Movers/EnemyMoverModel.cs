using UnityEngine;

namespace Gameplay.Enemies.Movers
{
    public class EnemyMoverModel
    {
        public Vector3 Position { get; set; } = Vector3.zero;
        public float Speed { get; set; } = 5f;

        public void Move(Vector3 position)
        {
            Position = position;
        }
    }
}