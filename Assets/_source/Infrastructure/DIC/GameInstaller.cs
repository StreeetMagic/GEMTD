using Infrastructure.Services.InputServices;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIC
{
  public class GameInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindInputService();
    }

    private void BindInputService() =>
      Container
        .Bind<IInputService>()
        .To<InputService>()
        .AsSingle();
  }
}