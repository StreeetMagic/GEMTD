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
            if (other.TryGetComponent(out EnemyView _))
            {
                _shooter.AddTarget(other.transform);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyView _))
            {
                if (_shooter.Targets.Contains(other.transform))
                {
                    _shooter.RemoveTarget(other.transform);
                }
            }
        }
    }
}