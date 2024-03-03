using UnityEngine;
using TMPro;

public class InsultPicker : MonoBehaviour
{
    [SerializeField] private string[] wordList;

    [SerializeField] private TextMeshProUGUI[] wordDisplays;

    [SerializeField] private float maxWordChangeTimer;
    private float currentTimer;

    private Canvas worldCanvas;

    private void Awake()
    {
        worldCanvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        Mathf.Clamp(currentTimer, 0f, maxWordChangeTimer);

        if(currentTimer >= maxWordChangeTimer)
        {
            GenerateWordsForCanvas();
            currentTimer = 0f;
        }
    }

    private void GenerateWordsForCanvas()
    {
        for(int i = 0; i < wordDisplays.Length; i++)
        {
            int wordChoice = Random.Range(0, wordList.Length);
            wordDisplays[i].text = wordList[wordChoice];
        }
    }

    private void Show()
    {
        worldCanvas.enabled = true;
    }
    private void Hide()
    {
        worldCanvas.enabled = false;
    }

    private void OnEnable()
    {
        CustomEvents.OnStandingInFrontOfMirror += Show;
        CustomEvents.OnLeaveMirror += Hide;
    }

    private void OnDisable()
    {
        CustomEvents.OnStandingInFrontOfMirror -= Show;
        CustomEvents.OnLeaveMirror -= Hide;
    }


}
