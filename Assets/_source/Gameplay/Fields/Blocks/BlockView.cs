using Games;
using UnityEngine;

namespace Gameplay.Fields.Blocks
{
  public class BlockView : MonoBehaviour
  {
    private Material _defaultMaterial;
    private Material _paintedMaterial;
    private Renderer _renderer;

    public BlockModel BlockModel { get; private set; }

    private void Awake()
    {
      _renderer = GetComponentInChildren<Renderer>();
      _defaultMaterial = _renderer.material;
      _paintedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Painted);
    }

    public void OnDestroy()
    {
      BlockModel.Painted -= OnBlockPainted;
      BlockModel.UnPainted -= OnBlockUnPainted;
    }

    public void Init(BlockModel blockModel)
    {
      BlockModel = blockModel;

      BlockModel.Painted += OnBlockPainted;
      BlockModel.UnPainted += OnBlockUnPainted;
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