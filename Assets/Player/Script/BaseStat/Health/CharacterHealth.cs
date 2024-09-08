using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
   [SerializeField] private InventoryUsing inventoryUsing;
   [SerializeField] private PlayerHealthSO healthSO;

   private void Awake()
   {
      inventoryUsing.onItemUsed += TryUsedItem;
   }

   private void TryUsedItem(ItemObject itemObject)
   {
      if (itemObject is IUsableItem healthObject)
      {
        healthObject.UseItem();
      }
   }
}