using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.StaticDataServices;
using TMPro;
using UnityEngine;
using Zenject;

namespace UserInterface.HeadsUpDisplays.ThronePanels
{
  public class ThronePanelView : MonoBehaviour
  {
    private ICurrentDataService _currentDataService;

    private int _maxHealth;
    private IStaticDataService _staticDataService;
    [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }

    [Inject]
    public void Construct(ICurrentDataService currentDataService, IStaticDataService staticDataService)
    {
      _currentDataService = currentDataService;
      _staticDataService = staticDataService;
    }

    private void Awake()
    {
      _maxHealth = _staticDataService.GameConfig.ThroneHealth;
    }

    private void OnEnable()
    {
      HealthText.text = GetHealthText(_currentDataService.ThroneModel.Health.Value, _maxHealth);
      _currentDataService.ThroneModel.Health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
      _currentDataService.ThroneModel.Health.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
      HealthText.text = GetHealthText(health, _maxHealth);
    }

    private string GetHealthText(int current, int max) =>
      $"{current}/{max}";
  }
}