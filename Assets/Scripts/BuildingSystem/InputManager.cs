using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : SingletonBehaviour<InputManager>
{
    private Camera camera;
    private Vector3 lastPosition;

    public LayerMask groundLayer;
    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    private void Update()
    {
        //when left mouse  clicked
        if (Input.GetMouseButtonDown(0))
        {
            EventBus.TriggerEvent(EventName.OnClicked); 
        }
        //cancel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.TriggerEvent(EventName.OnExit); 
        }
    }
    /// <summary>
    /// Get the position of the  clicked ground
    /// </summary>
    /// <returns></returns>
    public Vector3 GetSelectedMapPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        RaycastHit2D hit2D = Physics2D.Raycast(camera.ScreenToWorldPoint(mousePos), Vector2.zero, groundLayer);
        if (hit2D.collider)
        {
            lastPosition = hit2D.transform.position;
        }

        return lastPosition;
    }
    /// <summary>
    /// Get cell position by mouse position
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetSelectedCell()
    {
        Vector2 mousePos = GetSelectedMapPosition();
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        return pos;
    }
    /// <summary>
    /// Get cell position by position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2Int GetSelectedCell(Vector3 position)
    {
        Vector2Int pos = Vector2Int.zero;
        pos = new Vector2Int(Mathf.RoundToInt(position.x / 8), Mathf.RoundToInt(position.y / 8));
        return pos;
    }

    /// <summary>
    /// Check if the mouse was clicked over a UI element
    /// </summary>
    /// <returns></returns>
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    
}