using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Character/Health", fileName = "NewPlayerHealth")]
public class CharacterHealthSO : ScriptableObject
{
     public float healthValue;
}
    
