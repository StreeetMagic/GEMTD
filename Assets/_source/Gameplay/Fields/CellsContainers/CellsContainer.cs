using System.Diagnostics.CodeAnalysis;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.CellsContainers
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class CellsContainer : MonoBehaviour
    {
        private CellView[] _cellViews;

        public void Init(CellView[] cellViews)
        {
            _cellViews = cellViews;
        }
    }
}
