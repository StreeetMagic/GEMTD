using System.Collections;
using System.Collections.Generic;
using Gameplay.BlockGrids.Cells;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/MapWallsConfig", fileName = "MapWallsConfigSO")]
public class MapWallsConfig : ScriptableObject
{
    public List<Coordinates> Coordinates;
}