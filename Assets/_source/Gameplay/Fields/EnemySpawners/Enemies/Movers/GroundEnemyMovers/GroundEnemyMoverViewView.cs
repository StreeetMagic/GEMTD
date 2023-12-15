using System;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies.Movers.GroundEnemyMovers
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class GroundEnemyMoverViewView : MonoBehaviour, IEnemyMoverView
    {
        private EnemyMoverModel _enemyMoverModel;
        private Rigidbody _rigidbody;

        public Vector2Int LastReachedPoint { get; set; }
        public Vector2Int NextPoint { get; set; }
        private int _nextCheckPointIndex;

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
            _nextCheckPointIndex = 1;
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

            float newXposition = Mathf.MoveTowards(position.x, NextPoint.x, _enemyMoverModel.Speed * Time.deltaTime);
            float newZposition = Mathf.MoveTowards(position.z, NextPoint.y, _enemyMoverModel.Speed * Time.deltaTime);

            cachedTransform.position = new Vector3(newXposition, 0, newZposition);

            float distance = Vector3.Distance(transform.position, new Vector3(NextPoint.x, 0, NextPoint.y));

            if (distance < MinDistance)
            {
                ReachPoint();
            }
            else
            {
                Debug.LogWarning("Иду дальше");
            }

            _enemyMoverModel.Move(transform.position);
        }

        private void ReachPoint()
        {
            transform.position = new Vector3(NextPoint.x, 0, NextPoint.y);

            LastReachedPoint = NextPoint;

            if (LastReachedPoint == _enemyMoverModel.Points[^1])
            {
                _enemyMoverModel.Die();
                return;
            }

            _nextCheckPointIndex++;
            NextPoint = _enemyMoverModel.Points[_nextCheckPointIndex];
        }

        private void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
}