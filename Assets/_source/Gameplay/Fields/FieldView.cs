using System.Linq;
using Gameplay.Fields.CellsContainers;
using Gameplay.Fields.EnemySpawners;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields
{
    public class FieldView : MonoBehaviour
    {
        private FieldModel _fieldModel;

        private CellsContainerView CellsContainerView { get; set; }
        private EnemySpawnerView EnemySpawnerView { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        private void Awake()
        {
            CellsContainerView = GetComponentInChildren<CellsContainerView>();
            EnemySpawnerView = GetComponentInChildren<EnemySpawnerView>();
        }

        public void Init(FieldModel fieldModel)
        {
            _fieldModel = fieldModel;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _fieldModel.CellsContainerModel.CellModels.Select(cellData => GameFactoryService.FieldFactory.CreateCellView(cellData, CellsContainerView.transform)).ToArray();
            EnemySpawnerView.Init(_fieldModel.EnemySpawnerModel);
        }
    }
}