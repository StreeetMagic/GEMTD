using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Cells.CellsContainers;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields
{
    public class FieldView : MonoBehaviour
    {
        private FieldData _fieldData;

        public CellView[] CellViews { get; private set; }
        private CellsContainer CellsContainer { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        private void Awake()
        {
            CellsContainer = GetComponentInChildren<CellsContainer>();
        }

        public void Init(FieldData fieldData)
        {
            _fieldData = fieldData;
            CellViews = _fieldData.CellDatas.Select(cellData => GameFactoryService.FieldFactory.CreateCellView(cellData, CellsContainer.transform)).ToArray();
        }
    }
}