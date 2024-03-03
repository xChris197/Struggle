using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] private TaskDataSO taskData;
    [SerializeField] private bool bIsActive = false;
    [SerializeField] private bool bIsFinished = false;

    public TaskDataSO GetTaskData()
    {
        return taskData;
    }

    public void FinishTask()
    {
        if (!bIsFinished && bIsActive)
        {
            TaskManager.Instance.GetNextTask();
            bIsFinished = true;
            bIsActive = false;
        }
    }

    public void SetIsActive(bool _state)
    {
        bIsActive = _state;
    }

    public bool GetIsActive()
    {
        return bIsActive;
    }
}
