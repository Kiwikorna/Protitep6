using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/BaseItem", fileName = "newBaseItem")]
public class ItemObject : ScriptableObject
{
    public GameObject prefab;

    public Sprite sprite;

    public bool stackable;
    public Item item;

    public enum Item
    {
        Spell,
        Subject
    }
    
}
