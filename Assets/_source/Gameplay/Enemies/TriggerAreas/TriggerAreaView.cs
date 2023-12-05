using UnityEngine;

namespace Gameplay.Enemies.TriggerAreas
{
    public class TriggerAreaView : MonoBehaviour
    {
        [SerializeField] private EnemyView _enemyView;

        public TriggerAreaModel TriggerAreaModel { get; set; }

        private void Awake()
        {
            TriggerAreaModel = new TriggerAreaModel(_enemyView.EnemyModel);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerAreaModel.OnTriggerEnter(other);
        }
    }
}