using System;
using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject, IStaticData
    {
        [field: SerializeField] public float SpeedCoefficient { get; private set; }
        [field: SerializeField] public float HealthPointsCoefficient { get; private set; }

        [SerializeField] private List<EnemyValues> _enemies;

        public IReadOnlyList<EnemyValues> Enemies => _enemies;
    }

    [Serializable]
    public struct EnemyValues
    {
        [Header(" ")]
        public int RoundNumber;
        public int HealthPoints;
        public int MovementSpeed;
    }
}