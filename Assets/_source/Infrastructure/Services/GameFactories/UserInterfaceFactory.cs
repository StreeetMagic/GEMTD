using Infrastructure.Services.ZenjectFactory;
using UserInterface.HeadsUpDisplays;

namespace Infrastructure.Services.GameFactories
{
  public class UserInterfaceFactory
  {
    private readonly IZenjectFactory _zenjectFactory;

    public UserInterfaceFactory(IZenjectFactory zenjectFactory)
    {
      _zenjectFactory = zenjectFactory;
    }

    public HeadsUpDisplayView CreateHeadUpDisplay() =>
      _zenjectFactory.Instantiate<HeadsUpDisplayView>();
  }
}