using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer sfxAudioMixer;
    [SerializeField] private AudioMixer themeAudioMixer;

    private float sfxCurrentVol;
    private float themeCurrentVol;

    private const string VOLUME_MIXER_FLOAT = "Volume";
    private const int MIN_VOLUME_VALUE = -80;

    public void SetSFXVolume(float _volume)
    {
        sfxAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, _volume);
    }

    public void SetThemeVolume(float _volume)
    {
        themeAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, _volume);
    }

    //If the player toggles mute then it will keep track of whatever the sliders values were
    //Upon unmuting, it will set the sliders back to whatever value they were at before muting.
    public void SetIsMute(bool _state)
    {
        if(_state)
        {
            sfxAudioMixer.GetFloat(VOLUME_MIXER_FLOAT, out sfxCurrentVol);
            themeAudioMixer.GetFloat(VOLUME_MIXER_FLOAT, out themeCurrentVol);

            sfxAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, MIN_VOLUME_VALUE);
            themeAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, MIN_VOLUME_VALUE);
        }
        else
        {
            sfxAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, sfxCurrentVol);
            themeAudioMixer.SetFloat(VOLUME_MIXER_FLOAT, themeCurrentVol);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
