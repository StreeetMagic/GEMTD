using System;
using System.Collections.Generic;
using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
    public class TowersConfig : IStaticData
    {
        private readonly List<TowerValues> _towerValues;

        private IAssetProviderService AssetProviderService => ServiceLocator.Instance.Get<IAssetProviderService>();

        public TowersConfig()
        {
            var bMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.BMaterial);
            var dMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.DMaterial);
            var eMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.EMaterial);
            var gMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.GMaterial);
            var pMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.PMaterial);
            var qMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.QMaterial);
            var rMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.RMaterial);
            var yMaterial = AssetProviderService.Get<Material>(Constants.AssetsPath.Materials.YMaterial);

            _towerValues = new List<TowerValues>()
            {
                new TowerValues(TowerType.B1, bMaterial, 1, TowerType.B2, TowerType.B3),
                new TowerValues(TowerType.B2, bMaterial, 2, TowerType.B3, TowerType.B4),
                new TowerValues(TowerType.B3, bMaterial, 3, TowerType.B4, TowerType.B5),
                new TowerValues(TowerType.B4, bMaterial, 4, TowerType.B5, TowerType.B6),
                new TowerValues(TowerType.B5, bMaterial, 5, TowerType.B6),
                new TowerValues(TowerType.B6, bMaterial, 6),

                new TowerValues(TowerType.D1, dMaterial, 1, TowerType.D2, TowerType.D3),
                new TowerValues(TowerType.D2, dMaterial, 2, TowerType.D3, TowerType.D4),
                new TowerValues(TowerType.D3, dMaterial, 3, TowerType.D4, TowerType.D5),
                new TowerValues(TowerType.D4, dMaterial, 4, TowerType.D5, TowerType.D6),
                new TowerValues(TowerType.D5, dMaterial, 5, TowerType.D6),
                new TowerValues(TowerType.D6, dMaterial, 6),

                new TowerValues(TowerType.E1, eMaterial, 1, TowerType.E2, TowerType.E3),
                new TowerValues(TowerType.E2, eMaterial, 2, TowerType.E3, TowerType.E4),
                new TowerValues(TowerType.E3, eMaterial, 3, TowerType.E4, TowerType.E5),
                new TowerValues(TowerType.E4, eMaterial, 4, TowerType.E5, TowerType.E6),
                new TowerValues(TowerType.E5, eMaterial, 5, TowerType.E6),
                new TowerValues(TowerType.E6, eMaterial, 6),

                new TowerValues(TowerType.G1, gMaterial, 1, TowerType.G2, TowerType.G3),
                new TowerValues(TowerType.G2, gMaterial, 2, TowerType.G3, TowerType.G4),
                new TowerValues(TowerType.G3, gMaterial, 3, TowerType.G4, TowerType.G5),
                new TowerValues(TowerType.G4, gMaterial, 4, TowerType.G5, TowerType.G6),
                new TowerValues(TowerType.G5, gMaterial, 5, TowerType.G6),
                new TowerValues(TowerType.G6, gMaterial, 6),

                new TowerValues(TowerType.P1, pMaterial, 1, TowerType.P2, TowerType.P3),
                new TowerValues(TowerType.P2, pMaterial, 2, TowerType.P3, TowerType.P4),
                new TowerValues(TowerType.P3, pMaterial, 3, TowerType.P4, TowerType.P5),
                new TowerValues(TowerType.P4, pMaterial, 4, TowerType.P5, TowerType.P6),
                new TowerValues(TowerType.P5, pMaterial, 5, TowerType.P6),
                new TowerValues(TowerType.P6, pMaterial, 6),

                new TowerValues(TowerType.Q1, qMaterial, 1, TowerType.Q2, TowerType.Q3),
                new TowerValues(TowerType.Q2, qMaterial, 2, TowerType.Q3, TowerType.Q4),
                new TowerValues(TowerType.Q3, qMaterial, 3, TowerType.Q4, TowerType.Q5),
                new TowerValues(TowerType.Q4, qMaterial, 4, TowerType.Q5, TowerType.Q6),
                new TowerValues(TowerType.Q5, qMaterial, 5, TowerType.Q6),
                new TowerValues(TowerType.Q6, qMaterial, 6),

                new TowerValues(TowerType.R1, rMaterial, 1, TowerType.R2, TowerType.R3),
                new TowerValues(TowerType.R2, rMaterial, 2, TowerType.R3, TowerType.R4),
                new TowerValues(TowerType.R3, rMaterial, 3, TowerType.R4, TowerType.R5),
                new TowerValues(TowerType.R4, rMaterial, 4, TowerType.R5, TowerType.R6),
                new TowerValues(TowerType.R5, rMaterial, 5, TowerType.R6),
                new TowerValues(TowerType.R6, rMaterial, 6),

                new TowerValues(TowerType.Y1, yMaterial, 1, TowerType.Y2, TowerType.Y3),
                new TowerValues(TowerType.Y2, yMaterial, 2, TowerType.Y3, TowerType.Y4),
                new TowerValues(TowerType.Y3, yMaterial, 3, TowerType.Y4, TowerType.Y5),
                new TowerValues(TowerType.Y4, yMaterial, 4, TowerType.Y5, TowerType.Y6),
                new TowerValues(TowerType.Y5, yMaterial, 5, TowerType.Y6),
                new TowerValues(TowerType.Y6, yMaterial, 6),
            };
        }

        public Material GetMaterial(TowerType towerModelType) =>
            _towerValues.Find(x => x.Type == towerModelType).Material;

        public Vector3 GetScale(int level) =>
            new(0.8f, 1 + (0.4f * (level - 1)), 0.8f);
        
        public TowerValues GetTowerValues(TowerType towerModelType) =>
            _towerValues.Find(x => x.Type == towerModelType);
    }

    [Serializable]
    public class TowerValues
    {
        public TowerType Type;
        public Material Material;
        public int Level;
        public TowerType SingleMergeType;
        public TowerType DoubleMergeType;

        public TowerValues(TowerType type, Material material, int level, TowerType singleMergeType = TowerType.None, TowerType doubleMergeType = TowerType.None)
        {
            Type = type;
            Material = material;
            Level = level;
            SingleMergeType = singleMergeType;
            DoubleMergeType = doubleMergeType;
        }
    }
}