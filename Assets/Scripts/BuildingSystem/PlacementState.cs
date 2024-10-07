using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{

    [SerializeField]private int selectedObjectIndex = 1;
    [SerializeField] private int ID;
    private Grid grid;
    private ObjectDataSO objectDataSO;
    private GridData gridData;
    private PreviewSystem previewSystem;
    private ObjectPlacer objectPlacer;
    /// <summary>
    /// create a new state
    /// </summary>
    /// <param name="id"></param>
    /// <param name="preview"></param>
    /// <param name="grid"></param>
    /// <param name="objectDataSO"></param>
    /// <param name="gridData"></param>
    /// <param name="objectPlacer"></param>
    public PlacementState(int id,PreviewSystem preview, Grid grid, ObjectDataSO objectDataSO,GridData gridData,ObjectPlacer objectPlacer)
    {
        this.ID = id;
        this.grid = grid;
        this.objectDataSO = objectDataSO;
        this.gridData = gridData;
        previewSystem = preview;
        this.objectPlacer = objectPlacer;
        selectedObjectIndex = objectDataSO.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            preview.StartShowingPreview(objectDataSO.objectsData[selectedObjectIndex].PoolObjectType,objectDataSO.objectsData[selectedObjectIndex].Size);
        }



    }
    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector2Int gridPosition)
    {
        //check if available
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            return;
        }
        GridData selectedData = gridData;
        // getting positions according to building size
        List<Vector2Int> placedPositions = selectedData.CalculatePositions(gridPosition, objectDataSO.objectsData[selectedObjectIndex].Size);
        // create a new building
        int index = objectPlacer.PlaceObject(objectDataSO.objectsData[selectedObjectIndex].Prefab, 
            gridPosition,objectDataSO.objectsData[selectedObjectIndex].ID,placedPositions);
        
        //new building is added to the list
        selectedData.AddObjectAt(gridPosition,objectDataSO.objectsData[selectedObjectIndex].Size,objectDataSO.objectsData[selectedObjectIndex].ID,index);
        
        previewSystem.UpdatePosition(gridPosition,false);
    }
    /// <summary>
    /// check and update the new position
    /// </summary>
    /// <param name="gridPosition"></param>
    public void UpdateState(Vector2Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePosition(gridPosition,placementValidity);
    }

    /// <summary>
    /// checking the available of the position
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="selectedObjectIndex"></param>
    /// <returns></returns>
    private bool CheckPlacementValidity(Vector2Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = gridData;
        return selectedData.CanPlaceObjectAt(gridPosition, objectDataSO.objectsData[selectedObjectIndex].Size);
    }
}
