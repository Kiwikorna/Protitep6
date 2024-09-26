using UnityEngine;

public abstract class BaseSpellProjectile : MonoBehaviour
{
    [SerializeField]private GameObject spellPrefab;
   [field:SerializeField] public float SpellDamage { get; private set; }
   [field:SerializeField] public float SpellManaCost { get; private set; }
   [field:SerializeField] public float SpellRange { get; private set; }
   [field:SerializeField] public float SpellSpeed { get; private set; }
   
    public abstract void SpellProjectileFly();
    public  GameObject GetSpellPrefab() => spellPrefab;

   
}
