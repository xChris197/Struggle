using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/TaskData")]
public class TaskDataSO : ScriptableObject
{
    [field: SerializeField] public string TaskDescription { get; private set; } 
}
