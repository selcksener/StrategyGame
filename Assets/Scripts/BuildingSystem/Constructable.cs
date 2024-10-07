using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Constructable : MonoBehaviour,IClickable
{

    public int ID;

    public Vector2Int currentPosition;
    //cells covered by the building
    public List<Vector2Int> positions = new List<Vector2Int>();
    
    /// <summary>
    /// when create a new unit
    /// </summary>
    /// <param name="id">unit's id</param>
    /// <param name="positions">cell positions</param>
    public virtual void ConstructableWasPlaced(int id,List<Vector2Int> positions,Vector2Int currentPosition)
    {
        ID = id;
        this.positions = positions;
        this.currentPosition = currentPosition;
        for (int i = 0; i < positions.Count; i++)
        {
            //Get the cell from the position
            Vector2Int cell = InputManager.Instance.GetSelectedCell(new Vector3(positions[i].x,positions[i].y,0f));
            GridSystem.Instance.cells[cell.x, cell.y].isAvailable = false;
        }
        GetComponent<BoxCollider2D>().enabled = true;
        
    }

    /// <summary>
    /// whether the building is selected
    /// </summary>
    /// <param name="state">true?selected/false?not selected</param>
    public virtual  void SelectDeselectBuilding(bool state)
    {
        
    }

    /// <summary>
    /// changing position of the building marker
    /// </summary>
    /// <param name="newCell"></param>
    public virtual void ChangeSpawnMarker(Cell newCell)
    {
       
    }

    public virtual void DestroyObject()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            //Get the cell from the position
            Vector2Int cell = InputManager.Instance.GetSelectedCell(new Vector3(positions[i].x, positions[i].y, 0f));
            GridSystem.Instance.cells[cell.x, cell.y].isAvailable = true;
        }
        PlacementSystem.DestroyObjectEvent?.Invoke(currentPosition,gameObject,ID);
    }
    public virtual void ChangeUnitPosition(GameObject unitObject)
    {
      
    }
}
