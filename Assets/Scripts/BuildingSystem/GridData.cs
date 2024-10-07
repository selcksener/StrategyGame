using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    //data of the placed objects
    public Dictionary<Vector2Int, PlacementData> placedObjects = new Dictionary<Vector2Int, PlacementData>();

    /// <summary>
    /// Add a new building
    /// </summary>
    /// <param name="gridPosition">World position</param>
    /// <param name="objectSize">Size of the building</param>
    /// <param name="ID">Unique id of the building</param>
    /// <param name="placedObjectIndex">Index of the placed object</param>
    public void AddObjectAt(Vector2Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        //Get positions covered by the building
        List<Vector2Int> positionToOccuply = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccuply, ID, placedObjectIndex);
        foreach (var pos in positionToOccuply)
        {
            if (placedObjects.ContainsKey(pos))
            {
                Debug.LogError("Dictionary already contains this cell position");
                return;
            }

            placedObjects[pos] = data;
        }
    }

    /// <summary>
    /// Get positions according to building size
    /// </summary>
    /// <param name="gridPosition">world position</param>
    /// <param name="objectSize">size of the building</param>
    /// <returns></returns>
    public List<Vector2Int> CalculatePositions(Vector2Int gridPosition, Vector2Int objectSize)
    {
        List<Vector2Int> returnVal = new();
        for (int i = 0; i < objectSize.x; i++)
        {
            for (int j = 0; j < objectSize.y; j++)
            {
                returnVal.Add(gridPosition + new Vector2Int(i * 8, j * 8));
            }
        }

        return returnVal;
    }

    /// <summary>
    /// Can the building be placed?
    /// </summary>
    /// <param name="gridPosition">world position</param>
    /// <param name="objectSize">size of the building</param>
    /// <returns></returns>
    public bool CanPlaceObjectAt(Vector2Int gridPosition, Vector2Int objectSize)
    {
        List<Vector2Int> positionToOccpy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccpy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveObjectAt(Vector2Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}