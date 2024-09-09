using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player/Attack",fileName = "newPlayerAttack")]
public class PlayerAttackSO : ScriptableObject
{
    public GameObject prefab;

    public int attackDamage;

    public int manaFlow;

    public int speed;

}
