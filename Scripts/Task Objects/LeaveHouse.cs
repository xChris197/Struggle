using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveHouse : MonoBehaviour
{
    private bool bInRange;
    private AudioSource doorSound;

    private GameObject gameManager;
    private GameObject taskManager;

    private TaskManager taskManagerScript;
    private FadeController fadeController;

    [SerializeField] private string creditScene;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        taskManager = GameObject.Find("Task Manager");
        taskManagerScript = taskManager.GetComponent<TaskManager>();
        fadeController = gameManager.GetComponent<FadeController>();
        doorSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && bInRange && taskManagerScript.bCanEndGame)
        {
            StartCoroutine(EndGame());
        }

        if(taskManagerScript.bCanEndGame)
        {
            GetComponent<Interactable>().enabled = false;
        }
    }

    IEnumerator EndGame()
    {
        fadeController.FadeOut();
        doorSound.Play();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(creditScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = false;
        }
    }
}
