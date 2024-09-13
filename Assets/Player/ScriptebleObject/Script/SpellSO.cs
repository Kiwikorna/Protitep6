using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Player/Attack",fileName = "newPlayerAttack")]
public class SpellSO : ScriptableObject
{
    public GameObject prefab;

    public int attackDamage;

     public int manaCost;

    public int speed;

}
