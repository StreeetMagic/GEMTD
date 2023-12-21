using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class ThronePanelView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }

        private int _maxHealth;

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        private void Awake()
        {
            _maxHealth = StaticDataService.Get<GameConfig>().ThroneHealth;
        }

        private void OnEnable()
        {
            HealthText.text = GetHealthText(CurrentDataService.ThroneModel.Health.Value, _maxHealth);
            CurrentDataService.ThroneModel.Health.ValueChanged += OnHealthChanged;
        }
        
        private void OnDisable()
        {
            CurrentDataService.ThroneModel.Health.ValueChanged -= OnHealthChanged;
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