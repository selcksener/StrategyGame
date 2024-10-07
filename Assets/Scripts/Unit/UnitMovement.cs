using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
   private Unit unit;
   [SerializeField] private float speed=5f;
   [SerializeField] private Rigidbody2D rigidbody2d;
   [SerializeField] private Transform targetTransform;
   [SerializeField] private bool isMoving = false;
   [SerializeField] private List<Cell> waypointsList = new List<Cell>();
   [SerializeField] private float nextWaypointPassRadius = 1f;
   private int currentWaypoint = 0;
   private Bounds cellBounds;
   private Vector3 centerPosition;
  
   private bool isCommandToMove = false;
   public bool IsCommandToMove { get => isCommandToMove; set => isCommandToMove = value; }
   
   private void Awake()
   {
      unit = GetComponent<Unit>();
   }

   private void Update()
   {
      if (isMoving == false)
         return;
      if(targetTransform == null) return;
      
      FollowTarget();
   }

   private void FollowTarget()
   {
      if (targetTransform == null)
      {
         StopFollow();
         return;
      }
      
      Vector3 dir = waypointsList[currentWaypoint].transform.position+centerPosition - transform.position;
      float rotationz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0f,0f,rotationz);
      //moving towards  the target
      transform.position = Vector3.MoveTowards(transform.position,waypointsList[currentWaypoint].transform.position+centerPosition,speed * Time.deltaTime);
      //checking the distance for the next waypoint
      if (Vector2.Distance(transform.position,waypointsList[currentWaypoint].transform.position+centerPosition) < nextWaypointPassRadius)
      {
         currentWaypoint++;
         if (currentWaypoint > waypointsList.Count-1)
         {
           
            StopFollow();
         }
        
      }
   }
   /// <summary>
   /// stop when the target position is reached 
   /// </summary>
   public void StopFollow()
   {
      isMoving = false;
      isCommandToMove = false;
      unit.ChangeState(UnitState.Idle);
   }

   /// <summary>
   /// updating the target
   /// </summary>
   /// <param name="target"></param>
   public void SetTarget(Transform target)
   {
      targetTransform = target;
      //Pathfinding algorithm A*
      waypointsList =  Pathfinding.FindPathByPosition(transform.position-centerPosition, targetTransform,InputManager.Instance);
      IClickable clickable = target.GetComponent<IClickable>();
      
      if (waypointsList.Count > 0)
      {
         //if the target is building, remove the last waypoint
         if (clickable != null )
         {
            waypointsList.RemoveAt(waypointsList.Count-1);
         }
         //for the center position of the cell
         cellBounds = waypointsList[0].cellImage.bounds;
         centerPosition = new Vector3(cellBounds.size.x/2, cellBounds.size.y/2, 0f);
         currentWaypoint = 0;
         unit.ChangeState(UnitState.Following);
         isCommandToMove = true;
         isMoving = true;
      }
      else
      {
         targetTransform = null;
         
      }
      
   }
   
}
