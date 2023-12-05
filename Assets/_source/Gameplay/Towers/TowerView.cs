using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        public TowerModel TowerModel { get; private set; }
        public Material Material { get; private set; }

        public void Init(TowerModel towerModel, Material material)
        {
            TowerModel = towerModel;
            Material = material;

            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material = Material;
        }

        public void ReduceScale()
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
}