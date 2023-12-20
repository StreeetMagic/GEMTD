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
        public CellModel CellModel { get; private set; }
        public BlockView BlockView { get; private set; }
        public CheckpointView CheckpointView { get; private set; }
        public WallView WallView { get; private set; }
        public TowerView TowerView { get; private set; }

        public bool IsPainted { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Init(CellModel cellModel)
        {
            CellModel = cellModel;
            BlockView = GameFactoryService.FieldFactory.CreateBlockView(CellModel.BlockModel, transform);
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
            CheckpointView = GameFactoryService.FieldFactory.CreateCheckpointView(CellModel.CheckPointModel, transform);
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
            WallView = GameFactoryService.FieldFactory.CreateWallView(CellModel.WallModel, transform);
        }

        private void OnTowerModelSet()
        {
            TowerView = GameFactoryService.FieldFactory.CreateTowerView(CellModel.TowerModel, transform);
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