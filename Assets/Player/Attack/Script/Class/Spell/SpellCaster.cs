using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class SpellCaster : MonoBehaviour
{  [Header("Cast Spell System")]
   [SerializeField] private Transform spellSpawnPoint;
   [SerializeField] private CharacterManaSO playerMana;
   [SerializeField] private SpellSlot[] _spellSlots;
  
   [Header("StateMove")]
  
   [SerializeField] private float timeStopMove;
   
   private SpellItem _currentSpellConfig;  // Для хранения конфигурации текущего заклинания
   private StateMove _stateMove;
   private bool _isCastingSpell = false;

   private void Awake()
   {
      _stateMove = StateMove.Move;
   }

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
            if (playerMana.manaCharacterValue > inventoryItem.Config.spellConfig.GetManaCost())
            {
               if (!_isCastingSpell)
               {
                  _currentSpellConfig = inventoryItem.Config;
                 
                  StartCoroutine(DelaySpell());
                  
               }
            }
         }
      }
   }
   private IEnumerator BlockMovement()
   {
      _stateMove = StateMove.NoMove;
      Debug.Log(_stateMove);// Блокируем движение
      yield return new WaitForSeconds(timeStopMove);  // Ждем окончания времени блокировки
      _stateMove = StateMove.Move;  // Возвращаем возможность движения
   }
   private IEnumerator DelaySpell()
   {
      
      
      _isCastingSpell = true;
      yield return new WaitForSeconds(_currentSpellConfig.spellConfig.GetDelayForSpell());
   
      // Создаем заклинание
      var spellObject = Instantiate(_currentSpellConfig.spellConfig.GetSpellPrefab(), spellSpawnPoint.position, spellSpawnPoint.rotation);
      spellObject.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * _currentSpellConfig.spellConfig.GetSpeed();
      playerMana.manaCharacterValue -= _currentSpellConfig.spellConfig.GetManaCost();
      StartCoroutine(BlockMovement());
      // Уменьшаем количество маны
      
   
      // Возвращаем возможность движения после каста
      _isCastingSpell = false;
      
      
   }

   public StateMove GetStateMove() => _stateMove;
}
