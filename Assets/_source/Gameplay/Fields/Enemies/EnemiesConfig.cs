using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;

namespace Gameplay.Fields.Enemies
{
    public class EnemyValues
    {
        public int RoundNumber;
        public float HealthPoints;
        public float MovementSpeed;
    }

    public class EnemiesConfig : IStaticData
    {
        private List<EnemyValues> _enemies;

        public float DotaMoveSpeedMultiplier { get; private set; } = 0.008f;
        public float MoverSpeedMultiplier { get; private set; } = 0.6f;
        public float HealthPointsMultiplier { get; private set; } = 1.15f;

        public IReadOnlyList<EnemyValues> Enemies => _enemies;

        public EnemiesConfig()
        {
            _enemies = new List<EnemyValues>()
            {
                new EnemyValues()
                    .With(x => x.RoundNumber = 1)
                    .With(x => x.HealthPoints = 5)
                    .With(x => x.MovementSpeed = 525),

                new EnemyValues()
                    .With(x => x.RoundNumber = 2)
                    .With(x => x.HealthPoints = 10)
                    .With(x => x.MovementSpeed = 600),

                new EnemyValues()
                    .With(x => x.RoundNumber = 3)
                    .With(x => x.HealthPoints = 20)
                    .With(x => x.MovementSpeed = 525),

                new EnemyValues()
                    .With(x => x.RoundNumber = 4)
                    .With(x => x.HealthPoints = 20)
                    .With(x => x.MovementSpeed = 1000),

                new EnemyValues()
                    .With(x => x.RoundNumber = 5)
                    .With(x => x.HealthPoints = 24)
                    .With(x => x.MovementSpeed = 500),

                new EnemyValues()
                    .With(x => x.RoundNumber = 6)
                    .With(x => x.HealthPoints = 100)
                    .With(x => x.MovementSpeed = 450),

                new EnemyValues()
                    .With(x => x.RoundNumber = 7)
                    .With(x => x.HealthPoints = 100)
                    .With(x => x.MovementSpeed = 600),

                new EnemyValues()
                    .With(x => x.RoundNumber = 8)
                    .With(x => x.HealthPoints = 70)
                    .With(x => x.MovementSpeed = 750),

                new EnemyValues()
                    .With(x => x.RoundNumber = 9)
                    .With(x => x.HealthPoints = 120)
                    .With(x => x.MovementSpeed = 600),

                new EnemyValues()
                    .With(x => x.RoundNumber = 10)
                    .With(x => x.HealthPoints = 2100)
                    .With(x => x.MovementSpeed = 750),
            };
        }
    }
}