using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ManaItem", fileName = "newManaItem")]
public class MagicObject : ItemObject
{
    public int manaItem;
}
