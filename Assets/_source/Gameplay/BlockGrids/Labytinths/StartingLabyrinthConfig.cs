using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.BlockGrids.Cells;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.StaticDataServices;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfigSO")]
public class StartingLabyrinthConfig : ScriptableObject
{
    [ShowInInspector] private Coordinates[] _coordinates;

    private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
    private MapWallsConfig MapWallsConfig => StaticDataService.Get<MapWallsConfig>();

    [Button]
    public void SetCoordinates()
    {
        _coordinates = MapWallsConfig.Coordinates.ToArray();
    }

    public Coordinates[] Coordinates => _coordinates.ToArray();
}