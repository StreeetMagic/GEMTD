using Games;
using Infrastructure;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.StaticDataServices;
using TMPro;
using UnityEngine;
using Zenject;

namespace UserInterface.HeadsUpDisplays.ThronePanels
{
    public class ThronePanelView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }

        private int _maxHealth;

        private ICurrentDataService _currentDataService;
        private IStaticDataService _staticDataService;
        
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

        private string GetHealthText(int current, int max)
        {
            return $"{current}/{max}";
        }
    }
}