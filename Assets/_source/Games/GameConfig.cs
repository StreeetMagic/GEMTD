using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Games
{
    public class GameConfig : IStaticData
    {
        public int ThroneHealth { get; private set; } = 100;
    }
}