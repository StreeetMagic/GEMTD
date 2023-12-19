using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Fields.Cells
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        public CellModel CelLModel { get; private set; }

        public BlockView BlockView { get; private set; }
        public CheckpointView CheckpointView { get; private set; }
        public WallView WallView { get; private set; }
        public TowerView TowerView { get; private set; }

        public bool IsPainted { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Init(CellModel cellModel)
        {
            CelLModel = cellModel;
            BlockView = GameFactoryService.FieldFactory.CreateBlockView(CelLModel.BlockModel, transform);
            Subscribe();
        }

        public void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            CelLModel.ChekpointModelSet += OnCheckpointModelSet;
            CelLModel.WallModelSet += OnWallModelSet;
            CelLModel.WallModelRemoved += OnWallModelRemoved;
            CelLModel.TowerModelSet += OnTowerModelSet;
            CelLModel.TowerModelRemoved += OnTowerModelRemoved;
            CelLModel.TowerModelConfirmed += OnTowerModelConfirmed;
        }

        private void OnTowerModelConfirmed()
        {
            if (TowerView != null)
                TowerView.ReduceScale();
        }

        private void Unsubscribe()
        {
            CelLModel.ChekpointModelSet -= OnCheckpointModelSet;
            CelLModel.WallModelSet -= OnWallModelSet;
            CelLModel.WallModelRemoved -= OnWallModelRemoved;
            CelLModel.TowerModelSet -= OnTowerModelSet;
            CelLModel.TowerModelRemoved -= OnTowerModelRemoved;
            CelLModel.TowerModelConfirmed -= OnTowerModelConfirmed;
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
            CheckpointView = GameFactoryService.FieldFactory.CreateCheckpointView(CelLModel.CheckPointModel, transform);
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
            WallView = GameFactoryService.FieldFactory.CreateWallView(CelLModel.WallModel, transform);
        }

        private void OnTowerModelSet()
        {
            TowerView = GameFactoryService.FieldFactory.CreateTowerView(CelLModel.TowerModel, transform);
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