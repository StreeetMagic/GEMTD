using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields
{
    [CreateAssetMenu(menuName = "Configs/Field Congig", fileName = "FieldConfig")]
    public class FieldConfig : ScriptableObject, IStaticData
    {
        [field: SerializeField] public int FieldXSize { get; private set; }
        [field: SerializeField] public int FieldYSize { get; private set; }
    }
}