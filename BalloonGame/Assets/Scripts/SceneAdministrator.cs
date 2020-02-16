using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdministrator : MonoBehaviour
{
    /// <summary>
    /// manages the end game menu.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("BalloonGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
