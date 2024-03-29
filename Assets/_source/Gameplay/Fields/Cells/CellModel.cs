﻿using System;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls;
using Infrastructure.Services.GameFactories;
using UnityEngine;

namespace Gameplay.Fields.Cells
{
  public class CellModel
  {
    private readonly IGameFactoryService _gameFactoryService;

    public CellModel(Vector2Int coordinates, BlockModel blockModel, IGameFactoryService gameFactoryService)
    {
      _gameFactoryService = gameFactoryService;
      Coordinates = coordinates;
      BlockModel = blockModel;
    }

    public Vector2Int Coordinates { get; }
    public CheckPointModel CheckPointModel { get; private set; }
    public WallModel WallModel { get; private set; }
    public BlockModel BlockModel { get; private set; }
    public TowerModel TowerModel { get; private set; }
    public bool TowerIsConfirmed { set; get; }
    public bool IsPassable => WallModel == null && TowerModel == null;

    public bool IsEmpty => CheckPointModel == null && WallModel == null && TowerModel == null;
    public bool HasWall => WallModel != null;
    public bool CanBeReplacedWithTower => WallModel != null && TowerModel == null && CheckPointModel == null && TowerIsConfirmed == false;
    public bool HasCheckPoint => CheckPointModel != null;

    public event Action ChekpointModelSet;
    public event Action WallModelSet;
    public event Action WallModelRemoved;
    public event Action TowerModelSet;
    public event Action TowerModelRemoved;
    public event Action TowerModelConfirmed;

    public void SetCheckpointModel(CheckPointModel checkPointModel)
    {
      CheckPointModel = checkPointModel;
      ChekpointModelSet?.Invoke();
    }

    public void SetTowerModel(TowerModel towerModel)
    {
      TowerModel = towerModel;
      TowerModelSet?.Invoke();
    }

    public void SetWallModel(WallModel wallModel)
    {
      WallModel = wallModel;
      WallModelSet?.Invoke();
    }

    public void RemoveWallModel()
    {
      WallModel = null;
      WallModelRemoved?.Invoke();
    }

    public void RemoveTowerModel()
    {
      TowerModel = null;
      TowerModelRemoved?.Invoke();
    }

    public void ConfirmTower()
    {
      TowerModelConfirmed?.Invoke();
      TowerIsConfirmed = true;
    }

    public void Upgrade(TowerType towerType, int level)
    {
      RemoveTowerModel();
      SetTowerModel(_gameFactoryService.FieldFactory.CreateTowerModel(towerType, level));
    }
  }
}