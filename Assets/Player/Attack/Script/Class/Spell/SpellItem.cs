using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Spell/spellObject",fileName = "spellObject")]
public class SpellItem : ItemInInventory
{
      public SpellConfig spellConfig ;
      public int id;

}
