
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : MonoBehaviour
{
   [SerializeField] List<ComboResipe> comboResipes;
   private Dictionary<HashSet<int>, List<SpellItem>> combinationsRecipe;

   private void Awake()
   {
      InitializeRecipeDictionary();
   }
   

   private void InitializeRecipeDictionary()
   {
      combinationsRecipe = new Dictionary<HashSet<int>, List<SpellItem>>(new HashSetComparer());
      foreach (var combo in comboResipes)
      {
         var set = new HashSet<int> { combo.firstItem.id, combo.secondItem.id };

         if (!combinationsRecipe.ContainsKey(set))
         {
            combinationsRecipe[set] = new List<SpellItem>();
         }

         combinationsRecipe[set].Add(combo.resultItem);
      }
   }

   public SpellItem GetCombinationResult(SpellItem spellItem1, SpellItem spellItem2)
   {
      if (spellItem1 != null && spellItem2 != null)
      {
         var set = new HashSet<int> { spellItem1.id, spellItem2.id };
         
         
         
         if (combinationsRecipe.TryGetValue(set, out List<SpellItem> results))
         {
            if (results.Count > 0)
            {
               return results[0]; // или выбирать результат по другому критерию
            }
         }
      }

      return null;
   }


   
   
}
