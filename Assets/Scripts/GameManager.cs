using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gamePaused = false;

    public void TogglePauseState()
    {
        if (gamePaused)
            InputActionsManager.Instance.EnableInteractions();
        else
            InputActionsManager.Instance.DisableInteractions();

        Time.timeScale = gamePaused ? 1f : 0f;
        gamePaused = !gamePaused;
    }

    public void CloseGame() => Application.Quit();
}
