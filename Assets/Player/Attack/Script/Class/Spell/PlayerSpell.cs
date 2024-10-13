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
  private BaseSpell _baseSpell;

   private bool _isCastingSpell = false;

   private void Start()
   {
      ControllerPlayer.Instance.OnCastBaseSpellButtonPressed += SpellCast;
      
   }
   private void SpellCast()
   {
      
      
      if (playerMana.manaCharacterValue > _baseSpell.ManaCost)
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
      var spell = Instantiate(_baseSpell.GetSpellPrefab(), spellSpawnPoint.position, spellSpawnPoint.rotation);
      
      spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * _baseSpell.Speed;
      playerMana.manaCharacterValue -= _baseSpell.ManaCost;
      _isCastingSpell = false;
   }
   
   
   
}
