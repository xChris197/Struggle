using UnityEngine;

public class Dryer : BaseItem
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        CustomEvents.OnInteractWithItem?.Invoke(this);
        FinishTask();
    }

    public void FinishTask()
    {
        if (TryGetComponent(out Task task))
        {
            if (task.GetIsActive())
            {
                if (GetItemDataSO().ItemAudioClip != null)
                {
                    audioSource.clip = GetItemDataSO().ItemAudioClip;
                    audioSource.enabled = true;
                    audioSource.Play();
                }
                task.FinishTask();
            }
        }
    }
}
