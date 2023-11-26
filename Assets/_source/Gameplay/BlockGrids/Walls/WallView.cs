using Gameplay.BlockGrids.Walls;
using UnityEngine;

namespace Gameplay.BlockGrids.Cells
{
    public class WallView : MonoBehaviour
    {
        public WallData WallData { get; private set; }
        
        public void Init(WallData wallData)
        {
            WallData = wallData; 
        }
    }
}