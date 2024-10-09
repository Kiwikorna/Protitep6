using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
   [SerializeField] private InventoryUsing inventoryUsing;
   [SerializeField] private CharacterHealthSO healthSO;

   private void Awake()
   {
      inventoryUsing.OnItemUsed += TryUsedItem;
   }

   private void TryUsedItem(Item item)
   {
      if (item is IHealthFlask healthObject)
      {
        healthSO.health += healthObject.HealthValue;
      }
   }
}