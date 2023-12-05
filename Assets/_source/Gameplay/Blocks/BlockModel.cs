using System;

namespace Gameplay.Blocks
{
    public class BlockModel
    {
        public event Action Painted;
        public event Action UnPainted;
        
        public bool IsPainted { get; set; }

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