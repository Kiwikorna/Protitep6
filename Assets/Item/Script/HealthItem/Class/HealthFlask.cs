using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    [SerializeField] private PlayerHealthSO person;
    [SerializeField] private HealthObject health;


    public void Health()
    {
            person.health += health.healthFlask;
    }
}
