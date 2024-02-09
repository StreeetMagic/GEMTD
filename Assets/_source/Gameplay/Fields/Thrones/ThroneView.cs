using Gameplay.Fields.Enemies;
using Infrastructure.Services.CurrentDatas;
using UnityEngine;
using Zenject;

namespace Gameplay.Fields.Thrones
{
  public class ThroneView : MonoBehaviour
  {
    private ICurrentDataService _currentDataService;

    [Inject]
    public void Construct(ICurrentDataService currentDataService)
    {
      _currentDataService = currentDataService;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out EnemyView _))
      {
        _currentDataService.ThroneModel.Health.Value -= 5;
      }
    }
  }
}