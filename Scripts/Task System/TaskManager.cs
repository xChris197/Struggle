using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private Task[] task;

    [SerializeField] private float textScreenTime;
    [SerializeField] private float textWaitTime;

    [SerializeField] private TextMeshProUGUI taskDescription;
    [SerializeField] private int index = 0;

    [SerializeField] private GameObject[] taskObjectsToEnable;

    private GameObject player;
    [SerializeField] private Camera mainCam;

    [SerializeField] private BoxCollider endDoorCol;

    [HideInInspector]public bool bCanUseTV = true;
    [HideInInspector] public bool bCanUseFridge = false;
    [HideInInspector] public bool bCanUseRazor = false;
    [HideInInspector] public bool bCanUsePhone = false;
    [HideInInspector] public bool bCanUseMask = false;
    [HideInInspector] public bool bCanEndGame = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Adds onto the index in order to show different task texts that UpdateTask() uses
    public void AddToIndex()
    {
        index++;
    }

    //Shows the task text
    public IEnumerator UpdateTask()
    {
        switch (index)
        {
            case 1:
                bCanUseTV = false;
                bCanUseFridge = true;
                taskObjectsToEnable[index - 1].GetComponent<Interactable>().enabled = true;
                taskObjectsToEnable[index - 1].GetComponent<DoorController>().enabled = true;
                taskObjectsToEnable[index].GetComponent<DisableObject>().enabled = true;
                taskObjectsToEnable[index].GetComponent<BoxCollider>().enabled = true;
                break;
            case 2:
                bCanUseFridge = false;
                bCanUseRazor = true;
                taskObjectsToEnable[index].GetComponent<Interactable>().enabled = false;
                taskObjectsToEnable[index].GetComponent<RazorBlade>().enabled = true;
                break;
            case 3:
                bCanUseRazor = false;
                bCanUsePhone = true;
                taskObjectsToEnable[index].GetComponent<PhoneConversation>().enabled = true;
                taskObjectsToEnable[index].GetComponent<BoxCollider>().enabled = true;
                break;
            case 4:
                bCanUsePhone = false;
                bCanUseMask = true;
                taskObjectsToEnable[index].GetComponent<HappyFace>().enabled = true;
                taskObjectsToEnable[index].GetComponent<DisableObject>().enabled = true;
                taskObjectsToEnable[index].GetComponent<BoxCollider>().enabled = true;
                break;
            case 5:
                bCanUseMask = false;
                bCanEndGame = true;
                taskObjectsToEnable[index].GetComponent<Interactable>().enabled = false;
                taskObjectsToEnable[index].GetComponent<LeaveHouse>().enabled = true;
                taskObjectsToEnable[index - 3].GetComponent<Suicide>().enabled = true;
                taskObjectsToEnable[index - 3].GetComponent<BoxCollider>().enabled = true;
                taskObjectsToEnable[index - 3].GetComponent<Interactable>().enabled = false;
                endDoorCol.enabled = true;
                break;
        }

        yield return new WaitForSeconds(textWaitTime);
        taskDescription.text = task[index].description;
        taskDescription.enabled = true;
        yield return new WaitForSeconds(textScreenTime);
        taskDescription.enabled = false;

    }

    public void DisablePlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        mainCam.GetComponent<PlayerLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnablePlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        mainCam.GetComponent<PlayerLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
