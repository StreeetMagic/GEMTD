using Gameplay.Towers.Shooters.Projectiles.Movers;
using UnityEngine;

namespace Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
    [RequireComponent(typeof(Rigidbody))]
    public class DefaultProjectileMoverView : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private IProjectileMoverModel _moverModel;
        public Rigidbody Rigidbody { get; private set; }

        public void Init(IProjectileMoverModel moverModel)
        {
            _moverModel = moverModel;

            _moverModel.Transform = transform;
            _moverModel.Rigidbody = Rigidbody;
            _moverModel.Target = _target;
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