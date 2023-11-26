using Gameplay.BlockGrids.Cells;
using UnityEngine;

namespace Gameplay.BlockGrids.CellsContainers
{
    public class CellsContainer : MonoBehaviour
    {
        private CellView[] _cellViews;

        public void Init(CellView[] cellViews)
        {
            _cellViews = cellViews;
        }
    }
}
