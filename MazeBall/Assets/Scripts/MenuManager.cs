using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text sizeText;
    public static int sizeIndicator;
    public static bool leftOrRight = true;
    [SerializeField] private GameObject inGameToggle;
    // Update is called once per frame
    void Update()
    {
        sizeText.text = "The maze size is:" + MazeManager.rowColManager;
        if (leftOrRight)
        {
            inGameToggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            inGameToggle.GetComponent<Toggle>().isOn = false;
        }
        //print(sizeIndicator);
    }

    public void IncreaseSize()
    {
        MazeManager.rowColManager += 1;
    }

    public void DecreaseSize()
    {
        MazeManager.rowColManager -= 1;
    }

    public void SetSizeSmall()
    {
        sizeIndicator = 0;
        EndGame.sizeNumber = 0;
    }

    public void SetSizeMedium()
    {
        sizeIndicator = 1;
        EndGame.sizeNumber = 1;
    }

    public void SetSizeLarge()
    {
        sizeIndicator = 2;
        EndGame.sizeNumber = 2;
    }
    public void RightOrLeftHand(bool newValue)
    {
        leftOrRight = newValue;
    }
}
