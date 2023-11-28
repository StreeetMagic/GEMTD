using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Walls;
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

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Init(CellData cellData)
        {
            CelLData = cellData;
            BlockView = GameFactoryService.BlockGridFactory.CreateBlockView(CelLData.BlockData, transform);
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
        }

        private void Unsubscribe()
        {
            CelLData.ChekpointDataSet -= OnCheckpointDataSet;
            CelLData.WallDataSet -= OnWallDataSet;
            CelLData.WallDataRemoved -= OnWallDataRemoved;
        }

        private void OnCheckpointDataSet()
        {
            CheckpointView = GameFactoryService.BlockGridFactory.CreateCheckpointView(CelLData.CheckpointData, transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(name);
        }

        public void UnHighlight()
        {
            BlockView.UnHighlight();
        }

        public void Highlight(Material material)
        {
            BlockView.Highlight(material);
        }

        private void OnWallDataSet()
        {
            WallView = GameFactoryService.BlockGridFactory.CreateWallView(CelLData.WallData, transform);
        }

        private void OnWallDataRemoved()
        {
            Destroy(WallView.gameObject);
            WallView = null;
        }
    }
}