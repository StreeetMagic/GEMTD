using System;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers.GroundEnemyMovers
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundEnemyMoverViewView : MonoBehaviour, IEnemyMoverView
    {
        private EnemyMoverModel _enemyMoverModel;
        private Rigidbody _rigidbody;

        public Vector3 LastReachedPoint { get; set; }
        public Vector3 NextPoint { get; set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
        }

        public void Init(EnemyMoverModel enemyMoverModel)
        {
            _enemyMoverModel = enemyMoverModel;
            LastReachedPoint = _enemyMoverModel.Points[0];
            NextPoint = _enemyMoverModel.Points[1];
            _enemyMoverModel.Dead += OnDead;
        }

        private void OnDestroy()
        {
            _enemyMoverModel.Dead -= OnDead;
        }

        public void Move()
        {
            const float MinDistance = 0.1f;

            Transform cachedTransform = transform;
            Vector3 position = cachedTransform.position;

            _rigidbody.MovePosition(position + (NextPoint - position).normalized * (_enemyMoverModel.Speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, NextPoint) < MinDistance)
                ReachPoint();

            _enemyMoverModel.Move(transform.position);
        }

        private void ReachPoint()
        {
            int lastReachedCheckpointIndex = Array.IndexOf(_enemyMoverModel.Points, LastReachedPoint);

            LastReachedPoint = NextPoint;

            if (lastReachedCheckpointIndex < _enemyMoverModel.Points.Length - 1)
            {
                NextPoint = _enemyMoverModel.Points[lastReachedCheckpointIndex + 1];
            }
            else
            {
                NextPoint = _enemyMoverModel.Points[0];
            }
        }

        public void OnDead()
        {
            Destroy(gameObject);
        }
    }
}