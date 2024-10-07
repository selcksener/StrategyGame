using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAttackable
{
    public float currentHealth,maxHealth;
    public Health healthTracker;
    private Constructable constructable;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        constructable = GetComponent<Constructable>();
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.red;
        }
    }

    /// <summary>
    /// Take damage to the object
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            spriteRenderer.color = Color.white;
            constructable.DestroyObject();
        }
        //updating object's ui
        healthTracker.UpdateUI(currentHealth,maxHealth);
    }
}
