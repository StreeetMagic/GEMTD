using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultProjectileMoverView : MonoBehaviour
    {
        private IProjectileMoverModel _moverModel;

        private Rigidbody Rigidbody { get; set; }

        public void Init(IProjectileMoverModel moverModel)
        {
            _moverModel = moverModel;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 current = transform.position;
            Vector3 target = _moverModel.Target.DamagePosition;
            float maxDistanceDelta = _moverModel.Speed * Time.fixedDeltaTime;
            
            Rigidbody.MovePosition(Vector3.MoveTowards(current, target, maxDistanceDelta));
            
            _moverModel.Move(Rigidbody.position);
        }
    }
}