using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    private TaskManager taskManager;
    private bool bGameIsPaused = false;

    [SerializeField] private GameObject[] menus;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string sceneName;

    private void Start()
    {
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !bGameIsPaused)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && bGameIsPaused)
        {
            UnPauseGame();
        }
    }

    void PauseGame()
    {
        bGameIsPaused = true;
        taskManager.DisablePlayer();
        pauseMenu.SetActive(true);
    }

    public void UnPauseGame()
    {
        bGameIsPaused = false;
        taskManager.EnablePlayer();
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
