using Gameplay.Enemies;
using Gameplay.Towers.Shooters;
using UnityEngine;

namespace Gameplay.Towers.TargetDetectors
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
                Debug.Log("Чую противника");
                _shooter.Targets.Push(other.transform);
            }
        }
    }
}