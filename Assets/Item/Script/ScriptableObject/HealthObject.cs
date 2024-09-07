using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/HealthItem",fileName = "HealthObject")]
public class HealthObject : ItemObject
{
   public int healthFlask;
   private void Awake()
   {
      flackItem = FlackItem.HealthItem;
   }
}
