using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private AttackController attackController;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            IClickable clickable = other.GetComponent<IClickable>();
            //check if clickable is not null!
            if (clickable != null && other.CompareTag("Enemy"))
            {
                //updating the target object
                attackController.SetTarget(other.gameObject);
                
            }
        }
    }
}
