using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private AudioClip fallingSFX;

    private const string IS_WALKING = "bIsWalking";
    private const string ON_INTERACTED = "OnInteracted";

    private WaitForSeconds waitTime;


    private Animator anim;
    [SerializeField] private Animator camAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        waitTime = new WaitForSeconds(2f);
    }

    private void Update()
    {
        anim.SetBool(IS_WALKING, Player.Instance.IsMoving());
    }

    //Calls the death animation for the final task
    //Plays the appropriate sound effects
    private void HandleCameraDeathAnimation()
    {
        Player.Instance.SetPlayerState(PlayerState.Interacting);
        camAnim.enabled = true;
        camAnim.SetTrigger(ON_INTERACTED);
        SoundManager.Instance.PlaySFXSound(fallingSFX);
        Player.Instance.RenderPlayerBody(false);
        CustomEvents.OnGameFinished?.Invoke();
    }

    private void FinishGameAnimation()
    {
        StartCoroutine(GameEndFade());
    }

    private IEnumerator GameEndFade()
    {
        yield return waitTime;
        FadeUI.Instance.ToggleFadeOut();
        yield return waitTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnEnable()
    {
        CustomEvents.OnInteractWithRazor += HandleCameraDeathAnimation;
        CustomEvents.OnGameFinished += FinishGameAnimation;
    }

    private void OnDisable()
    {
        CustomEvents.OnInteractWithRazor -= HandleCameraDeathAnimation;
        CustomEvents.OnGameFinished += FinishGameAnimation;
    }
}
