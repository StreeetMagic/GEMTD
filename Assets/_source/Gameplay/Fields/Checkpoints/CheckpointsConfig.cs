using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Checkpoints
{
    public class CheckpointValues
    {
        public int Number;
        public Vector2Int Coordinates;
    }

    public class CheckpointsConfig : IStaticData
    {
        private readonly List<CheckpointValues> _checkpointValues;

        public IReadOnlyList<CheckpointValues> CheckpointValues => _checkpointValues;

        public CheckpointsConfig()
        {
            _checkpointValues = new List<CheckpointValues>()
            {
                new CheckpointValues()
                    .With(e => e.Number = 0)
                    .With(e => e.Coordinates = new Vector2Int(2, 15)),

                new CheckpointValues()
                    .With(e => e.Number = 1)
                    .With(e => e.Coordinates = new Vector2Int(2, 8)),

                new CheckpointValues()
                    .With(e => e.Number = 2)
                    .With(e => e.Coordinates = new Vector2Int(14, 8)),

                new CheckpointValues()
                    .With(e => e.Number = 3)
                    .With(e => e.Coordinates = new Vector2Int(14, 14)),

                new CheckpointValues()
                    .With(e => e.Number = 4)
                    .With(e => e.Coordinates = new Vector2Int(8, 14)),

                new CheckpointValues()
                    .With(e => e.Number = 5)
                    .With(e => e.Coordinates = new Vector2Int(8, 2)),

                new CheckpointValues()
                    .With(e => e.Number = 6)
                    .With(e => e.Coordinates = new Vector2Int(14, 2))
            };
        }
    }
}