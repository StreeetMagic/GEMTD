using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemyContainers;
using Gameplay.Fields.EnemySpawners;
using Gameplay.Fields.Thrones;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;
using Gameplay.Fields.Walls;
using Games;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Services.ZenjectFactory;
using Infrastructure.Utilities;
using UnityEngine;

namespace Infrastructure.Services.GameFactories.Factories
{
  public class FieldFactory
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly ICurrentDataService _currentDataService;
    private readonly IGameFactoryService _gameFactoryService;
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private readonly IStaticDataService _staticDataService;
    private readonly IZenjectFactory _zenjectFactory;

    public FieldFactory(IZenjectFactory zenjectFactory, IStaticDataService staticDataService,
      ICurrentDataService currentDataService, IGameFactoryService gameFactoryService,
      IStateMachine<IGameLoopState> gameLoopStateMachine, ICoroutineRunner coroutineRunner)
    {
      _zenjectFactory = zenjectFactory;
      _staticDataService = staticDataService;
      _currentDataService = currentDataService;
      _gameFactoryService = gameFactoryService;
      _gameLoopStateMachine = gameLoopStateMachine;
      _coroutineRunner = coroutineRunner;
    }

    public FieldModel CreateFieldModel(GameConfig gameConfig) =>
      new(CreateCellModels(_staticDataService.FieldConfig.FieldSize), CreateEnemySpawnerModel(gameConfig), _staticDataService);

    private EnemySpawnerModel CreateEnemySpawnerModel(GameConfig gameConfig) =>
      new(CreateEnemyContainerModel(), gameConfig.SpawnCooldown, gameConfig.WaveMobCount, _coroutineRunner, _staticDataService,
        _gameFactoryService, _currentDataService);

    private EnemyContainerModel CreateEnemyContainerModel() =>
      new(_gameLoopStateMachine);

    public BlockView CreateBlockView(BlockModel blockModel, Transform parent) =>
      _zenjectFactory.Instantiate<BlockView>()
        .With(e => e.Init(blockModel))
        .With(e => e.transform.SetParent(parent))
        .With(e => e.transform.localPosition = Vector3.zero);

    public CheckpointView CreateCheckpointView(CheckPointModel checkPointModel, Transform transform) =>
      _zenjectFactory.Instantiate<CheckpointView>()
        .With(e => e.transform.SetParent(transform))
        .With(e => e.transform.localPosition = Vector3.zero)
        .With(e => e.name = "Checkpoint " + checkPointModel.Number);

    public CellView CreateCellView(CellModel cellModel, Transform transform) =>
      _zenjectFactory.Instantiate<CellView>()
        .With(e => e.Init(cellModel))
        .With(e => e.transform.SetParent(transform))
        .With(e => e.transform.localPosition = new Vector3(cellModel.Coordinates.x, 0, cellModel.Coordinates.y))
        .With(e => e.name = "Cell (" + cellModel.Coordinates.x + ", " + cellModel.Coordinates.y + ")");

    public FieldView CreateFieldView(FieldModel fieldModel) =>
      _zenjectFactory.Instantiate<FieldView>()
        .With(e => e.Init(fieldModel));

    public WallView CreateWallView(WallModel wallModel, Transform transform) =>
      _zenjectFactory.Instantiate<WallView>()
        .With(e => e.Init(wallModel))
        .With(e => e.transform.SetParent(transform))
        .With(e => e.transform.localPosition = Vector3.zero);

    public TowerView CreateTowerView(TowerModel towerModel, Transform transform) =>
      _zenjectFactory.Instantiate<TowerView>()
        .With(e => e.Init(towerModel, _staticDataService.TowersConfig.GetMaterial(towerModel.Type)))
        .With(e => e.transform.SetParent(transform))
        .With(e => e.transform.localPosition = Vector3.zero)
        .With(e => e.SetScale(_staticDataService.TowersConfig.GetScale(towerModel.Level)));

    public void CreateCheckpointsModels()
    {
      IReadOnlyList<CheckpointValues> configs = _staticDataService.CheckpointsConfig.CheckpointValues;

      var checkpointDatas = new CheckPointModel[configs.Count];

      for (var i = 0; i < configs.Count; i++)
      {
        CellModel cell = _currentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(configs[i].Coordinates);
        checkpointDatas[i] = CreateCheckPointModel(configs[i].Number, cell);
        cell.SetCheckpointModel(checkpointDatas[i]);
      }
    }

    public WallModel CreateWallModel() =>
      new();

    public TowerModel CreateTowerModel(TowerType towerType, int level)
    {
      var singleProjectileShooterModel = new SingleProjectileShooterModel(_gameFactoryService);
      return new TowerModel(towerType, singleProjectileShooterModel, new TargetDetetcorModel(singleProjectileShooterModel), level);
    }

    public void CreateStartingLabyrinth() =>
      _staticDataService.StartingLabyrinthConfig.Coordinates.ToList().ForEach(coordinate => _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinate)
        .SetWallModel(CreateWallModel()));

    private CheckPointModel CreateCheckPointModel(int number, CellModel cell) =>
      new(number, cell);

    private CellModel[] CreateCellModels(int size) =>
      Enumerable.Range(0, size)
        .SelectMany(i => Enumerable.Range(0, size)
          .Select(j => new CellModel(new Vector2Int(i, j), new BlockModel(), _gameFactoryService)))
        .ToArray();

    public ThroneModel CreateThroneModel() =>
      new(_staticDataService.GameConfig.ThroneHealth);
  }
}