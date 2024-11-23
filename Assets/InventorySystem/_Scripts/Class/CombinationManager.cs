using System;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : MonoBehaviour
{
   [SerializeField] List<ComboResipe> comboResipes;
   private Dictionary<(int,int),SpellItem> combinationsRecipe;

   private void Awake()
   {
      InitializeRecipeDictionary();
   }

   private void InitializeRecipeDictionary()
   {
      combinationsRecipe = new Dictionary<(int, int), SpellItem>();
      foreach (var combo in comboResipes)
      {
         combinationsRecipe[(combo.firstItem.id, combo.secondItem.id)] = combo.resultItem;
         combinationsRecipe[(combo.secondItem.id, combo.firstItem.id)] = combo.resultItem;
      }
   }


   public SpellItem GetCombinationResult(SpellItem spellItem1, SpellItem spellItem2)
   {
      // Проверяем, что оба предмета существуют
      if (spellItem1 != null && spellItem2 != null)
      {
         return combinationsRecipe.GetValueOrDefault((spellItem1.id, spellItem2.id));
      }

      return null;
   }
   
   
}
