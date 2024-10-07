using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildingInfoManager : MonoBehaviour
{
    public LayerMask buildingLayer;
    public Constructable selectedBuilding;
    private bool isSelected = false;
   
    private void Awake()
    {
        EventBus.RegisterEvent<string>(EventName.CreateUnitEvent, CreateUnit);
    }

    private void OnDestroy()
    {
        EventBus.UnregisterEvent<string>(EventName.CreateUnitEvent, CreateUnit);
    }
    /// <summary>
    /// Check if the mouse was clicked over a UI element
    /// </summary>
    /// <returns></returns>
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    private void Update()
    {
        if (IsPointerOverUI()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            //cast a ray
            RaycastHit2D hit2D = RaycastExtension.GetRaycastHit2DFromCamera(buildingLayer);
            //If it hits collider
            if (hit2D.collider && hit2D.collider.CompareTag("Building"))
            {
                SelectedBuilding(hit2D.collider.gameObject);
            }
            else
            {
                if (isSelected)
                {
                    EventBus.TriggerEvent(EventName.SelectedBuildingEvent,-1);
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.SelectDeselectBuilding(false);
                    }

                    selectedBuilding = null;
                    isSelected = false;
                }
            }
        }
        //if select a building and mouse right click
        if (selectedBuilding != null && Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit2D = RaycastExtension.GetRaycastHit2DFromCamera(Constants.GroundLayer);
            if (hit2D.collider && hit2D.collider.CompareTag("Ground"))
            {
                if (hit2D.collider.TryGetComponent(out Cell cell))
                {
                    if (cell.isAvailable)
                    {   
                        //changing position of the building marker
                        ChangeSpawnMarker(cell);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// when a building is selected
    /// </summary>
    /// <param name="selectedObject"></param>
    private void SelectedBuilding(GameObject selectedObject)
    {
        if (selectedBuilding != null)
        {  
            //pre-selected building
            selectedBuilding.SelectDeselectBuilding(false);
        }
        selectedBuilding = selectedObject.GetComponent<Constructable>();
        if (selectedBuilding != null)
        {
            isSelected = true;
            selectedBuilding.SelectDeselectBuilding(true);
            EventBus.TriggerEvent(EventName.SelectedBuildingEvent,selectedBuilding.ID);
        }
        else
        {
            isSelected = false;
            EventBus.TriggerEvent(EventName.SelectedBuildingEvent,-1);
        }
    }
    /// <summary>
    /// Changing position of the building marker
    /// </summary>
    /// <param name="newCell"></param>
    private void ChangeSpawnMarker(Cell newCell)
    {
        selectedBuilding.ChangeSpawnMarker(newCell);
    }

    /// <summary>
    /// creating a new unit
    /// </summary>
    /// <param name="unitName">Specific unit name</param>
    private void CreateUnit(string unitName)
    {
        UnitData data = GameManager.Instance.UnitObjectDataSO.unitsData.Find(x=>x.Name == unitName);
        GameObject unitObject = PoolManager.Instance.GetObject(data.PoolObjectType);
        selectedBuilding.ChangeUnitPosition(unitObject);
        unitObject.SetActive(true);
        
    }
    
}
