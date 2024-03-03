using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taskDescription;

    private TaskDataSO taskData;

    private void SetTaskText(TaskDataSO _data)
    {
        ClearTaskData();

        taskData = _data;
        if(taskData != null)
        {
            taskDescription.text = taskData.TaskDescription;
        }
    }

    private void ClearTaskData()
    {
        taskDescription.text = "";
    }

    private void OnEnable()
    {
        CustomEvents.OnGettingNewTask += SetTaskText;
    }

    private void OnDisable()
    {
        CustomEvents.OnGettingNewTask -= SetTaskText;
    }
}
