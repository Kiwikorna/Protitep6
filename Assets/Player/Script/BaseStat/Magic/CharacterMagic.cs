using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    [SerializeField] private PlayerMagicSO _magicSO;

    private void Update()
    {
        Magic();
    }

    private void Magic()
    {
        if (Controller.Instance.GetInteractionUseHandler())
        {
            var _itemObject = InventoryManager.Instance.GetSelectedSlot(true);
            if (_itemObject is IManaFlask magicObject)
            {
                _magicSO.manaPlayer += magicObject.ManaValue;
            }
        }
    }
}