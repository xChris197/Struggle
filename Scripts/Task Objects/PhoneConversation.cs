using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneConversation : MonoBehaviour
{
    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private string promptMessage;

    [SerializeField] private BoxCollider triggerBox;

    private TaskManager taskManager;
    private bool bInRange;
    private Renderer rend;
    [SerializeField] private Material phoneMessage;
    [SerializeField] private Material phoneOff;

    [SerializeField] private GameObject convoBackground;
    [SerializeField] private GameObject convoParent;

    [TextArea]
    [SerializeField] private string[] sentences;

    [SerializeField] private GameObject[] messageBubbles;
    [SerializeField] private TextMeshProUGUI[] sentenceFields;

    [SerializeField] private GameObject replyButton;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private float typingSpeed;
    [SerializeField] private int index = 0;

    [SerializeField] private AudioSource typeSound;

    void Start()
    {
        rend = GetComponent<Renderer>();
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
    }

    void Update()
    {
        if(taskManager.bCanUsePhone)
        {
            rend.material = phoneMessage;
        }

        if (Input.GetKeyDown(KeyCode.E) && taskManager.bCanUsePhone && bInRange)
        {
            taskManager.DisablePlayer();
            convoBackground.SetActive(true);
            StartCoroutine(TypingEffect());
        }

        if(index >= sentences.Length - 1)
        {
            exitButton.SetActive(true);
            replyButton.SetActive(false);
        }
    }

    IEnumerator TypingEffect()
    {
        yield return new WaitForSeconds(1.5f);
        messageBubbles[index].SetActive(true);
        sentenceFields[index].enabled = true;
        foreach(char letter in sentences[index].ToCharArray())
        {
            sentenceFields[index].text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (index % 2 == 0)
        {
            replyButton.SetActive(true);
        }
        else
        {
            NextSentence();
        }
    }

    public void NextSentence()
    {
        replyButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypingEffect());
            typeSound.Play();
        }
    }

    public void ExitConversation()
    {
        taskManager.AddToIndex();
        StartCoroutine(taskManager.UpdateTask());
        taskManager.EnablePlayer();
        convoParent.SetActive(false);
        rend.material = phoneOff;
        GetComponent<PhoneConversation>().enabled = false;
        triggerBox.enabled = false;
        HidePrompt();
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
