using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [SerializeField] private List<Task> taskList;

    private Task currentTask;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GetNextTask();
    }

    //Returns a Task and sets it as Active
    public void GetNextTask()
    {
        if(taskList.Count > 0)
        {
            Task task = taskList[0];
            taskList.Remove(task);
            task.SetIsActive(true);

            currentTask = task;

            CustomEvents.OnGettingNewTask?.Invoke(currentTask.GetTaskData());
        }
        else
        {
            Debug.LogWarning("There are no more tasks to return");
        }
    }
}
