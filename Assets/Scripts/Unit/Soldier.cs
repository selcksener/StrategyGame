using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public override void ChangeState(UnitState newState)
    {
        base.ChangeState(newState);
        switch (newState)
        {
            case UnitState.Idle:
    
                break;
            case UnitState.Following:

                break;
            case UnitState.Attacking:
                
                break;
        }
    }
    
    
}

public enum UnitState
{
    Idle,
    Following,
    Attacking,
}
