using Gameplay.Fields.Cells.Blocks;
using Gameplay.Fields.Cells.Checkpoints;
using Gameplay.Fields.Cells.Towers;
using Gameplay.Fields.Cells.Walls;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Fields.Cells
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        public CellData CelLData { get; private set; }
        public BlockView BlockView { get; private set; }
        public CheckpointView CheckpointView { get; private set; }
        public WallView WallView { get; private set; }
        public TowerView TowerView { get; private set; }
        public bool IsPainted { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Init(CellData cellData)
        {
            CelLData = cellData;
            BlockView = GameFactoryService.FieldFactory.CreateBlockView(CelLData.BlockData, transform);
            Subscribe();
        }

        public void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            CelLData.ChekpointDataSet += OnCheckpointDataSet;
            CelLData.WallDataSet += OnWallDataSet;
            CelLData.WallDataRemoved += OnWallDataRemoved;
            CelLData.TowerDataSet += OnTowerDataSet;
            CelLData.TowerDataRemoved += OnTowerDataRemoved;
            CelLData.TowerConfirmed += OnTowerConfirmed; 
        }

        private void OnTowerConfirmed()
        {
            TowerView.ReduceScale();
        }

        private void Unsubscribe()
        {
            CelLData.ChekpointDataSet -= OnCheckpointDataSet;
            CelLData.WallDataSet -= OnWallDataSet;
            CelLData.WallDataRemoved -= OnWallDataRemoved;
            CelLData.TowerDataSet -= OnTowerDataSet;
            CelLData.TowerDataRemoved -= OnTowerDataRemoved;
            CelLData.TowerConfirmed -= OnTowerConfirmed;
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
            CheckpointView = GameFactoryService.FieldFactory.CreateCheckpointView(CelLData.CheckpointData, transform);
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
            WallView = GameFactoryService.FieldFactory.CreateWallView(CelLData.WallData, transform);
        }

        private void OnTowerDataSet()
        {
            TowerView = GameFactoryService.FieldFactory.CreateTowerView(CelLData.TowerData, transform);
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