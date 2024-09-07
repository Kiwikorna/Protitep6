using UnityEngine;
using TMPro;
public class UIPlayerHealth : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private PlayerHealthSO player;
    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + player.health.ToString();
    }
}
