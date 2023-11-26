using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

public class BlockGridView : MonoBehaviour
{
    private BlockGridData _blockGridData;

    public void Init(BlockGridData blockGridData)
    {
        _blockGridData = blockGridData;
    }
}