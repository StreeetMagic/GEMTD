using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Fields.Enemies.HealthBars
{
  public class HealthBarView : MonoBehaviour
  {
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private Image _image;

    private float _maxHealth;

    private void Start()
    {
      _maxHealth = _enemyView.EnemyModel.Health.Value;
    }

    private void OnEnable()
    {
      _enemyView.EnemyModel.Health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
      _enemyView.EnemyModel.Health.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
      _image.fillAmount = health / _maxHealth;
    }
  }
}