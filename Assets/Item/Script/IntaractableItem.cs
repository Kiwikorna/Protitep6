using UnityEngine;

public class IntaractableItem : MonoBehaviour, Interactable
{
    [SerializeField] private Item item;
    public void Interaction()
    {
        if (PlayerInput.Instance.GetInteractionHandler())
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
        
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
