using Gameplay.Fields.EnemySpawners.Enemies.Movers;
using Gameplay.Fields.EnemySpawners.Enemies.TriggerAreas;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(IEnemyMoverView))]
    public class EnemyView : MonoBehaviour
    {
        private IEnemyMoverView _enemyMoverView;
        private TriggerAreaView _triggerAreaView;

        public EnemyModel EnemyModel { get; set; }

        private void Awake()
        {
            _enemyMoverView = GetComponent<IEnemyMoverView>();

            _triggerAreaView = GetComponentInChildren<TriggerAreaView>();
        }

        public void Init(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
            _enemyMoverView.Init(EnemyModel.MoverModel);
            _triggerAreaView.Init(EnemyModel);

            EnemyModel.Died += OnDied;
        }

        private void OnDestroy()
        {
            EnemyModel.Died -= OnDied;
        }

        private void OnDied(EnemyModel enemyModel)
        {
            GameObject cachedGameObject;
            (cachedGameObject = gameObject).SetActive(false);
            Destroy(cachedGameObject);
        }
    }
}