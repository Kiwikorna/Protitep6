using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private SlotUsing slotUsing;
   [SerializeField] private CharacterHealthSO healthValueSo;

   private void Awake()
   {
      slotUsing.OnItemUsed += TryUsedItem;
   }

   private void TryUsedItem(ItemInInventory itemInInventory)
   {
      if (itemInInventory is IHealthFlask health)
      {
        healthValueSo.healthValue += health.HealthValue;
      }
   }
}