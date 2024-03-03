using UnityEngine;

public class Lamp : BaseItem
{
    [SerializeField] private GameObject[] visualObjectsOn;
    [SerializeField] private GameObject[] visualObjectsOff;

    private bool bIsOn = true;

    public override void Interact()
    {
        if (bIsOn)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        foreach (GameObject obj in visualObjectsOn)
        {
            obj.SetActive(true);
        }

        foreach(GameObject obj in visualObjectsOff)
        {
            obj.SetActive(false);
        }

        bIsOn = true;
    }

    private void Hide()
    {
        bIsOn = false;

        foreach (GameObject obj in visualObjectsOn)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in visualObjectsOff)
        {
            obj.SetActive(true);
        }
    }
}