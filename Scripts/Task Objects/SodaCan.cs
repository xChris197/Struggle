using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SodaCan : MonoBehaviour
{
    private TaskManager taskManager;
    private bool bInRange;

    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private AudioSource drinkSound;

    private void Start()
    {
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bInRange && taskManager.bCanUseFridge)
        {
            taskManager.AddToIndex();
            drinkSound.Play();
            StartCoroutine(taskManager.UpdateTask());
            promptBox.SetActive(false);
            promptText.text = "";
        }
    }

    public void SetPrompt(string promptText)
    {
        this.promptText.text = promptText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = true;
            promptBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = false;
            promptBox.SetActive(false);
        }
    }
}
