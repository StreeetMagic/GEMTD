using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.Towers.Resources
{
    public class TowerView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        public TowerData TowerData { get; private set; }
        public Material Material { get; private set; }

        public void Init(TowerData towerData, Material material)
        {
            TowerData = towerData;
            Material = material;

            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material = Material;
        }

        public void ReduceScale()
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }
}