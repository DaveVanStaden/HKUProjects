using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdministrator : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }
    //close application
    public void QuitGame()
    {
        Application.Quit();
    }
    //reload the game.
    public void ReloadScene()
    {
        SceneManager.LoadScene("OtterGame");
    }
    //load the main menu.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
