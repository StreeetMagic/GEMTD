using Gameplay.Enemies.Movers;
using Gameplay.Enemies.TriggerAreas;
using UnityEngine;

namespace Gameplay.Enemies
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
            EnemyModel = new EnemyModel();
            _enemyMoverView.Init(EnemyModel.MoverModel);
            _triggerAreaView = GetComponentInChildren<TriggerAreaView>();
            _triggerAreaView.Init(EnemyModel);
        }
    }
}