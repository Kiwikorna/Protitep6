using System;
using UnityEngine;
using TMPro;
public class UIPlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerMagicSO player;
    [SerializeField] private TextMeshProUGUI text;


    private void Update()
    {
        text.text = "Mana: " + player.manaPlayer.ToString();
    }
}
