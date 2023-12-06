using UnityEngine;

namespace Gameplay.Towers.TargetDetectors
{
    [RequireComponent(typeof(Collider))]
    public class TargetDetetcorView : MonoBehaviour
    {
        private TargetDetetcorModel _targetDetetcorModel;

        public void Init(TargetDetetcorModel targetDetetcorModel)
        {
            _targetDetetcorModel = targetDetetcorModel;
        }

        private void OnTriggerEnter(Collider other)
        {
            _targetDetetcorModel.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _targetDetetcorModel.OnTriggerExit(other);
        }
    }
}