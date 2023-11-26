using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsContainer : MonoBehaviour
{
    private CellView[] _cellViews;

    public void Init(CellView[] cellViews)
    {
        _cellViews = cellViews;
    }
}
