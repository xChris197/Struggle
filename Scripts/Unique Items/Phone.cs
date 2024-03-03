
public class Phone : BaseItem
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
                Player.Instance.SetCanPlayerMove(false);
                Player.Instance.SetCanMoveCursor(true);
                CustomEvents.OnInteractWithPhone?.Invoke();
            }
        }
    }

    public void FinishTask()
    {
        if (TryGetComponent(out Task task))
        {
            if (task.GetIsActive())
            {
                Player.Instance.SetCanPlayerMove(true);
                Player.Instance.SetCanMoveCursor(false);
                task.FinishTask();
            }
        }
    }
}
