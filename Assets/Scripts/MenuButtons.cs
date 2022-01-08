using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void StartOver()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        Assets.Scripts.Model.GlobalBar.Reset();
    }
}
