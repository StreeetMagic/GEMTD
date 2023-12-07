using System;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers
{
    public class EnemyMoverModel
    {
        public EnemyMoverModel(Vector3 position, Vector3[] points)
        {
            Position = position;
            Points = points;
        }

        public event Action Dead;

        public Vector3 Position { get; set; }
        public float Speed { get; set; } = 5f;
        public Vector3[] Points { get; set; }

        public void Move(Vector3 position)
        {
            Position = position;

            if (Position == Points[Points.Length - 1])
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