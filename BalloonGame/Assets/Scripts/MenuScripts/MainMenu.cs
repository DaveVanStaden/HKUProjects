using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
/// <summary>
/// manages the main menu.
/// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("BalloonGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
