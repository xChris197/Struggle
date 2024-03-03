using System;

public static class CustomEvents
{
    public static Action<BaseItem> OnInteractWithItem;
    public static Action<ItemDataSO> OnInteractableObjectSelected;

    public static Action OnStandingInFrontOfMirror;
    public static Action OnLeaveMirror;

    public static Action<TaskDataSO> OnGettingNewTask;

    public static Action OnInteractWithPhone;

    public static Action OnInteractWithRazor;

    public static Action<bool> OnSetIsGamePaused;
    public static Action OnCheckCanPauseGame;

    public static Action OnGameFinished;
}
