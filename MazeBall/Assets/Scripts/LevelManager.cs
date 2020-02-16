using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Summary: This very basic script allows us to navigate between scenes.

    private static bool created = false;

    public void LoadLevel(string levelName)
    {
        //Loads the scene with the the same name as 'levelName'.
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        //Turns the game off.
        Application.Quit();
    }

    void Awake()
    {
        //Makes sure that the levelmanager doesn't get destroyed when moving to another scene.
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}