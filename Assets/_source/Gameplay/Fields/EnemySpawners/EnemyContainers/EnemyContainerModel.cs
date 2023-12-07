using System.Collections.Generic;
using Gameplay.Fields.EnemySpawners.Enemies;

namespace Gameplay.Fields.EnemySpawners.EnemyContainers
{
    public class EnemyContainerModel
    {
        public List<EnemyModel> Enemies { get; } = new List<EnemyModel>();
    }
}