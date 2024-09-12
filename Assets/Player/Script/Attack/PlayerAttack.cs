using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private PlayerAttackSO playerSpell;
   [SerializeField] private CharacterMagicSO playerMana;
   public event Action onManaSpellUsed;

   private void Update()
   {
      if (playerMana.manaCharacter > playerSpell.manaFlow)
      {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            var spell = Instantiate(playerSpell.prefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
            spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * playerSpell.speed;
         
            onManaSpellUsed?.Invoke();
         }
      }
   }
}
