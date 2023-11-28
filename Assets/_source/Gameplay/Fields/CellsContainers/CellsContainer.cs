using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.CellsContainers
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
