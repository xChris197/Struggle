using System.Collections;
using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    [SerializeField] private GameObject[] messageBubbles;
    [SerializeField] private GameObject[] playerMessageBubbles;
    [SerializeField] private GameObject phoneBackground;

    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject replyButton;

    [SerializeField] private AudioClip messageSentSFX;

    [SerializeField] private float timeBetweenTexts;

    private WaitForSeconds waitTime;
    private int index = 0;

    private void Start()
    {
        waitTime = new WaitForSeconds(timeBetweenTexts);
    }

    private void StartPhoneTask()
    {
        phoneBackground.SetActive(true);
        replyButton.SetActive(false);
        Player.Instance.SetPlayerState(PlayerState.Interacting);
        StartCoroutine(SendNextText());
    }

    public void AdvanceTexts()
    {
        SoundManager.Instance.PlaySFXSound(messageSentSFX);
        playerMessageBubbles[index].SetActive(true);
        index++;
        replyButton.SetActive(false);
        StartCoroutine(SendNextText());
    }

    private IEnumerator SendNextText()
    {
        yield return waitTime;
        SoundManager.Instance.PlaySFXSound(messageSentSFX);
        messageBubbles[index].SetActive(true);

        if (index < messageBubbles.Length - 1)
        {
            yield return waitTime;
            replyButton.SetActive(true);
        }
        else
        {
            closeButton.SetActive(true);
        }
    }

        private void OnEnable()
    {
        CustomEvents.OnInteractWithPhone += StartPhoneTask;
    }

    private void OnDisable()
    {
        CustomEvents.OnInteractWithPhone -= StartPhoneTask;
    }
}
