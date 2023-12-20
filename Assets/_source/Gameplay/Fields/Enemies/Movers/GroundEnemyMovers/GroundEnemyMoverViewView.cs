using UnityEngine;

namespace Gameplay.Fields.Enemies.Movers.GroundEnemyMovers
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class GroundEnemyMoverViewView : MonoBehaviour, IEnemyMoverView
    {
        [SerializeField] private Transform _damagePoint;

        private EnemyMoverModel _enemyMoverModel;

        public Vector2Int LastReachedPoint { get; set; }
        public Vector2Int NextPoint { get; set; }
        private int _nextCheckPointIndex;

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
            _enemyMoverModel.Model.Died += OnDead;
        }

        private void OnDestroy()
        {
            _enemyMoverModel.Model.Died -= OnDead;
        }

        public void Move()
        {
            const float MinDistance = 0.01f;

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

            _enemyMoverModel.Move(transform.position, _damagePoint.position);
        }

        private void ReachPoint()
        {
            transform.position = new Vector3(NextPoint.x, 0, NextPoint.y);

            LastReachedPoint = NextPoint;

            if (LastReachedPoint == _enemyMoverModel.Points[^1])
            {
                _enemyMoverModel.Model.Die();
                return;
            }

            _nextCheckPointIndex++;
            NextPoint = _enemyMoverModel.Points[_nextCheckPointIndex];
        }

        private void OnDead(EnemyModel enemy)
        {
            gameObject.SetActive(false);
        }
    }
}