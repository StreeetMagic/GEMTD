using System;
using UnityEngine;

namespace Gameplay.Enemies.Movers.GroundEnemyMovers
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundEnemyMoverViewView : MonoBehaviour, IEnemyMoverView
    {
        private EnemyMoverModel _enemyMoverModel;
        private Rigidbody _rigidbody;

        [field: SerializeField] public Transform[] CheckPoints { get; set; }
        public Transform LastReachedCheckpoint { get; set; }
        public Transform NextCheckpoint { get; set; }

        public void ReachCheckpoint()
        {
            int lastReachedCheckpointIndex = Array.IndexOf(CheckPoints, LastReachedCheckpoint);

            LastReachedCheckpoint = NextCheckpoint;

            if (lastReachedCheckpointIndex < CheckPoints.Length - 1)
            {
                NextCheckpoint = CheckPoints[lastReachedCheckpointIndex + 1];
            }
            else
            {
                NextCheckpoint = CheckPoints[0];
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            LastReachedCheckpoint = CheckPoints[0];
            NextCheckpoint = CheckPoints[1];
        }

        public void Init(EnemyMoverModel enemyMoverModel)
        {
            _enemyMoverModel = enemyMoverModel;
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            _rigidbody.MovePosition(transform.position + (NextCheckpoint.position - transform.position).normalized * (_enemyMoverModel.Speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, NextCheckpoint.position) < 0.1f)
            {
                ReachCheckpoint();
            }

            _enemyMoverModel.Move(transform.position);
        }
    }
}