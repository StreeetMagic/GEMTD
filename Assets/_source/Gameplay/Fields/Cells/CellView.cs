using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls;
using Infrastructure;
using Infrastructure.Services.GameFactories;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay.Fields.Cells
{
  public class CellView : MonoBehaviour, IPointerDownHandler
  {
    private IGameFactoryService _gameFactoryService;

    public CellModel CellModel { get; private set; }
    public BlockView BlockView { get; private set; }
    public CheckpointView CheckpointView { get; private set; }
    public WallView WallView { get; private set; }
    public TowerView TowerView { get; private set; }

    public bool IsPainted { get; set; }

    [Inject]
    public void Construct(IGameFactoryService gameFactoryService)
    {
      _gameFactoryService = gameFactoryService;
    }

    public void Init(CellModel cellModel)
    {
      CellModel = cellModel;
      BlockView = _gameFactoryService.FieldFactory.CreateBlockView(CellModel.BlockModel, transform);
      Subscribe();
    }

    public void OnDestroy()
    {
      Unsubscribe();
    }

    private void Subscribe()
    {
      CellModel.ChekpointModelSet += OnCheckpointModelSet;
      CellModel.WallModelSet += OnWallModelSet;
      CellModel.WallModelRemoved += OnWallModelRemoved;
      CellModel.TowerModelSet += OnTowerModelSet;
      CellModel.TowerModelRemoved += OnTowerModelRemoved;
      CellModel.TowerModelConfirmed += OnTowerModelConfirmed;
    }

    private void OnTowerModelConfirmed()
    {
      // if (TowerView != null)
      //     TowerView.SetScale();
    }

    private void Unsubscribe()
    {
      CellModel.ChekpointModelSet -= OnCheckpointModelSet;
      CellModel.WallModelSet -= OnWallModelSet;
      CellModel.WallModelRemoved -= OnWallModelRemoved;
      CellModel.TowerModelSet -= OnTowerModelSet;
      CellModel.TowerModelRemoved -= OnTowerModelRemoved;
      CellModel.TowerModelConfirmed -= OnTowerModelConfirmed;
    }

    private void OnTowerModelRemoved()
    {
      if (TowerView != null)
      {
        Destroy(TowerView.gameObject);
        TowerView = null;
      }
    }

    private void OnCheckpointModelSet()
    {
      CheckpointView = _gameFactoryService.FieldFactory.CreateCheckpointView(CellModel.CheckPointModel, transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      Debug.Log(name);
    }

    public void UnHighlight()
    {
      BlockView.UnHighlight();
      IsPainted = false;
    }

    public void Highlight(Material material)
    {
      BlockView.Highlight(material);
    }

    private void OnWallModelSet()
    {
      WallView = _gameFactoryService.FieldFactory.CreateWallView(CellModel.WallModel, transform);
    }

    private void OnTowerModelSet()
    {
      TowerView = _gameFactoryService.FieldFactory.CreateTowerView(CellModel.TowerModel, transform);
    }

    private void OnWallModelRemoved()
    {
      Destroy(WallView.gameObject);
      WallView = null;
    }

    public void PaintBlock(Material material)
    {
      BlockView.PaintBlock(material);
      IsPainted = true;
    }
  }
}