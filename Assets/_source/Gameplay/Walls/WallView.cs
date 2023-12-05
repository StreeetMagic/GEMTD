using UnityEngine;

namespace Gameplay.Walls
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