using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector2Int position);
    void UpdateState(Vector2Int gridPosition);
}
