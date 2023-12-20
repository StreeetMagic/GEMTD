using System;
using System.Collections.Generic;
using Games;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
    public class TowersConfig : IStaticData
    {
        public List<TowerValues> TowerValues;

        private Material BMaterial;
        private Material DMaterial;
        private Material EMaterial;
        private Material GMaterial;
        private Material PMaterial;
        private Material QMaterial;
        private Material RMaterial;
        private Material YMaterial;

        public TowersConfig()
        {
            BMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.BMaterial);
            DMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.DMaterial);
            EMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.EMaterial);
            GMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.GMaterial);
            PMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.PMaterial);
            QMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.QMaterial);
            RMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.RMaterial);
            YMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.YMaterial);

            TowerValues = new List<TowerValues>()
            {
                new TowerValues(TowerType.B1, BMaterial, 1),
                new TowerValues(TowerType.B2, BMaterial, 2),
                new TowerValues(TowerType.B3, BMaterial, 3),
                new TowerValues(TowerType.B4, BMaterial, 4),
                new TowerValues(TowerType.B5, BMaterial, 5),
                new TowerValues(TowerType.B6, BMaterial, 6),

                new TowerValues(TowerType.D1, DMaterial, 1),
                new TowerValues(TowerType.D2, DMaterial, 2),
                new TowerValues(TowerType.D3, DMaterial, 3),
                new TowerValues(TowerType.D4, DMaterial, 4),
                new TowerValues(TowerType.D5, DMaterial, 5),

                new TowerValues(TowerType.E1, EMaterial, 1),
                new TowerValues(TowerType.E2, EMaterial, 2),
                new TowerValues(TowerType.E3, EMaterial, 3),
                new TowerValues(TowerType.E4, EMaterial, 4),
                new TowerValues(TowerType.E5, EMaterial, 5),

                new TowerValues(TowerType.P1, PMaterial, 1),
                new TowerValues(TowerType.P2, PMaterial, 2),
                new TowerValues(TowerType.P3, PMaterial, 3),
                new TowerValues(TowerType.P4, PMaterial, 4),
                new TowerValues(TowerType.P5, PMaterial, 5),

                new TowerValues(TowerType.Q1, QMaterial, 1),
                new TowerValues(TowerType.Q2, QMaterial, 2),
                new TowerValues(TowerType.Q3, QMaterial, 3),
                new TowerValues(TowerType.Q4, QMaterial, 4),
                new TowerValues(TowerType.Q5, QMaterial, 5),

                new TowerValues(TowerType.R1, RMaterial, 1),
                new TowerValues(TowerType.R2, RMaterial, 2),
                new TowerValues(TowerType.R3, RMaterial, 3),
                new TowerValues(TowerType.R4, RMaterial, 4),
                new TowerValues(TowerType.R5, RMaterial, 5),

                new TowerValues(TowerType.Y1, YMaterial, 1),
                new TowerValues(TowerType.Y2, YMaterial, 2),
                new TowerValues(TowerType.Y3, YMaterial, 3),
                new TowerValues(TowerType.Y4, YMaterial, 4),
                new TowerValues(TowerType.Y5, YMaterial, 5),

                new TowerValues(TowerType.G1, GMaterial, 1),
                new TowerValues(TowerType.G2, GMaterial, 2),
                new TowerValues(TowerType.G3, GMaterial, 3),
                new TowerValues(TowerType.G4, GMaterial, 4),
                new TowerValues(TowerType.G5, GMaterial, 5),
            };
        }

        public Material GetMaterial(TowerType towerModelType)
        {
            return TowerValues.Find(x => x.Type == towerModelType).Material;
        }

        public Vector3 GetScale(int level) =>
            new(0.8f, 1 + (0.4f * (level - 1)), 0.8f);
    }

    [Serializable]
    public class TowerValues
    {
        public TowerType Type;
        public Material Material;
        public int Level;

        public TowerValues(TowerType type, Material material, int level)
        {
            Type = type;
            Material = material;
            Level = level;
        }
    }
}