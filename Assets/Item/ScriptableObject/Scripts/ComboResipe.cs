using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Recipe/RecipeCombo",fileName = "newRecipeCombo")]
public class ComboResipe : ScriptableObject
{
   public SpellItem firstItem;
   public SpellItem secondItem;

   public SpellItem resultItem;
   


}
