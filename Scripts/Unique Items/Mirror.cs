using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private AudioClip heartbeatSFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            CustomEvents.OnStandingInFrontOfMirror?.Invoke();
            SoundManager.Instance.PlaySFXSound(heartbeatSFX);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            CustomEvents.OnLeaveMirror?.Invoke();
            SoundManager.Instance.PlaySFXSound(null);
        }
    }
}
