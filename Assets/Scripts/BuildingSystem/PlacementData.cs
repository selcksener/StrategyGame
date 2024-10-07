using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
   public List<Vector2Int> occupiedPositions;
   public int ID { get;private set; }
   public int PlacedObjectIndex { get; private set; }
   /// <summary>
   /// info of the created building
   /// </summary>
   /// <param name="occupiedPositions"></param>
   /// <param name="id"></param>
   /// <param name="placedObjectIndex"></param>
   public PlacementData(List<Vector2Int> occupiedPositions, int id, int placedObjectIndex)
   {
      this.occupiedPositions = occupiedPositions;
      ID = id;
      PlacedObjectIndex = placedObjectIndex;
   }
}
