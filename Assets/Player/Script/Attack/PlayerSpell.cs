using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SpellCaster : MonoBehaviour
{
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private SpellSO playerSpellSo;
   [SerializeField] private CharacterManaSO playerMana;

   private void Update()
   {
      if (playerMana.manaCharacter > playerSpellSo.manaCost)
      {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            var spell = Instantiate(playerSpellSo.prefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
            spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * playerSpellSo.speed;

            playerMana.manaCharacter -= playerSpellSo.manaCost;
         }
      }
   }
}
