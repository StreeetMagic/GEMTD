using UnityEngine;

namespace Gameplay.Walls
{
    public class WallView : MonoBehaviour
    {
        public WallModel WallModel { get; private set; }
        
        public void Init(WallModel wallModel)
        {
            WallModel = wallModel; 
        }
    }
}