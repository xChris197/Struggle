using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource themeSource;

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

    public void PlaySFXSound(AudioClip _clip)
    {
        if(_clip == null)
        {
            sfxSource.Stop();
            sfxSource.clip = null;
        }

        sfxSource.clip = _clip;
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void PlayThemeSound(AudioClip _clip)
    {
        if (_clip == null)
        {
            sfxSource.Stop();
            sfxSource.clip = null;
        }

        themeSource.clip = _clip;
        themeSource.Play();
    }
}
