using UnityEngine;

public class IntaractableItem : MonoBehaviour, Interactable
{
    [SerializeField] private Item item;
    public void Intarection()
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
