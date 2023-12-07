using System;
using System.Collections.Generic;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Enemies.Spawners
{
    public class EnemySpawnerModel
    {
        private CoroutineDecorator _spawningCoroutine;
        
        public EnemySpawnerModel(EnemyContainerModel containerModel)
        {
            ContainerModel = containerModel;
        }

        public EnemyContainerModel ContainerModel { get; }
        private CoroutineRunner CoroutineRunner => ServiceLocator.Instance.Get<CoroutineRunner>();
        
    }

    public class EnemyContainerModel
    {
        public List<EnemyModel> Enemies { get; } = new List<EnemyModel>();
    }

    public class EnemySpawnerView : MonoBehaviour
    {
        public EnemyContainerView ContainerView { get; set; }
        public EnemySpawnerModel Model { get; set; }

        public void Init(EnemySpawnerModel model)
        {
            Model = model;
        }

        private void Awake()
        {
            ContainerView = GetComponentInChildren<EnemyContainerView>();
        }
    }

    public class EnemyContainerView : MonoBehaviour
    {
    }
}