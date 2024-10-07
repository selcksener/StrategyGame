using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : SingletonBehaviour<GridSystem>
{
    public GameObject cellPrefab;
    public float offsetCameraSize = 1f;
    public Color cellColor1, cellColor2;
    public Cell[,] cells;
    
    private int row;
    private int col;
    private Vector3 pos;
    private Vector2 borderX, borderY;
    
    
    public int Row { get { return row; } }
    public int Col { get { return col; } }

    protected override void Awake()
    {
        base.Awake();
        CameraBorder();
        CreateGrid();
    }

    /// <summary>
    /// Creating the new grid
    /// </summary>
    private void CreateGrid()
    {
        pos = Vector3.zero;
        col = (Mathf.RoundToInt(Mathf.Abs(borderY.x) + Mathf.Abs(borderY.y)) / 8)+1;
        row = (Mathf.RoundToInt(Mathf.Abs(borderX.x) + Mathf.Abs(borderX.y)) / 8)+1;
        cells = new Cell[row, col];
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.GetComponent<SpriteRenderer>().color = (i + j) % 2 == 1 ? cellColor1 : cellColor2;
                pos.x += 8;
                cell.GetComponent<Cell>().UpdateCell(new Vector2Int(j, i));
                cells[j, i] = cell.GetComponent<Cell>();
            }
            pos.x = 0;
            pos.y += 8;
        }

        //Change camera position and orthographic size
        Camera.main.transform.position = new Vector3(row * 8 / 2, col * 8 / 2, -10);
    }

    /// <summary>
    /// Borders of the main camera
    /// </summary>
    private void CameraBorder()
    {
        Vector2 screen = new Vector2(Screen.width, Screen.height);

        var upperLeftScreen = new Vector3(0, Screen.height, 0);
        var upperRightScreen = new Vector3(Screen.width, Screen.height, 0);

        var uLeft = Camera.main.ScreenToWorldPoint(upperLeftScreen);
        var uRight = Camera.main.ScreenToWorldPoint(upperRightScreen);
        borderX = new Vector3(uLeft.x, Mathf.Abs(uRight.x));
        borderY = new Vector3(Mathf.Abs(uRight.y), uRight.y);
    }
}