
public class SodaCan : BaseItem
{
    public override void Interact()
    {
        FinishTask();
    }

    public void FinishTask()
    {
        if (!TryGetComponent(out Task task))
        {
            return;
        }

        if (task.GetIsActive())
        {
            if (GetItemDataSO().ItemAudioClip != null)
            {
                SoundManager.Instance.PlaySFXSound(GetItemDataSO().ItemAudioClip);
            }

            task.FinishTask();
            Destroy(gameObject);
        }
    }

}
