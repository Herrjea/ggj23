using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] float radius = 1;

    [SerializeField] LayerMask interactableLayer;


    private void Awake()
    {
        GameEvents.InteractionRequested.AddListener(InteractionRequested);
    }


    void InteractionRequested()
    {
        Collider2D[] found = Physics2D.OverlapCircleAll(transform.position, radius, interactableLayer);

        if (found.Length > 0)
        {
            int closestEntityIndex = GetClosestIndex(found);
            if (found[closestEntityIndex].TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
            else
                Debug.LogWarning("Object in Interactable layer that doesn't implement IInteractable. Cuidao.");
        }
    }


    int GetClosestIndex(Collider2D[] found)
    {
        int closestIndex = 0;
        float closestDistance = Vector3.Distance(found[0].transform.position, transform.position);
        float newDistance;

        for (int i = 1; i < found.Length; i++)
        {
            newDistance = Vector3.Distance(found[i].transform.position, transform.position);

            if (newDistance < closestDistance)
            {
                closestIndex = i;
                closestDistance = newDistance;
            }
        }

        return closestIndex;
    }
}
