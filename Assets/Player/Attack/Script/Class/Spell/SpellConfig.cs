using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public  class SpellConfig
{
     [SerializeField] private GameObject prefab;
     [SerializeField] private float damage;
     [SerializeField] private float manaCost;
     [SerializeField] private float range;
     [SerializeField] private float speed;
    

   public GameObject GetSpellPrefab() => prefab;
   public float GetManaCost() => manaCost;
   public float GetDamage() => damage;
   public float GetRange() => range;
   public float GetSpeed() => speed;

}
