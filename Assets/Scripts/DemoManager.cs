using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DemoManager : MonoBehaviour
{
    public PlacementSystem PlacementSystem;
    public ObjectPlacer objectPlacer;
    public GridData gridData;
    public List<int> demoEnemyBuilding;
    public List<GameObject> demoEnemyUnit;

    private void Start()
    {
        for (int i = 0; i < demoEnemyBuilding.Count; i++)
        {
           
            SetDemoBuilding(demoEnemyBuilding[i]);
        }
    }


    private void SetDemoBuilding(int buildingID)
    {
        ObjectData objectData = GameManager.Instance.ObjectDataSO.objectsData.Find(x => x.ID == buildingID);
        int x = Random.Range(0, GridSystem.Instance.Row);
        int y = Random.Range(0, GridSystem.Instance.Col);
        Vector2Int newPosition = GetRandomAvailablePosition(new Vector2Int(x,y), objectData.Size);
        while (newPosition == new Vector2Int(-1,-1))
        {
            x = Random.Range(0, GridSystem.Instance.Row);
            y = Random.Range(0, GridSystem.Instance.Col);
            newPosition = GetRandomAvailablePosition(new Vector2Int(x,y), objectData.Size);
        }
        PlacementSystem.StartPlacement(buildingID);

        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(GridSystem.Instance.cells[x,y].transform.position.x), Mathf.RoundToInt(GridSystem.Instance.cells[x,y].transform.position.y));
        PlacementSystem.buildingState.OnAction(gridPosition);
        PlacementSystem.StopPlacement();
        GameObject building =objectPlacer.placedObjects[^1];
        Constructable constructable = building.GetComponent<Constructable>();
        building.tag = "Enemy";
        Enemy enemy = building.AddComponent<Enemy>();
        enemy.currentHealth = objectData.HealthData.MaxHealth;
        enemy.maxHealth = objectData.HealthData.MaxHealth;
        enemy.healthTracker = building.GetComponent<Health>();

    }

    public Vector2Int GetRandomAvailablePosition(Vector2Int position, Vector2Int size)
    {
        Vector2Int randomPosition = new Vector2Int(-1, -1);
        while (GridSystem.Instance.cells[position.x, position.y].isAvailable == false)
        {
            return randomPosition;
        }

        List<Vector2Int> positions = GetOccupiedPositions(position, size);
        
        foreach (var pos in positions)
        {
            if (pos.x >= GridSystem.Instance.Row || pos.y >= GridSystem.Instance.Col) return randomPosition;
            if (GridSystem.Instance.cells[pos.x,pos.y].isAvailable == false)
            {
                return randomPosition;
            }
        }
        randomPosition = position;
        return randomPosition;
    }

    private List<Vector2Int> GetOccupiedPositions(Vector2Int position, Vector2Int size)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                positions.Add(position+new Vector2Int(i,j));
            }
        }

        return positions;
    }
}