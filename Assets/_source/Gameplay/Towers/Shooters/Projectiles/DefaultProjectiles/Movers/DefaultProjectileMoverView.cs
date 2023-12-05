using Gameplay.Towers.Shooters.Projectiles.Movers;
using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultProjectileMoverView : MonoBehaviour
    {
        private IProjectileMoverModel _moverModel;
        public Rigidbody Rigidbody { get; private set; }

        public void Init(IProjectileMoverModel moverModel, Transform target)
        {
            _moverModel = moverModel;

            _moverModel.Transform = transform;
            _moverModel.Rigidbody = Rigidbody;
            _moverModel.Target = target;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _moverModel.Move();
        }
    }
}