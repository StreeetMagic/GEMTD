using InfastuctureCore.Services.StaticDataServices;

namespace Gameplay.Fields
{
    public class FieldConfig : IStaticData
    {
        public int FieldSize { get; private set; } = 17;
    }
}