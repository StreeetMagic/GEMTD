using Games;
using Infrastructure.Services.AssetProviders;
using Infrastructure.Services.ZenjectFactory;
using UserInterface.HeadsUpDisplays;

namespace Infrastructure.Services.GameFactories
{
  public class UserInterfaceFactory
  {
    private IZenjectFactory _zenjectFactory;

    public UserInterfaceFactory(IZenjectFactory zenjectFactory)
    {
      _zenjectFactory = zenjectFactory;
    }

    public HeadsUpDisplayView CreateHeadUpDisplay() =>
      _zenjectFactory.Instantiate<HeadsUpDisplayView>();
  }
}