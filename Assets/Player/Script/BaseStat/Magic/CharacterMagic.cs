using UnityEngine;

public class CharacterMagic : MonoBehaviour
{
    [SerializeField] private MagicFlask flask;
    private ItemObject _itemObject;
    private void Update()
    {
        Magic();
    }
    private void Magic()
    {
        if (Controller.Instance.GetInteractionUseHandler())
        {
           
            if (flask != null)
            {
                
                if (_itemObject.flackItem == ItemObject.FlackItem.ManaItem)
                {
                    _itemObject = InventoryManager.Instance.GetSelectedSlot(true);
                    Debug.Log("Легко!");
                    flask.RecoveryMagic();
                }
            }
        }
    }
}
