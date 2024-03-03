using UnityEngine;

public enum ItemType { Story, Task, Both};

[CreateAssetMenu(menuName = "Scriptable Objects/ItemData")]
public class ItemDataSO : ScriptableObject
{
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public string ItemDescription { get; private set; }
    [field: SerializeField] public AudioClip ItemAudioClip { get; private set; }
}
