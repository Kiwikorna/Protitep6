using UnityEngine;

public class MagicFlask : MonoBehaviour
{
    [SerializeField] private PlayerMagicSO player;
    [SerializeField] private MagicObject magicObject;
    


    public void RecoveryMagic()
    {
        player.manaPlayer += magicObject.manaItem;
       
    }
        
}
