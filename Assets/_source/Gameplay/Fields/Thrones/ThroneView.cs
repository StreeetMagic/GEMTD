using Gameplay.Fields.Enemies;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Gameplay.Fields.Thrones
{
    public class ThroneView : MonoBehaviour
    {
        public ThroneModel Model => ServiceLocator.Instance.Get<ICurrentDataService>().ThroneModel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView enemyView))
            {
                Model.Health.Value -= 5;
            }
        }
    }
}