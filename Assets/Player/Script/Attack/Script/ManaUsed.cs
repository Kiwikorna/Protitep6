using System;
using UnityEngine;

public class ManaUsed : MonoBehaviour
{
  [SerializeField] private PlayerAttack playerAttack;
  [SerializeField] private PlayerAttackSO playerAttackMana;
  [SerializeField] private CharacterMagicSO playerManaSO;

  private void Awake()
  {
    playerAttack.onManaSpellUsed += OnManaUsed;
  }

  private void OnManaUsed()
  {
      playerManaSO.manaCharacter -= playerAttackMana.manaFlow;
  }
  
}
