using UnityEngine;

namespace Gameplay.Enemies.TriggerAreas
{
    public class TriggerAreaView : MonoBehaviour
    {
        public TriggerAreaModel TriggerAreaModel { get; set; }

        public void Init(EnemyModel enemyModel)
        {
            TriggerAreaModel = new TriggerAreaModel(enemyModel);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerAreaModel.OnTriggerEnter(other);
        }
    }
}