using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
   //created buildings
   public List<GameObject> placedObjects = new();

   /// <summary>
   /// Create a new building
   /// </summary>
   /// <param name="prefab"></param>
   /// <param name="position"></param>
   /// <param name="id"></param>
   /// <param name="positions"></param>
   /// <returns></returns>
   public int PlaceObject(GameObject prefab, Vector2 position,int id,List<Vector2Int> positions)
   {
      GameObject newObject =
         PoolManager.Instance.GetObject(GameManager.Instance.ObjectDataSO.objectsData.Find(x => x.ID == id)
            .PoolObjectType);//Instantiate(prefab);
      newObject.transform.position = position;
      Vector2Int pos = new Vector2Int((int)position.x, (int)position.y);
      newObject.GetComponent<Constructable>().ConstructableWasPlaced(id,positions,pos);
      placedObjects.Add(newObject);
      EventBus.TriggerEvent(EventName.ObjectPlacer);
      return placedObjects.Count - 1;
   }
   
}
