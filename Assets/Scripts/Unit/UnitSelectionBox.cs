using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelectionBox : MonoBehaviour
{
    private Camera myCam;
 
    [SerializeField] private RectTransform boxVisual;
 
    private Rect selectionBox;
    private Vector2 startPosition;
    private Vector2 endPosition;
 
    private void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }
 
    private void Update()
    {
        // When Clicked
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            startPosition = Input.mousePosition;
            boxVisual.gameObject.SetActive(true);
            // For selection the Units
            selectionBox = new Rect();
        }
 
        // When Dragging
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
 
        // When Releasing
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            boxVisual.gameObject.SetActive(false);
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }
    /// <summary>
    /// Calculate the starting and ending positions of the selection box.
    /// </summary>
    void DrawVisual()
    {
        
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;
        Vector2 boxCenter = (boxStart + boxEnd) / 2;
 
        // Set the position of the visual selection box based on its center.
        boxVisual.position = boxCenter;
        // Calculate the size of the selection box in both width and height.
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        // Set the size of the visual selection box based on its calculated size.
        boxVisual.sizeDelta = boxSize;
    }
 
    void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }
 
 
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void SelectUnits()
    {
      //  UnitSelectionManager.Instance.unitsSelected.Clear();
        foreach (var unit in UnitSelectionManager.Instance.allUnitsList)
        {
            
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelectionManager.Instance.DragSelect(unit);
            }
        }
    }
}
 
