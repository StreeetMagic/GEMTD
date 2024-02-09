using Infrastructure.GameBootstrappers;
using Infrastructure.Services.ZenjectFactory;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameRunners
{
  /// <summary>
  ///   <para>Should be placed on scenes manually</para>
  ///   <para>1 GameRunner on each scene</para>
  /// </summary>
  public class GameRunner : MonoBehaviour
  {
    [SerializeField] private GameBootstrapper _bootstrapperPrefab;

    private IZenjectFactory _factory;

    [Inject]
    public void Construct(IZenjectFactory factory)
    {
      _factory = factory;
    }

    private void Awake()
    {
      CreateBootstrapper();
    }

    private void CreateBootstrapper()
    {
      if (FindObjectOfType<GameBootstrapper>() == null)
        _factory.Instantiate(_bootstrapperPrefab);

      Destroy(gameObject);
    }
  }
}