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
            Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _moverModel.Target.MoverModel.Position, _moverModel.Speed * Time.fixedDeltaTime));
            _moverModel.Move(Rigidbody.position);
        }
    }
}