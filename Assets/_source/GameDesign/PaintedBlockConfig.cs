using System.Collections.Generic;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

[CreateAssetMenu(fileName = "PaintedBlockConfig", menuName = "Configs/PaintedBlockConfig")]
public class PaintedBlockConfig : ScriptableObject , IStaticData
{
    public List<Coordinates> Coordinates;
}