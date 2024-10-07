using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : SingletonBehaviour<UnitSelectionManager>
{

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();
    public LayerMask followingLayer;
    private void Update()
    {
        //when clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2D = RaycastExtension.GetRaycastHit2DFromCamera(LayerMask.GetMask("Unit"));
            if (hit2D.collider)
            {
                SelectByClicking(hit2D.collider.gameObject);
                
            }
            else
            {
                DeselectAll();
            }
        }
        //when right mouse clicked
        if (Input.GetMouseButtonDown(1) && unitsSelected.Count>0)
        {
            RaycastHit2D hit2D =RaycastExtension.GetRaycastHit2DFromCamera(followingLayer);
            if (hit2D.collider)
            {
                FollowTarget(hit2D.collider.gameObject);
            }
            else
            {
                StopFollowing();
            }
        }
    }
    /// <summary>
    /// stop moving
    /// </summary>
    private void StopFollowing()
    {
        
    }
    /// <summary>
    /// updating the following target object
    /// </summary>
    /// <param name="targetObject"></param>
    private void FollowTarget(GameObject targetObject)
    {
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<UnitMovement>().SetTarget(targetObject.transform);
        }
    }
    /// <summary>
    /// select unit by clicking
    /// </summary>
    /// <param name="selectedUnit"></param>
    private void SelectByClicking(GameObject selectedUnit)
    {
        foreach (var unit in unitsSelected)
        {
            TriggerSelectionIndicator(unit,false);
        }
        unitsSelected.Clear();
        unitsSelected.Add(selectedUnit);
        SelectUnit(selectedUnit,true);
    }
    /// <summary>
    /// selected object is activated
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="isSelected"></param>
    private void SelectUnit(GameObject unit,bool isSelected)
    {
        TriggerSelectionIndicator(unit,isSelected);
    }
    /// <summary>
    /// select unit by dragging
    /// </summary>
    /// <param name="unit"></param>
    public void DragSelect(GameObject unit)
    {
        
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit,true);
        }
    }
    /// <summary>
    /// Clear selected units
    /// </summary>
    private void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
         TriggerSelectionIndicator(unit,false);   
        }
        unitsSelected.Clear();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="isVisible"></param>
    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

}
