using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Animator animFade;

    private void Start()
    {
        animFade = GameObject.Find("Fade").GetComponent<Animator>();
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(48f);
        animFade.SetBool("bFadeOut", true);
        yield return new WaitForSeconds(2.5f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }
}
