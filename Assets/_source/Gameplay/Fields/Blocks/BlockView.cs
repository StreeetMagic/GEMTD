using Games;
using UnityEngine;

namespace Gameplay.Fields.Blocks
{
    [RequireComponent(typeof(Renderer))]
    public class BlockView : MonoBehaviour
    {
        private Material _defaultMaterial;
        private Material _paintedMaterial;
        private Renderer _renderer;

        public BlockData BlockData { get; private set; }

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _defaultMaterial = _renderer.material;
            _paintedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Painted);
        }

        public void OnDestroy()
        {
            BlockData.Painted -= OnBlockPainted;
            BlockData.UnPainted -= OnBlockUnPainted;
        }

        public void Init(BlockData blockData)
        {
            BlockData = blockData;

            BlockData.Painted += OnBlockPainted;
            BlockData.UnPainted += OnBlockUnPainted;
        }

        public void UnHighlight()
        {
            _renderer.material = _defaultMaterial;
        }

        public void Highlight(Material material)
        {
            _renderer.material = material;
        }

        public void PaintBlock(Material material)
        {
            _renderer.material = material;
        }

        private void OnBlockUnPainted()
        {
            PaintBlock(_defaultMaterial);
        }

        private void OnBlockPainted()
        {
            PaintBlock(_paintedMaterial);
        }
    }
}