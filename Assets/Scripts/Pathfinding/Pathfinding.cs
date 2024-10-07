/*   PATHFINDING A*

    For more information : 
    https://github.com/selcksener/DotConnectGame
 
 */

using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public InputManager inputManager;
    public static List<Cell> FindPathByPosition(Vector3 startPos, Transform targetPos,InputManager inputManager)
    {
        Vector2Int startGridPosition =  inputManager.GetSelectedCell(startPos);
        Vector2Int endGridPosition =  inputManager.GetSelectedCell(targetPos.position);
        Cell startCell = GridSystem.Instance.cells[Mathf.RoundToInt(startGridPosition.x), Mathf.RoundToInt(startGridPosition.y)];
        Cell targetCell = GridSystem.Instance.cells[Mathf.RoundToInt(endGridPosition.x), Mathf.RoundToInt(endGridPosition.y)];
       return  FindPath(startCell, targetCell, GridSystem.Instance);

    }

    public static List<Cell> FindPath(Cell startPos, Cell targetPos, GridSystem grid)
    {
        List<Cell> nodes_path = new List<Cell>();

        Cell startNode = grid.cells[startPos.x, startPos.y];
        Cell endNode = grid.cells[targetPos.x, targetPos.y];

        List<Cell> openList = new List<Cell>();
        HashSet<Cell> closedList = new HashSet<Cell>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Cell currentNode = openList[0];

            for (int i = 0; i < openList.Count; i++)
            {
                //cost is lower than current
                if (openList[i].fCost < currentNode.fCost ||
                    (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];

                }
            }

            openList.Remove(currentNode); //not to check again
            closedList.Add(currentNode); //

            // the pathfinding ends when the current node reaches the end node
            if (currentNode == endNode)
            {
                List<Cell> path = new List<Cell>();
                Cell node = endNode;
                while (node != startNode)
                {
                    path.Add(node);
                    node = node.parent;
                }

                nodes_path = path;
                nodes_path.Reverse();
               
                break;
            }

            List<Cell> neighCells = GetNeighbours(currentNode, grid);
            foreach (Cell neighbour in neighCells)
            {
                if ((closedList.Contains(neighbour) || (neighbour.isAvailable == false) ) &&  neighbour != endNode) continue;
                //Calculate movement cost to neighbour
                int movementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (movementCostToNeighbour < neighbour.gCost || openList.Contains(neighbour) == false)
                {
                    //Set Cost
                    neighbour.gCost = movementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openList.Contains(neighbour))
                        openList.Add(neighbour);
                }
            }
        }

        //if find path , add start node to the list
        if (nodes_path.Count > 0)
        {
            nodes_path.Insert(0, startPos);
            //nodes_path.RemoveAt(nodes_path.Count-1);
        }
        return nodes_path;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns></returns>
    private static int GetDistance(Cell nodeA, Cell nodeB)
    {
        //pathfinding algorithm - A* algorithm
        int distX = Mathf.Abs(nodeA.x - nodeB.x);
        int distY = Mathf.Abs(nodeA.y - nodeB.y);
        int d = distX > distY ? 14 * distY + 10 * (distX - distY) : 14 * distX + 10 * (distY - distX);
        return d;
    }


    /// <summary>
    /// neighbors of the cell
    /// </summary>
    /// <param name="_node">cell to search neighbors</param>
    /// <returns></returns>
    public static List<Cell> GetNeighbours(Cell _node, GridSystem grid)
    {
        List<Cell> neighbours = new List<Cell>();
        neighbours = new List<Cell>();
        for (int i = -1; i <= 1; i++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (i == 0 && y == 0) continue; // not search for itself
                //if ((Mathf.Abs(i) + Mathf.Abs(y)) % 2 == 0) continue; //only searches left-right-up-down neighbour
                int checkX = _node.x + i;
                int checkY = _node.y + y;
                if (checkX >= 0 && checkX < grid.Row && checkY >= 0 &&
                    checkY < grid.Col)
                {
                    neighbours.Add(grid.cells[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
}