using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/HealthItem",fileName = "HealthObject")]
public class HealthItemInInventory : ItemInInventory,IHealthFlask
{
   public int healthValue;
   

   public float HealthValue
   {
      get => healthValue;
      set => value = healthValue;
   }
}
