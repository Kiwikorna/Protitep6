using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
   [SerializeField]private HealthFlask healthFlask;
   private ItemObject _itemObject;
   
   private void Update()
   {
      HealthFlask();
   }

   private void HealthFlask()
   {
      if (Controller.Instance.GetInteractionUseHandler())
      {
         _itemObject = InventoryManager.Instance.GetSelectedSlot(true);
         if (_itemObject != null)
         {
            if (_itemObject.flackItem == ItemObject.FlackItem.HealthItem)
            {
               healthFlask.Health();
               
              
            }
         }
      }
   }
}
