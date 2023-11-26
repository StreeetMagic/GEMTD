using UnityEngine;

namespace Gameplay.BlockGrids
{
    [CreateAssetMenu(menuName = "Configs/BlockGrid Congig", fileName = "BlockGridConfigSO")]
    public class BlockGridConfig : ScriptableObject
    {
        [field: SerializeField] public int FieldXSize { get; private set; }
        [field: SerializeField] public int FieldYSize { get; private set; }
    }
}