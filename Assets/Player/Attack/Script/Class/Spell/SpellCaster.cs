using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class SpellCaster : MonoBehaviour
{
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private CharacterManaSO playerMana;
   [SerializeField] private float spellDelayPressButton;
   [SerializeField] private SpellSlot[] _spellSlots;
   private SpellConfig _currentSpellConfig;  // Для хранения конфигурации текущего заклинания

   private bool _isCastingSpell = false;

   private void Start()
   {
      ControllerPlayer.Instance.OnCastBaseSpellButtonPressed += SpellCast;
   }

   private void SpellCast()
   {
      for (int i = 0; i < _spellSlots.Length; i++)
      {
         SpellSlot spellSlot = _spellSlots[i];
         InventoryItem inventoryItem = spellSlot.GetComponentInChildren<InventoryItem>();

         // Проверяем, есть ли конфигурация заклинания у предмета
         if (inventoryItem != null && inventoryItem.Config != null)
         {
            if (playerMana.manaCharacterValue > inventoryItem.Config.GetManaCost())
            {
               if (!_isCastingSpell)
               {
                  _currentSpellConfig = inventoryItem.Config;  // Присваиваем текущую конфигурацию заклинания
                  StartCoroutine(DelaySpell());
               }
            }
         }
      }
   }

   private IEnumerator DelaySpell()
   {
      _isCastingSpell = true;
      yield return new WaitForSeconds(spellDelayPressButton);

      // Используем _currentSpellConfig для создания заклинания
      var spell = Instantiate(_currentSpellConfig.GetSpellPrefab(), spellSpawnPoint.position, spellSpawnPoint.rotation);
      spell.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * _currentSpellConfig.GetSpeed();

      // Уменьшаем количество маны
      playerMana.manaCharacterValue -= _currentSpellConfig.GetManaCost();
      _isCastingSpell = false;
   }
}
