using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    private bool bDoorOpen;
    [SerializeField] private AudioSource openDoorSound;

    [SerializeField] private Animator anim;
    private bool bInRange;
    private bool bGotAnimatorComponent;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private string animBoolName;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && bInRange && bDoorOpen)
        {
            CloseDoor();
        }
        else if(Input.GetKeyDown(KeyCode.E) && bInRange && !bDoorOpen)
        {
            OpenDoor();
        }

    }

    public void SetPrompt(string promptText)
    {
        this.promptText.text = promptText;
    }

    void OpenDoor()
    {
        bDoorOpen = true;
        anim.SetBool(animBoolName, true);
        openDoorSound.Play();
    }

    void CloseDoor()
    {
        bDoorOpen = false;
        anim.SetBool(animBoolName, false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = true;
            promptBox.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = false;
            promptBox.SetActive(false);
        }
    }
}
