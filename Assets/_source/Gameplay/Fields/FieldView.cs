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
        private FieldModel _fieldModel;

        public CellView[] CellViews { get; private set; }
        private CellsContainerModel CellsContainerModel { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        private void Awake()
        {
            CellsContainerModel = GetComponentInChildren<CellsContainerModel>();
        }

        public void Init(FieldModel fieldModel)
        {
            _fieldModel = fieldModel;
            CellViews = _fieldModel.CellDatas.Select(cellData => GameFactoryService.FieldFactory.CreateCellView(cellData, CellsContainerModel.transform)).ToArray();
        }
    }
}