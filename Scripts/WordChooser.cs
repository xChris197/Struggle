using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordChooser : MonoBehaviour
{
    [SerializeField] private string[] wordsToPick;
    [SerializeField] private TextMeshProUGUI[] wordDisplays;

    [SerializeField] private float delay = 0.2f;

    private bool bStartGeneration = false;

    void Update()
    {
        if(!bStartGeneration)
        {
            StartCoroutine(WordGeneration());
        }
    }

    IEnumerator WordGeneration()
    {
        bStartGeneration = true;
        foreach (TextMeshProUGUI word in wordDisplays)
        {
            int num = Random.Range(0, wordsToPick.Length - 1);
            string wordPicked = wordsToPick[num];
            word.text = wordPicked;
        }
        yield return new WaitForSeconds(delay);
        bStartGeneration = false;
    }
}
