using Gameplay.Fields.Enemies;
using Infrastructure;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Gameplay.Fields.Thrones
{
    public class ThroneView : MonoBehaviour
    {
        private ThroneModel Model => ServiceLocator.Instance.Get<ICurrentDataService>().ThroneModel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView _))
            {
                Model.Health.Value -= 5;
            }
        }
    }
}