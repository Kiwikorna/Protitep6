using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Item/Spell/spellObject",fileName = "spellObject")]
public class SpellItem : ItemInInventory
{
    [FormerlySerializedAs("spellProjectile")] public BaseSpell spell;
}
