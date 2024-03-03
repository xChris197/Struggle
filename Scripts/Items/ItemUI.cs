using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryItemUI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiObjects;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private float letterScrollingSpeed;

    [SerializeField] private AudioClip[] typingSounds;

    private WaitForSeconds waitTime;
    private BaseItem baseItem;

    private void Start()
    {
        waitTime = new WaitForSeconds(letterScrollingSpeed);
    }

    //Checks the Item Type passed in due to some items being both normal descriptive items and task items
    //Checks if a task item is active and if it isn't and can still display something, then do so accordingly.
    private void CheckItemType(BaseItem _item)
    {
        switch (_item.GetItemDataSO().ItemType)
        {
            case ItemType.Task:
                break;

            case ItemType.Story:
                SetupUIElements(_item);
                break;

            case ItemType.Both:
                if (!_item.IsATaskItem())
                {
                    SetupUIElements(_item);
                    break;
                }
                else if (_item.IsATaskItem())
                {
                    Task task = _item.GetCurrentTask();
                    if (!task.GetIsActive())
                    {
                        SetupUIElements(_item);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            default:
                Debug.LogError("No implementation for the type passed in");
                break;
        }

    }

    //Sets up the data needed to display text
    //Calls the Coroutine to display the text via a typing-like effect.
    private void SetupUIElements(BaseItem _item)
    {
        Player.Instance.SetCanPlayerMove(false);

        ClearUIData();
        ShowUI();

        itemName.text = _item.GetItemDataSO().ItemName;
        baseItem = _item;

        Player.Instance.SetPlayerState(PlayerState.Interacting);

        StartCoroutine(TypeItemDescription());
    }

    //Adds each letter of the item description to the text field
    //Yields the code for a tiny amount of time to give a scrolling type effect
    private IEnumerator TypeItemDescription()
    {
        foreach (char letter in baseItem.GetItemDataSO().ItemDescription.ToCharArray())
        {
            itemDescription.text += letter;
            int typingSoundChoice = Random.Range(0, typingSounds.Length - 1);
            SoundManager.Instance.PlaySFXSound(typingSounds[typingSoundChoice]);
            yield return waitTime;
        }

        closeMenuButton.gameObject.SetActive(true);
    }

    private void ShowUI()
    {
        foreach(GameObject obj in uiObjects)
        {
            obj.SetActive(true);
            closeMenuButton.gameObject.SetActive(false);
            Player.Instance.SetCanMoveCursor(true);
        }
    }

    public void HideUI()
    {
        foreach (GameObject obj in uiObjects)
        {
            obj.SetActive(false);
            closeMenuButton.gameObject.SetActive(false);
            ClearUIData();
        }

        Player.Instance.SetCanPlayerMove(true);
        Player.Instance.SetCanMoveCursor(false);
        Player.Instance.SetPlayerState(PlayerState.Idle);
    }

    private void ClearUIData()
    {
        itemName.text = "";
        itemDescription.text = "";
    }

    private void OnEnable()
    {
        CustomEvents.OnInteractWithItem += CheckItemType;
    }

    private void OnDisable()
    {
        CustomEvents.OnInteractWithItem -= CheckItemType;
    }
}
