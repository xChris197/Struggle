using UnityEngine;

public class FadeUI : MonoBehaviour
{
    public static FadeUI Instance { get; private set; }

    private const string FADE_TRIGGER = "FinishedGame";

    [SerializeField] private Animator anim;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void ToggleFadeOut()
    {
        anim.SetTrigger(FADE_TRIGGER);
    }
}
