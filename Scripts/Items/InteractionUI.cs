using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;

    private const string INTERACTION_SUFFIX = ": E To Interact";

    private ItemDataSO itemData;

    private void SetInteractionText(ItemDataSO _item)
    {
        itemData = _item;

        if (itemData != null)
        {
            interactionText.text = itemData.name + INTERACTION_SUFFIX;

        }
        else
        {
            interactionText.text = "";
        }
    }

    private void OnEnable()
    {
        CustomEvents.OnInteractableObjectSelected += SetInteractionText;
    }

    private void OnDisable()
    {
        CustomEvents.OnInteractableObjectSelected -= SetInteractionText;
    }
}
