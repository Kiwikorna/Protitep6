using UnityEngine;
using TMPro;
public class UIPlayerHealth : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private CharacterHealthSO player;
    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + player.health.ToString();
    }
}
