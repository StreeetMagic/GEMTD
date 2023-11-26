using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private BlockView _block;

    public void Init(BlockView block)
    {
        _block = block;
    }
}
