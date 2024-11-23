using UnityEngine;

public class ResultCombo : MonoBehaviour
{
    [SerializeField] private GameObject resultSlotPrefab;

    public void AddItemToSlot(SpellItem spellItem)
    {
        if (transform.childCount == 0) // Проверяем, что слот пустой
        {
            var newSpellResult = Instantiate(resultSlotPrefab, transform);
            InventoryItem itemResult = newSpellResult.GetComponent<InventoryItem>();
            itemResult.ImageIntializedAndRefreshItem(spellItem);
        }
    }

    public void ClearSlot()
    {
        
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject); // Удаляем все дочерние объекты
            }
        
    }

    public InventoryItem GetChildrenItem() => GetComponentInChildren<InventoryItem>();
}