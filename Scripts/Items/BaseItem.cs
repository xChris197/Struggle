using UnityEngine;

//The base item script that all items inherit from
public class BaseItem : MonoBehaviour
{
    [SerializeField] private ItemDataSO itemData;
    private Task task;

    public virtual void Interact()
    {
        Debug.LogError("BaseItem Interact has been called!");
    }

    public ItemDataSO GetItemDataSO()
    {
        return itemData;
    }

    public bool IsATaskItem()
    {
        if (!TryGetComponent(out Task _task))
        {
            return false;
        }
        else
        {
            SetTask(_task);
            return true;
        }
    }

    private void SetTask(Task _task)
    {
        task = _task;
    }

    public Task GetCurrentTask()
    {
        return task;
    }
}
