using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class SpellCaster : MonoBehaviour
{
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private CharacterManaSO playerMana;
   [SerializeField] private float spellDelayPressButton;
    private GameObject _spellPrefab;
   [SerializeField]private UsualSpellProjectile _baseSpellProjectile;

   private bool _isCastingSpell = false;

   private void Start()
   {
      Controller.Instance.onCastBaseSpellButtonPressed += SpellCast;
   }
   private void SpellCast()
   {
      
      
      if (playerMana.manaCharacter > _baseSpellProjectile.SpellManaCost)
      {
         if (!_isCastingSpell)
         {
            StartCoroutine(DelaySpell());
            
         }
      }
   }
   private IEnumerator DelaySpell()
   {
      _isCastingSpell = true;
      yield return new WaitForSeconds(spellDelayPressButton);
      var spell = Instantiate(_baseSpellProjectile.GetSpellPrefab(), spellSpawnPoint.position, spellSpawnPoint.rotation);
      
      spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * _baseSpellProjectile.SpellSpeed;
      playerMana.manaCharacter -= _baseSpellProjectile.SpellManaCost;
      _isCastingSpell = false;
   }
   
   
   
}
