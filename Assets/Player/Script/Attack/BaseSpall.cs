using System;
using UnityEngine;

public class BaseSpall : MonoBehaviour
{
    private const float LifeTime = 3f;
    [SerializeField] private CharacterMagicSO playerMana;
    [SerializeField] private PlayerAttackSO playerDamage;


    private void Update()
    {
        if(playerMana.manaCharacter <= playerDamage.manaFlow)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BehaviourEnemy enemy = other.GetComponent<BehaviourEnemy>();
        if (CanCastSpell())
        {
            if (enemy != null)
            {

                enemy.TakeDamage(playerDamage.attackDamage);

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            ConsumeMana();
        }
        


    }
    private bool CanCastSpell()
    {
        // Check if there is enough mana to cast the spell
        return playerMana.manaCharacter > playerDamage.manaFlow;
    }

    private void ConsumeMana()
    {
        // Deduct the mana cost from the player's mana
        playerMana.manaCharacter -= playerDamage.manaFlow;
    }
}
