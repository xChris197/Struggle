using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private TextMeshProUGUI introPrompt;

    private FadeController fadeController;
    private TaskManager taskManager;
    private GameObject player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        fadeController = GameObject.Find("Game Manager").GetComponent<FadeController>();
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
        fadeController.StartingFade();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        fadeController.FadeOut();
        introPrompt.enabled = false;
        yield return new WaitForSeconds(2.5f);
        mainCam.enabled = true;
        GetComponent<Camera>().enabled = false;
        fadeController.FadeIn();
        yield return new WaitForSeconds(2.5f);
        taskManager.EnablePlayer();
        StartCoroutine(taskManager.UpdateTask());
        GetComponent<IntroController>().enabled = false;
    }
}
