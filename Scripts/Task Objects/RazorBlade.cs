using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorBlade : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 halfExtents;
    [SerializeField] private LayerMask playerLayer;
    private bool bInRange;

    private float timer;
    [SerializeField] private float timeBetweenPresses = 1.5f;

    private TaskManager taskManager;
    private Interactable interactScript;
    private RazorBlade razorScript;

    [SerializeField] private AudioSource sliceSound;

    [SerializeField] private GameObject[] bloodDecals;
    private int index = 0;

    private void Start()
    {
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
        interactScript = GetComponent<Interactable>();
        razorScript = GetComponent<RazorBlade>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        bInRange = Physics.CheckBox(transform.position + offset, halfExtents, Quaternion.identity, playerLayer);

        if (Input.GetKeyDown(KeyCode.E) && taskManager.bCanUseRazor && bInRange && index < bloodDecals.Length)
        {
            if (timer > timeBetweenPresses)
            {
                timer = 0;
                sliceSound.Play();
                bloodDecals[index].SetActive(true);
                index++;
            }
        }

        if(index >= bloodDecals.Length)
        {
            taskManager.AddToIndex();
            StartCoroutine(taskManager.UpdateTask());
            razorScript.enabled = false;
            interactScript.enabled = true;
        }
    }
}
