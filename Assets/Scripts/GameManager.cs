using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    public void CloseGame() => Application.Quit();
}
