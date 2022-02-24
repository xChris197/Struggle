using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TVController : MonoBehaviour
{
    private bool bIsOn = true;
    private TaskManager taskManager;
    private bool bInRange;

    private AudioSource staticSound;
    [SerializeField] private Light tvLight;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private GameObject promptBox;
    [SerializeField] private TextMeshProUGUI promptText;

    private Renderer rend;
    [SerializeField] private GameObject screen;
    [SerializeField] private Material tvOff;
    [SerializeField] private Material tvOn;

    //[SerializeField] private 

    void Start()
    {
        rend = screen.GetComponent<Renderer>();
        taskManager = GameObject.Find("Task Manager").GetComponent<TaskManager>();
        staticSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bInRange && bIsOn && taskManager.bCanUseTV)
        {
            TurnOff();
            taskManager.AddToIndex();
            StartCoroutine(taskManager.UpdateTask());
        }
        else if(Input.GetKeyDown(KeyCode.E) && bInRange && bIsOn && !taskManager.bCanUseTV)
        {
            TurnOff();
        }
        else if(Input.GetKeyDown(KeyCode.E) && bInRange && !bIsOn && !taskManager.bCanUseTV)
        {
            TurnOn();
        }

    }

    public void SetPrompt(string promptText)
    {
        this.promptText.text = promptText;
    }

    void TurnOn()
    {
        bIsOn = true;
        rend.material = tvOn;
        staticSound.enabled = true;
        staticSound.Play();
        tvLight.enabled = true;
    }

    void TurnOff()
    {
        bIsOn = false;
        rend.material = tvOff;
        staticSound.Stop();
        staticSound.enabled = false;
        tvLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = true;
            promptBox.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bInRange = false;
            promptBox.SetActive(false);
        }
    }
}
