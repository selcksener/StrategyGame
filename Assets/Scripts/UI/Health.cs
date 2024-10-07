using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   //public SpriteRenderer healthImage;
   public Image healthImage;
   public void UpdateUI(float currentHealth, float maxHealth)
   {
     // healthImage.
     healthImage.fillAmount = currentHealth / maxHealth;
   }
}
