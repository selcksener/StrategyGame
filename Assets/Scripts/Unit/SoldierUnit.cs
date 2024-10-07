using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : Constructable
{
    
   [SerializeField] private GameObject spawnMarker;
   [SerializeField] private Vector2Int spawnMarkerGridPosition;
   public override void ConstructableWasPlaced(int id,List<Vector2Int> positions,Vector2Int currentPosition)
   {
      base.ConstructableWasPlaced(id,positions,currentPosition);
      //set the  position of the marker
      spawnMarker.transform.position = transform.position;

      //find a suitable cell and set the marker
      Collider2D[] colliders =   Physics2D.OverlapBoxAll(transform.position, transform.localScale * 2,0f,LayerMask.GetMask("Ground"));
      for (int i = 0; i < colliders.Length; i++)
      {
          if (colliders[i].TryGetComponent(out Cell cell))
          {
              if (cell.isAvailable)
              {
                  spawnMarker.transform.position = cell.transform.position+new Vector3(cell.cellImage.bounds.size.x/2,cell.cellImage.bounds.size.y/2,0f);
                  spawnMarkerGridPosition = new Vector2Int(cell.x, cell.y);
              }
          }
      }
   }
   
    /// <summary>
    /// Activating and deactivating the marker on which the building is selected
    /// </summary>
    /// <param name="state">true?active/false?deactive</param>
   public override void SelectDeselectBuilding(bool state)
   {
       spawnMarker.SetActive(state);
   }
    /// <summary>
    /// Changing the position of the marker by  mouse
    /// </summary>
    /// <param name="newCell"></param>
   public override void ChangeSpawnMarker(Cell newCell)
   {
       spawnMarker.transform.position = newCell.transform.position+new Vector3(newCell.cellImage.bounds.size.x/2,newCell.cellImage.bounds.size.y/2,0f);
       spawnMarkerGridPosition = new Vector2Int(newCell.x, newCell.y);
   }
    public override void ChangeUnitPosition(GameObject newUnit)
    {
        newUnit.transform.position = spawnMarker.transform.position;
    }
}
