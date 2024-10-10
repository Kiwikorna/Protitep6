using System;
using UnityEngine;
using TMPro;
public class UIPlayerMana : MonoBehaviour
{
    [SerializeField] private CharacterManaSO player;
    [SerializeField] private TextMeshProUGUI text;


    private void Update()
    {
        text.text = "Mana: " + player.manaCharacterValue.ToString();
    }
}
