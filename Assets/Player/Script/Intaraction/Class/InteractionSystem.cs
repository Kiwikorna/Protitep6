using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private void Update()
    {
        Interactable interable = GetInteractionObject();
        
            if (interable != null)
            {
                interable.Intarection();
                
            }

        
    }

    private Interactable GetInteractionObject()
    {
        List<Interactable> listInteraction = new List<Interactable>();

        float interactionDistance = 2f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interaction))
            {
                listInteraction.Add(interaction);
            }
        }

        Interactable clouseInteraction = null;

        foreach (Interactable interaction in listInteraction)
        {
            if (clouseInteraction == null)
            {
                clouseInteraction = interaction;
            }
            else
            {
                if (Vector3.Distance(transform.position, interaction.GetTransform().position) <
                    Vector3.Distance(transform.position, clouseInteraction.GetTransform().position))
                {
                    clouseInteraction = interaction;
                }
            }
                
        }

        return clouseInteraction;
    }
}
