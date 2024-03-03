using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] private float timeToWait;

    private WaitForSeconds waitTime;

    private void Awake()
    {
        waitTime = new WaitForSeconds(timeToWait);
    }

    private void Start()
    {
        StartCoroutine(HeadBackToMainMenu());
    }

    private IEnumerator HeadBackToMainMenu()
    {
        yield return waitTime;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
