using System.Collections.Generic;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemySpawners.Enemies.Movers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners.Enemies
{
    public class EnemyModel
    {
        public ReactiveProperty<float> Health { get; } = new ReactiveProperty<float>();
        public EnemyMoverModel MoverModel { get; set; }

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public EnemyModel(Vector3 position)
        {
            List<Vector3> checkPoints = GetCheckPoints();

            MoverModel = new EnemyMoverModel(position, checkPoints.ToArray());
        }

        private List<Vector3> GetCheckPoints()
        {
            CheckPointModel[] positions = CurrentDataService.FieldModel.CellsContainerModel.GetCheckPointModels();

            List<Vector3> checkPoints = new();

            foreach (var positionModel in positions)
            {
                checkPoints.Add(positionModel.CellModel.Position);
            }

            Debug.Log(checkPoints.Count);
            return checkPoints;
        }

        public void TakeDamage(float damage)
        {
            Health.Value -= damage;
            Debug.Log("Получил урон: " + damage);
            Debug.Log("Осталось здоровья: " + Health.Value);
        }
    }
}