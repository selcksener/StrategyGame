using System;
using UnityEngine;
using UnityEngine.Events;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private int selectedID;
    public Grid grid;
    public GridSystem gridSystem;
    public IBuildingState buildingState;
    public InputManager inputManager;
    public GridData gridData;
    public ObjectPlacer objectPlacer;
    public PreviewSystem previewSystem;
    public Vector2Int lastDetectedPosition;
    public static UnityEvent<Vector2Int, GameObject,int> DestroyObjectEvent = new UnityEvent<Vector2Int, GameObject,int>(); 
    private void Awake()
    {
        // StartPlacement(0);
        gridData = new();
        DestroyObjectEvent.AddListener(DestroyBuilding);
        
    }
    private void Start()
        {
            EventBus.RegisterEvent<int>(EventName.StartPlacement,StartPlacement);
            }
    private void OnDestroy()
    {
        DestroyObjectEvent.RemoveListener(DestroyBuilding);
    }

    /// <summary>
    /// when the new building is selected from the ui
    /// </summary>
    /// <param name="id"></param>
    public void StartPlacement(int id)
    {
        selectedID = id;
        StopPlacement();
        //new state
        buildingState = new PlacementState(id, previewSystem, grid, GameManager.Instance.ObjectDataSO, gridData,
            objectPlacer);
        //register listener
        EventBus.RegisterEvent(EventName.OnClicked,PlaceStructure);
        EventBus.RegisterEvent(EventName.OnExit,StopPlacement);
    }
    /// <summary>
    /// Placement of the building
    /// </summary>
    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector2Int gridPosition = inputManager.GetSelectedCell();
        buildingState.OnAction(gridPosition);
        StopPlacement();
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopPlacement()
    {
        if (buildingState == null) return;
        buildingState.EndState();
        EventBus.UnregisterEvent(EventName.OnClicked,PlaceStructure);
        EventBus.UnregisterEvent(EventName.OnExit,StopPlacement);
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null) return;
        //updating the building position
        Vector2Int gridPosition = inputManager.GetSelectedCell();
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }

    private void DestroyBuilding(Vector2Int currentPosition,GameObject buildingObject,int id)
    {
        objectPlacer.placedObjects.Remove(buildingObject);
        gridData.RemoveObjectAt(currentPosition);
        PoolManager.Instance.AddObjectInPool(GameManager.Instance.ObjectDataSO.GetObjectDataById(id).PoolObjectType,
            buildingObject);
        if (buildingObject.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy);
            buildingObject.tag = "Building";
        }

    }
}