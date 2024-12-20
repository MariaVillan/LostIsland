using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    public LayerMask interactableLayers;
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;

    public void Update()
    {
        if (Input.GetKeyDown(interactionKey))
            Interact();
    }

    private void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, interactableLayers))
        {
            Pickup pickup = hit.transform.GetComponent<Pickup>();

            if (pickup != null)
            {
                GetComponentInParent<WindowHandler>().inventory.AddItem(pickup);
            }
        }
    }
}
