using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class SpellCaster : MonoBehaviour
{
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private SpellSO playerSpellSo;
   [SerializeField] private CharacterManaSO playerMana;
   [SerializeField] private float spellDelayPressButton;
   [SerializeField] private float spellCooldownAfterUsed;

   private bool _isCastingSpell = false;
   private bool _isCooldownSpell = false;

   private void Update()
   {
      if (playerMana.manaCharacter > playerSpellSo.manaCost)
      {
         if (Input.GetKeyDown(KeyCode.Space) && !_isCastingSpell && !_isCooldownSpell)
         {
            StartCoroutine(DelaySpell());
         }
      }
   }

   private IEnumerator DelaySpell()
   {
      _isCastingSpell = true;
      yield return new WaitForSeconds(spellDelayPressButton);
      var spell = Instantiate(playerSpellSo.prefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
      spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * playerSpellSo.speed;
      playerMana.manaCharacter -= playerSpellSo.manaCost;
      _isCastingSpell = false;
      StartCoroutine(CooldownSpell());
      
      
      
   }

   private IEnumerator CooldownSpell()
   {
      _isCooldownSpell = true;
      yield return new WaitForSeconds(spellCooldownAfterUsed);

      _isCooldownSpell = false;
   }
}
