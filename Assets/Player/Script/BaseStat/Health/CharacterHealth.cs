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

   private void TryUsedItem(ItemObject itemObject)
   {
      if (itemObject is IHealthFlask healthObject)
      {
        healthSO.health += healthObject.HealthValue;
      }
   }
}