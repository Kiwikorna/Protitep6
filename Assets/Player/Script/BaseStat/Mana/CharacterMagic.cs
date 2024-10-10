using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMagic : MonoBehaviour
{
    [FormerlySerializedAs("_magicSO")] [SerializeField] private CharacterManaSO _characterManaSo;
    [SerializeField] private SlotUsing slotUsing;

    private void Awake()
    {
        slotUsing.OnItemUsed += Magic;
    }

    private void Magic(Item item)
    {
        item = InventoryManager.Instance.GetSelectedSlot(true);
        if (item is IManaFlask magicObject)
        {
            _characterManaSo.manaCharacterValue += magicObject.ManaValue;
        }
    }
}