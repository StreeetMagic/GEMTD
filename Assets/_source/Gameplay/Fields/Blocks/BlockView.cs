using UnityEngine;

namespace Gameplay.Fields.Blocks
{
    public class BlockView : MonoBehaviour
    {
        private Material _defaultMaterial;
        private Renderer _renderer;

        public BlockData BlockData { get; private set; }

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _defaultMaterial = _renderer.material;
        }

        public void Init(BlockData blockData)
        {
            BlockData = blockData;
        }

        public void UnHighlight()
        {
            _renderer.material = _defaultMaterial;
        }

        public void Highlight(Material material)
        {
            _renderer.material = material;
        }
    }
}