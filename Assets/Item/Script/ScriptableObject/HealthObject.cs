using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/HealthItem",fileName = "HealthObject")]
public class HealthObject : ItemObject,IHealthFlask
{
   public int healthFlask;


   public float HealthValue
   {
      get => healthFlask;
      set => value = healthFlask;
   }
}
