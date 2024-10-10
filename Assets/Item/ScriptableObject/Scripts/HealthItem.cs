using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/HealthItem",fileName = "HealthObject")]
public class HealthItem : Item,IHealthFlask
{
   public int healthFlask;
   


   public float HealthValue
   {
      get => healthFlask;
      set => value = healthFlask;
   }
}
