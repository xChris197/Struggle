using UnityEngine;

public class TV : BaseItem
{
    [SerializeField] private Material tvOffMaterial;
    [SerializeField] private Material tvOnMaterial;

    [SerializeField] private AudioSource staticSFX;

    private Renderer rend;
    private bool bIsOn = true;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        staticSFX = GetComponent<AudioSource>();
    }

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
        rend.material = tvOnMaterial;
        staticSFX.enabled = true;

        bIsOn = true;
    }

    private void Hide()
    {
        rend.material = tvOffMaterial;
        staticSFX.enabled = false;
        FinishTask();
        bIsOn = false;
    }

    public void FinishTask()
    {
        if(TryGetComponent(out Task task))
        {
            if (task.GetIsActive())
            {
                task.FinishTask();
            }
        }
    }
}
