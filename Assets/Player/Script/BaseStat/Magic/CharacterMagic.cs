using System;
using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    [SerializeField] private CharacterManaSO _magicSO;
    [SerializeField] private InventoryUsing inventoryUsing;

    private void Awake()
    {
        inventoryUsing.OnItemUsed += Magic;
    }

    private void Magic(Item item)
    {
        item = InventoryManager.Instance.GetSelectedSlot(true);
        if (item is IManaFlask magicObject)
        {
            _magicSO.manaCharacter += magicObject.ManaValue;
        }
    }
}