using UnityEngine;

namespace Gameplay.Enemies
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
    public class EnemyView : MonoBehaviour
    {
        public EnemyModel EnemyModel { get; set; } = new EnemyModel();
    }
}