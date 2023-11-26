using UnityEngine;

namespace Games.Config.Resources
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "GameConfigSO")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public int FieldXSize { get; private set; }
        [field: SerializeField] public int FieldYSize { get; private set; }
    }
}