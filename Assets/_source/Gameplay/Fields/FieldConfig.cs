using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields
{
    [CreateAssetMenu(menuName = "Configs/Field Congig", fileName = "FieldConfig")]
    public class FieldConfig : ScriptableObject, IStaticData
    {
        [field: SerializeField] public int FieldSize { get; private set; }
    }
}