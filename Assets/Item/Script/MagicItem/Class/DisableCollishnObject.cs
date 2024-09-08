using System;
using UnityEngine;

public class DisableCollishnObject : MonoBehaviour
{

    // Коллайдер текущего объекта (предмета)
    private Collider thisCollider;

    private void Awake()
    {
        // Кэшируем коллайдер этого объекта
        thisCollider = GetComponent<Collider>();
        if (thisCollider == null)
        {
            Debug.LogError("Collider component not found on this game object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Debug.Log("Персонаж теперь может проходить сквозь объект!");
            Collider playerCollider = other.GetComponent<Collider>();

            if (playerCollider != null && thisCollider != null)
            {
               
                Physics.IgnoreCollision(playerCollider, thisCollider, true);
                Debug.Log("Персонаж теперь может проходить сквозь объект!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            
            Collider playerCollider = other.GetComponent<Collider>();

            if (playerCollider != null && thisCollider != null)
            {
               
                Physics.IgnoreCollision(playerCollider, thisCollider, false);
                Debug.Log("Персонаж больше не может проходить сквозь объект.");
            }
        }
    }
}
