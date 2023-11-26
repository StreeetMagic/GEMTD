using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

public class BlockGridView : MonoBehaviour
{
    private BlockGridData _blockGridData;

    private void Awake()
    {
        CellsContainer = GetComponentInChildren<CellsContainer>();
    }

    public void Init(BlockGridData blockGridData, CellView[] cellViews)
    {
        _blockGridData = blockGridData;
        CellsContainer.Init(cellViews);
    }

    public CellsContainer CellsContainer { get; private set; }
}