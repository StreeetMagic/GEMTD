using System;

namespace Gameplay.Fields.Blocks
{
  public class BlockModel
  {
    public bool IsPainted { get; set; }
    public event Action Painted;
    public event Action UnPainted;

    public void Paint()
    {
      IsPainted = true;
      Painted?.Invoke();
    }

    public void UnPaint()
    {
      IsPainted = false;
      UnPainted?.Invoke();
    }
  }
}