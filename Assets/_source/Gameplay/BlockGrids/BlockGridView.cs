using System.Collections.Generic;
using System.Linq;
using Gameplay.BlockGrids.Cells;
using Gameplay.BlockGrids.CellsContainers;
using Gameplay.BlockGrids.Checkpoints;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.BlockGrids
{
    public class BlockGridView : MonoBehaviour
    {
        private BlockGridData _blockGridData;
        private CellView[] _cellViews;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public CellsContainer CellsContainer { get; private set; }

        private void Awake()
        {
            CellsContainer = GetComponentInChildren<CellsContainer>();
        }

        public void Init(BlockGridData blockGridData)
        {
            _blockGridData = blockGridData;
            _cellViews = _blockGridData.CellDatas.Select(cellData => GameFactoryService.BlockGridFactory.CreateCellView(cellData, CellsContainer.transform)).ToArray();
        }
    }
}