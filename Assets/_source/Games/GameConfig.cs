using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Games
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "GameConfigSO")]
    public class GameConfig : ScriptableObject, IStaticData
    {
        [field: SerializeField] public int ThroneHealth { get; private set; }
    }
}