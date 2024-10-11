using UnityEngine;

public abstract class BaseSpellProjectile : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
   [field:SerializeField] public float Damage { get; private set; }
   [field:SerializeField] public float ManaCost { get; private set; }
   [field:SerializeField] public float Range { get; private set; }
   [field:SerializeField] public float Speed { get; private set; }
   
    public abstract void SpellProjectileFly();
    public  GameObject GetSpellPrefab() => prefab;

   
}
