using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/BaseItem", fileName = "newBaseItem")]
public class Item : ScriptableObject
{
    public GameObject prefab;

    public Sprite sprite;

    public bool stackable;
    public bool isLocked;
   
    
}
