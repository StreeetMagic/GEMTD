using Infrastructure.Utilities;

namespace Gameplay.Fields.Thrones
{
    public class ThroneModel
    {
        public ThroneModel(int health)
        {
            Health = new ReactiveProperty<int>(health); 
        }

        public ReactiveProperty<int> Health { get; private set; }
    }
}