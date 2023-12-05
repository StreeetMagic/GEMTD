using System;
using Gameplay.Towers.Shooters;
using Gameplay.Towers.TargetDetectors;
using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private SingleProjectileShooterView _shooterView;
        [SerializeField] private TargetDetetcorView _targetDetetcorView;

        private MeshRenderer _meshRenderer;
        public TowerModel TowerModel { get; private set; }
        public Material Material { get; private set; }

        public void Init(TowerModel towerModel, Material material)
        {
            TowerModel = towerModel;
            Material = material;

            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material = Material;

            _shooterView.Init(TowerModel.Shooter);
            _targetDetetcorView.Init(TowerModel.TargetDetetcor);
        }

        public void Awake()
        {
            var shooter = new SingleProjectileShooterModel();
            var targetDetector = new TargetDetetcorModel(shooter);
            var material = Resources.Load<Material>(Constants.AssetsPath.Materials.Highlighted);
            Init(new TowerModel(TowerType.B, 1, shooter, targetDetector), material);
        }

        public void ReduceScale()
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
}