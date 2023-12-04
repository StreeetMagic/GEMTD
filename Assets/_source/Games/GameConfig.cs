using InfastuctureCore.Services;
using UnityEngine;

namespace Games
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "GameConfigSO")]
    public class GameConfig : ScriptableObject , IStaticData
    {
    }
}