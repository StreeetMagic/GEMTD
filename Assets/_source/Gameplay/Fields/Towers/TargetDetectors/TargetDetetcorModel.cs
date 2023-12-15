using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.Towers.Shooters;
using UnityEngine;

namespace Gameplay.Fields.Towers.TargetDetectors
{
    public class TargetDetetcorModel
    {
        private IShooter _shooter;

        public TargetDetetcorModel(IShooter shooter)
        {
            _shooter = shooter;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyView view))
            {
                _shooter.AddTarget(view.EnemyModel);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyView view))
            {
                if (_shooter.Targets.Contains(view.EnemyModel))
                {
                    _shooter.RemoveTarget(view.EnemyModel);
                }
            }
        }
    }
}