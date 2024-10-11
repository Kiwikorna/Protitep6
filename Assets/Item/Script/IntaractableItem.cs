using UnityEngine;
using UnityEngine.Serialization;

public class IntaractableItem : MonoBehaviour, Interactable
{
    [SerializeField] private ItemInInventory itemInInventory;
    public void Interaction()
    {
        if (ControllerPlayer.Instance.GetInteractionHandler())
        {
            InventoryManager.Instance.AddItem(itemInInventory);
            Destroy(gameObject);
        }
        
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
