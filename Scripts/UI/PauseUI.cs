using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseUIElements;

    private void Start()
    {
        Hide();
    }

    private void SetGamePausedState(bool _state)
    {
        if(_state)
        {
            Player.Instance.SetCanMoveCursor(true);
            Player.Instance.SetCanPlayerMove(false);
            Show();
        }
        else
        {
            Player.Instance.SetCanMoveCursor(false);
            Player.Instance.SetCanPlayerMove(true);
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject obj in pauseUIElements)
        {
            obj.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach(GameObject obj in pauseUIElements)
        {
            obj.SetActive(false);
        }
    }

    private void OnEnable()
    {
        CustomEvents.OnSetIsGamePaused += SetGamePausedState;
    }

    private void OnDisable()
    {
        CustomEvents.OnSetIsGamePaused -= SetGamePausedState;
    }
}
