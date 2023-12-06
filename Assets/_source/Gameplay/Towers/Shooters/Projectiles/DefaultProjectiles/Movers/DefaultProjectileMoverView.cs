using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultProjectileMoverView : MonoBehaviour
    {
        private IProjectileMoverModel _moverModel;
        
        private Rigidbody Rigidbody { get; set; }

        public void Init(IProjectileMoverModel moverModel, Transform target)
        {
            _moverModel = moverModel;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _moverModel.Target.position, _moverModel.Speed * Time.fixedDeltaTime));
            _moverModel.Move(Rigidbody.position);
        }
    }
}