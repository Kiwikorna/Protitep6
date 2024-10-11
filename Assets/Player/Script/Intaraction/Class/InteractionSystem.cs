using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
  
    private void Update()
    {
        Interactable interactable = GetInteractionObject();
        
            if (interactable != null)
            {
                interactable.Interaction();
                
            }

        
    }

    private Interactable GetInteractionObject()
    {
        List<Interactable> interactions = new List<Interactable>();

        float distance = 2f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interaction))
            {
                interactions.Add(interaction);
            }
        }

        Interactable interacthionClose = null;

        foreach (Interactable interaction in interactions)
        {
            if (interacthionClose == null)
            {
                interacthionClose = interaction;
            }
            else
            {
                if (Vector3.Distance(transform.position, interaction.GetTransform().position) <
                    Vector3.Distance(transform.position, interacthionClose.GetTransform().position))
                {
                    interacthionClose = interaction;
                }
            }
                
        }

        return interacthionClose;
    }
}
