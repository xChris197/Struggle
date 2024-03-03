
public class Blade : BaseItem
{
    public override void Interact()
    {
        CustomEvents.OnInteractWithItem?.Invoke(this);
        StartTask();
    }

    private void StartTask()
    {
        if (TryGetComponent(out Task task))
        {
            if (task.GetIsActive())
            {
                if (GetItemDataSO().ItemAudioClip != null)
                {
                    SoundManager.Instance.PlaySFXSound(GetItemDataSO().ItemAudioClip);
                }

                Player.Instance.SetCanPlayerMove(false);
                CustomEvents.OnInteractWithRazor?.Invoke();
            }
        }
    }

    public void FinishTask()
    {
        if (TryGetComponent(out Task task))
        {
            task.FinishTask();
        }
    }
}
