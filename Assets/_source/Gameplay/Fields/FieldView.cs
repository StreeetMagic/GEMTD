using System.Linq;
using Gameplay.Fields.CellsContainers;
using Gameplay.Fields.EnemySpawners;
using Infrastructure;
using Infrastructure.Services.GameFactories;
using UnityEngine;
using Zenject;

namespace Gameplay.Fields
{
  public class FieldView : MonoBehaviour
  {
    private IGameFactoryService _gameFactoryService;
    private FieldModel _fieldModel;

    private CellsContainerView CellsContainerView { get; set; }
    private EnemySpawnerView EnemySpawnerView { get; set; }

    [Inject]
    public void Construct(IGameFactoryService gameFactoryService)
    {
      _gameFactoryService = gameFactoryService;
    }

    private void Awake()
    {
      CellsContainerView = GetComponentInChildren<CellsContainerView>();
      EnemySpawnerView = GetComponentInChildren<EnemySpawnerView>();
    }

    public void Init(FieldModel fieldModel)
    {
      _fieldModel = fieldModel;

      // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
      _fieldModel
        .CellsContainerModel
        .CellModels
        .Select(cellData =>
          _gameFactoryService
            .FieldFactory
            .CreateCellView(cellData, CellsContainerView.transform))
        .ToArray();

      EnemySpawnerView.Init(_fieldModel.EnemySpawnerModel);
    }
  }
}