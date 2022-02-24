using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAbovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject objectToDisplay;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            objectToDisplay.GetComponent<Canvas>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToDisplay.GetComponent<Canvas>().enabled = false;
        }
    }
}
