using Gameplay.Blocks;
using Gameplay.Checkpoints;
using Gameplay.Towers;
using Gameplay.Walls;
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
            CelLModel.ChekpointDataSet += OnCheckpointDataSet;
            CelLModel.WallDataSet += OnWallDataSet;
            CelLModel.WallDataRemoved += OnWallDataRemoved;
            CelLModel.TowerDataSet += OnTowerDataSet;
            CelLModel.TowerDataRemoved += OnTowerDataRemoved;
            CelLModel.TowerConfirmed += OnTowerConfirmed; 
        }

        private void OnTowerConfirmed()
        {
            TowerView.ReduceScale();
        }

        private void Unsubscribe()
        {
            CelLModel.ChekpointDataSet -= OnCheckpointDataSet;
            CelLModel.WallDataSet -= OnWallDataSet;
            CelLModel.WallDataRemoved -= OnWallDataRemoved;
            CelLModel.TowerDataSet -= OnTowerDataSet;
            CelLModel.TowerDataRemoved -= OnTowerDataRemoved;
            CelLModel.TowerConfirmed -= OnTowerConfirmed;
        }

        private void OnTowerDataRemoved()
        {
            if (TowerView == null)
            {
                return;
            }
            
            Destroy(TowerView.gameObject);
            TowerView = null;
        }

        private void OnCheckpointDataSet()
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

        private void OnWallDataSet()
        {
            WallView = GameFactoryService.FieldFactory.CreateWallView(CelLModel.WallData, transform);
        }

        private void OnTowerDataSet()
        {
            TowerView = GameFactoryService.FieldFactory.CreateTowerView(CelLModel.TowerModel, transform);
        }

        private void OnWallDataRemoved()
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