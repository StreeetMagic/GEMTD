using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.CellsContainers;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields
{
    public class FieldView : MonoBehaviour
    {
        private FieldData _fieldData;
        private CellView[] _cellViews;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public CellsContainer CellsContainer { get; private set; }

        private void Awake()
        {
            CellsContainer = GetComponentInChildren<CellsContainer>();
        }

        public void Init(FieldData fieldData)
        {
            _fieldData = fieldData;
            _cellViews = _fieldData.CellDatas.Select(cellData => GameFactoryService.BlockGridFactory.CreateCellView(cellData, CellsContainer.transform)).ToArray();
        }
    }
}