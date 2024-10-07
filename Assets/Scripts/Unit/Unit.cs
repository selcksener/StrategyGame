using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
   public string UnitName;
   public UnitState CurrentState = UnitState.Idle;
   public UnitAnimationController unitAnimationController;
   private void Start()
   {
      UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
   }

   private void OnDestroy()
   {
      UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
   }

   public virtual void ChangeState(UnitState newState)
   {
      CurrentState = newState;
      switch (newState)
      {
         case UnitState.Idle:
            unitAnimationController.IdleAnimation();
            break;
         case UnitState.Following:
            unitAnimationController.FollowAnimation();
            break;
         case UnitState.Attacking:
            unitAnimationController.AttackAnimation();
            break;
      }
   }

   
  
}
