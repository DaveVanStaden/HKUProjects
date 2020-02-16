using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdministrator : MonoBehaviour
{
    private static bool created = false;

    public void QuitGame()
    {
        Application.Quit();
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    internal static void LoadScene(string v)
    {
        throw new NotImplementedException();
    }
}