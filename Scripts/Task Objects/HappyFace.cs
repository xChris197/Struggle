using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class HappyFace : MonoBehaviour
{
    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private string promptMessage;
    private TaskManager taskManager;

    [SerializeField] private Volume ppEffects;
    [SerializeField] private Volume vignetteEffect;

    private bool bInRange;

    private void Start()
    {
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bInRange && taskManager.bCanUseMask)
        {
            taskManager.AddToIndex();
            StartCoroutine(taskManager.UpdateTask());
            ppEffects.enabled = false;
            vignetteEffect.enabled = true;
            HidePrompt();
        }
    }

    void HidePrompt()
    {
        promptText.text = "";
        promptBox.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptBox.SetActive(true);
            bInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptBox.SetActive(false);
            bInRange = false;
        }
    }

}
