using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : BaseItem
{
    private const string animTriggerName = "bOpening";

    [SerializeField] private bool bIsOpen = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        bIsOpen = !bIsOpen;
        anim.SetBool(animTriggerName, bIsOpen);

        if(GetItemDataSO().ItemAudioClip != null)
        {
            SoundManager.Instance.PlaySFXSound(GetItemDataSO().ItemAudioClip);
        }
    }
}
