using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Node
{
    public SpriteRenderer cellImage;
    public Cell parent;

    /// <summary>
    /// updating the cell
    /// </summary>
    /// <param name="_index">Position</param>
    public void UpdateCell(Vector2Int _index)
    {
        SetNode(_index.x, _index.y, true);
        gameObject.name = $"Cell[{_index.x}-{_index.y}]";

    }
}