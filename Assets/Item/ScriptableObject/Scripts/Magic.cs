using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item/ManaItem", fileName = "newManaItem")]
public class Magic : Item,IManaFlask
{
    public float manaItem;
    public float ManaValue
    {
        get => manaItem;
        set => value = manaItem;
    }
}
