using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private string promptText;
    [SerializeField] private float rayDistance;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            Interactable interactableGO = hit.collider.GetComponent<Interactable>();
            DoorController doorControllerGO = hit.collider.GetComponent<DoorController>();
            TVController tvControllerGO = hit.collider.GetComponent<TVController>();
            if(interactableGO != null)
            {
                interactableGO.SetPrompt(promptText);
            }
            if(doorControllerGO != null)
            {
                doorControllerGO.SetPrompt(promptText);
            }

            if(tvControllerGO != null)
            {
                tvControllerGO.SetPrompt(promptText);
            }
        }

    }
}