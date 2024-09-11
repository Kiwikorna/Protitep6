using System;
using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    [SerializeField] private CharacterMagicSO _magicSO;
    [SerializeField] private InventoryUsing inventoryUsing;

    private void Awake()
    {
        inventoryUsing.OnItemUsed += Magic;
    }

    private void Magic(ItemObject itemObject)
    {
        itemObject = InventoryManager.Instance.GetSelectedSlot(true);
        if (itemObject is IManaFlask magicObject)
        {
            _magicSO.manaCharacter += magicObject.ManaValue;
        }
    }
}