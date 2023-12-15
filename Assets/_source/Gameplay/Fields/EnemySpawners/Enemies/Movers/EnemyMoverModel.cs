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

            Debug.LogWarning(points.Length);
        }

        public event Action Dead;

        public Vector3 Position { get; set; }
        public float Speed { get; set; } = 10;
        public Vector2Int[] Points { get; }

        public void Move(Vector3 position)
        {
            Position = position;
        }

        public void Die()
        {
            Dead?.Invoke();
        }
    }
}