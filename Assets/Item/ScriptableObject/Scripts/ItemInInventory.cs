using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Item/BaseItem", fileName = "newBaseItem")]
public class ItemInInventory : ScriptableObject
{
    public GameObject prefabDrop;
    public Sprite sprite;
    public bool stackable;
    public bool isLocked;

}
