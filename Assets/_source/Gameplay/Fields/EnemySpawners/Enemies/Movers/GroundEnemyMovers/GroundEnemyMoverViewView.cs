using System;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers.GroundEnemyMovers
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundEnemyMoverViewView : MonoBehaviour, IEnemyMoverView
    {
        private EnemyMoverModel _enemyMoverModel;
        private Rigidbody _rigidbody;

        public Vector3 LastReachedCheckpoint { get; set; }
        public Vector3 NextCheckpoint { get; set; }

        public void ReachCheckpoint()
        {
            int lastReachedCheckpointIndex = Array.IndexOf(_enemyMoverModel.CheckPoints, LastReachedCheckpoint);

            LastReachedCheckpoint = NextCheckpoint;

            if (lastReachedCheckpointIndex < _enemyMoverModel.CheckPoints.Length - 1)
            {
                NextCheckpoint = _enemyMoverModel.CheckPoints[lastReachedCheckpointIndex + 1];
            }
            else
            {
                NextCheckpoint = _enemyMoverModel.CheckPoints[0];
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(EnemyMoverModel enemyMoverModel)
        {
            _enemyMoverModel = enemyMoverModel;
            LastReachedCheckpoint = _enemyMoverModel.CheckPoints[0];
            NextCheckpoint = _enemyMoverModel.CheckPoints[1];
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            const float MinDistance = 0.1f;

            Transform cachedTransform = transform;
            Vector3 position = cachedTransform.position;

            _rigidbody.MovePosition(position + (NextCheckpoint - position).normalized * (_enemyMoverModel.Speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, NextCheckpoint) < MinDistance)
                ReachCheckpoint();

            _enemyMoverModel.Move(transform.position);
        }
    }
}