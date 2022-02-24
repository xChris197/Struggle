using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 1.3f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 halfExtents;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float typeSpeed = 0.1f;

    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueTextDisplay;

    private TaskManager taskManager;

    [TextArea]
    [SerializeField] private string[] dialogueSentences;
    private int index = 0;

    [SerializeField] private GameObject dialogueButton;

    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCam;
    private bool bInRange;
    private bool bDialogueInUse = false;

    private MeshRenderer rend;
    private BoxCollider col;

    void Start()
    {
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        rend = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();
    }

    void Update()
    {
        //bInRange = Physics.CheckSphere(transform.position + offset, radius, playerLayer);
        bInRange = Physics.CheckBox(transform.position + offset, halfExtents, Quaternion.identity, playerLayer);
        if(Input.GetKeyDown(KeyCode.E) && bInRange && !bDialogueInUse)
        {
            HidePrompt();
            taskManager.DisablePlayer();
            StartCoroutine(SetDialogue());
        }
    }

    //Sets the text for the button prompt & displays the prompt
    public void SetPrompt(string promptText)
    {
        this.promptText.text = promptText;
    }

    //Sets the text for the dialogue prompt & displays the dialogue
    IEnumerator SetDialogue()
    {
        bDialogueInUse = true;
        Cursor.lockState = CursorLockMode.None;

        dialogueBox.SetActive(true);
        foreach(char letter in dialogueSentences[index].ToCharArray())
        {
            dialogueTextDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        dialogueButton.SetActive(true);
    }

    //Hides text prompt and deletes the content of the text field
    void HidePrompt()
    {
        promptText.text = "";
        promptBox.SetActive(false);
    }

    //Resets the dialogue box so it can be used again if the object is activated again
    public void ResetDialogue()
    {
        bDialogueInUse = false;
        dialogueTextDisplay.text = "";
        dialogueBox.SetActive(false);
        dialogueButton.SetActive(false);

        taskManager.EnablePlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptBox.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            promptBox.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
