using System.Collections.Generic;
using Games;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
    public class TowersConfig : IStaticData
    {
        public class TowerValues
        {
            public TowerType Type;
            public TowerType SingleMergeType;
            public TowerType DoubleMergeType;
            public int Level;
            public Material Material;
        }

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
                new TowerValues()
                    .With(e => e.Type = TowerType.B1)
                    .With(e => e.SingleMergeType = TowerType.B2)
                    .With(e => e.DoubleMergeType = TowerType.B3)
                    .With(e => e.Material = bMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.B2)
                    .With(e => e.SingleMergeType = TowerType.B3)
                    .With(e => e.DoubleMergeType = TowerType.B4)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.B3)
                    .With(e => e.SingleMergeType = TowerType.B4)
                    .With(e => e.DoubleMergeType = TowerType.B5)
                    .With(e => e.Material = bMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.B4)
                    .With(e => e.SingleMergeType = TowerType.B5)
                    .With(e => e.DoubleMergeType = TowerType.B6)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.B5)
                    .With(e => e.SingleMergeType = TowerType.B6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = bMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.B6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = bMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.D1)
                    .With(e => e.SingleMergeType = TowerType.D2)
                    .With(e => e.DoubleMergeType = TowerType.D3)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.D2)
                    .With(e => e.SingleMergeType = TowerType.D3)
                    .With(e => e.DoubleMergeType = TowerType.D4)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.D3)
                    .With(e => e.SingleMergeType = TowerType.D4)
                    .With(e => e.DoubleMergeType = TowerType.D5)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.D4)
                    .With(e => e.SingleMergeType = TowerType.D5)
                    .With(e => e.DoubleMergeType = TowerType.D6)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.D5)
                    .With(e => e.SingleMergeType = TowerType.D6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.D6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = dMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y1)
                    .With(e => e.SingleMergeType = TowerType.Y2)
                    .With(e => e.DoubleMergeType = TowerType.Y3)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y2)
                    .With(e => e.SingleMergeType = TowerType.Y3)
                    .With(e => e.DoubleMergeType = TowerType.Y4)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y3)
                    .With(e => e.SingleMergeType = TowerType.Y4)
                    .With(e => e.DoubleMergeType = TowerType.Y5)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y4)
                    .With(e => e.SingleMergeType = TowerType.Y5)
                    .With(e => e.DoubleMergeType = TowerType.Y6)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y5)
                    .With(e => e.SingleMergeType = TowerType.Y6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.Y6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = yMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.E1)
                    .With(e => e.SingleMergeType = TowerType.E2)
                    .With(e => e.DoubleMergeType = TowerType.E3)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.E2)
                    .With(e => e.SingleMergeType = TowerType.E3)
                    .With(e => e.DoubleMergeType = TowerType.E4)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.E3)
                    .With(e => e.SingleMergeType = TowerType.E4)
                    .With(e => e.DoubleMergeType = TowerType.E5)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.E4)
                    .With(e => e.SingleMergeType = TowerType.E5)
                    .With(e => e.DoubleMergeType = TowerType.E6)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.E5)
                    .With(e => e.SingleMergeType = TowerType.E6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.E6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = eMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.G1)
                    .With(e => e.SingleMergeType = TowerType.G2)
                    .With(e => e.DoubleMergeType = TowerType.G3)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.G2)
                    .With(e => e.SingleMergeType = TowerType.G3)
                    .With(e => e.DoubleMergeType = TowerType.G4)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.G3)
                    .With(e => e.SingleMergeType = TowerType.G4)
                    .With(e => e.DoubleMergeType = TowerType.G5)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.G4)
                    .With(e => e.SingleMergeType = TowerType.G5)
                    .With(e => e.DoubleMergeType = TowerType.G6)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.G5)
                    .With(e => e.SingleMergeType = TowerType.G6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.G6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = gMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q1)
                    .With(e => e.SingleMergeType = TowerType.Q2)
                    .With(e => e.DoubleMergeType = TowerType.Q3)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q2)
                    .With(e => e.SingleMergeType = TowerType.Q3)
                    .With(e => e.DoubleMergeType = TowerType.Q4)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q3)
                    .With(e => e.SingleMergeType = TowerType.Q4)
                    .With(e => e.DoubleMergeType = TowerType.Q5)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q4)
                    .With(e => e.SingleMergeType = TowerType.Q5)
                    .With(e => e.DoubleMergeType = TowerType.Q6)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q5)
                    .With(e => e.SingleMergeType = TowerType.Q6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.Q6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = qMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.R1)
                    .With(e => e.SingleMergeType = TowerType.R2)
                    .With(e => e.DoubleMergeType = TowerType.R3)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.R2)
                    .With(e => e.SingleMergeType = TowerType.R3)
                    .With(e => e.DoubleMergeType = TowerType.R4)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.R3)
                    .With(e => e.SingleMergeType = TowerType.R4)
                    .With(e => e.DoubleMergeType = TowerType.R5)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.R4)
                    .With(e => e.SingleMergeType = TowerType.R5)
                    .With(e => e.DoubleMergeType = TowerType.R6)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.R5)
                    .With(e => e.SingleMergeType = TowerType.R6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.R6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = rMaterial)
                    .With(e => e.Level = 6),

                new TowerValues()
                    .With(e => e.Type = TowerType.P1)
                    .With(e => e.SingleMergeType = TowerType.P2)
                    .With(e => e.DoubleMergeType = TowerType.P3)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 1),

                new TowerValues()
                    .With(e => e.Type = TowerType.P2)
                    .With(e => e.SingleMergeType = TowerType.P3)
                    .With(e => e.DoubleMergeType = TowerType.P4)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 2),

                new TowerValues()
                    .With(e => e.Type = TowerType.P3)
                    .With(e => e.SingleMergeType = TowerType.P4)
                    .With(e => e.DoubleMergeType = TowerType.P5)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 3),

                new TowerValues()
                    .With(e => e.Type = TowerType.P4)
                    .With(e => e.SingleMergeType = TowerType.P5)
                    .With(e => e.DoubleMergeType = TowerType.P6)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 4),

                new TowerValues()
                    .With(e => e.Type = TowerType.P5)
                    .With(e => e.SingleMergeType = TowerType.P6)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 5),

                new TowerValues()
                    .With(e => e.Type = TowerType.P6)
                    .With(e => e.SingleMergeType = TowerType.None)
                    .With(e => e.DoubleMergeType = TowerType.None)
                    .With(e => e.Material = pMaterial)
                    .With(e => e.Level = 6)
            };
        }

        public Material GetMaterial(TowerType towerModelType) =>
            _towerValues.Find(x => x.Type == towerModelType).Material;

        public Vector3 GetScale(int level) =>
            new(0.8f, 1 + (0.4f * (level - 1)), 0.8f);

        public TowerValues GetTowerValues(TowerType towerModelType) =>
            _towerValues.Find(x => x.Type == towerModelType);
    }
}