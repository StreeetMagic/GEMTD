using System;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers
{
    public class EnemyMoverModel
    {
        public EnemyMoverModel(Vector3 position, Vector2Int[] points)
        {
            Position = position;
            Points = points;
        }

        public event Action Dead;

        public Vector3 Position { get; set; }
        public float Speed { get; set; } = 5f;
        public Vector2Int[] Points { get; }

        public void Move(Vector3 position)
        {
            const float Tolerance = 0.01f;

            Position = position;

            if (Math.Abs(Position.x - Points[^1].x) < Tolerance && Math.Abs(Position.z - Points[^1].y) < Tolerance)
            {
                Die();
            }
        }

        private void Die()
        {
            Dead?.Invoke();
        }
    }
}