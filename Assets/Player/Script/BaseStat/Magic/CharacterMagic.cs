using System;
using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    [SerializeField] private PlayerMagicSO _magicSO;
    [SerializeField] private InventoryUsing inventoryUsing;

    private void Awake()
    {
        inventoryUsing.onItemUsed += Magic;
    }

    private void Magic(ItemObject itemObject)
    {
             itemObject = InventoryManager.Instance.GetSelectedSlot(true);
            if (itemObject is IManaFlask magicObject)
            {
                _magicSO.manaPlayer += magicObject.ManaValue;
            }
    }
}