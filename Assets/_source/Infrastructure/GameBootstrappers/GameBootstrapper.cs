using System.Collections;
using System.ComponentModel;
using Games;
using Infrastructure.DIC;
using UnityEngine;
using Zenject;

namespace Infrastructure.GameBootstrappers
{
  public class GameBootstrapper : MonoBehaviour
  {
    private IGodFactory _godFactory;

    [Inject]
    public void Construct(IGodFactory godFactory)
    {
      _godFactory = godFactory;
    }

    private void Awake()
    {
      Game game = _godFactory.Create<Game>();
      game.Start();
    }
  }
}