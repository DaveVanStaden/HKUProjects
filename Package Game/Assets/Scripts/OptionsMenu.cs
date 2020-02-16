using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Text[] optionTexts;

    public bool canSpawnObstacles = false;

    private void Start()
    {
        Settings.PackageSpawnSpeed = 10;
    }
    private void Update()
    {
        optionTexts[0].text = "De band snelheid is: " + Settings.PackageMovementSpeed.ToString();
        optionTexts[1].text = "De packetjes woorden zo snel gespawnd: " + Settings.PackageSpawnSpeed.ToString();

        boundaries();

    }
    public void StartGame()
    {
        SceneManager.LoadScene("PackageGameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ConveyorBeltSpeedUp()
    {
        Settings.PackageMovementSpeed += 1;
    }
    public void ConveyorBeltSpeedDown()
    {
        Settings.PackageMovementSpeed -= 1;
    }
    public void PackageSpeedUp()
    {
        Settings.PackageSpawnSpeed += 1;
    }
    public void PackageSpeedDown()
    {
        Settings.PackageSpawnSpeed -= 1;
    }
    private void boundaries()
    {
        if (Settings.PackageMovementSpeed < 1)
        {
            Settings.PackageMovementSpeed = 1;
        }
        if (Settings.PackageMovementSpeed > 10)
        {
            Settings.PackageMovementSpeed = 10;
        }
        if (Settings.PackageSpawnSpeed < 1)
        {
            Settings.PackageSpawnSpeed = 1;
        }
        if (Settings.PackageSpawnSpeed > 10)
        {
            Settings.PackageSpawnSpeed = 10;
        }
    }
}
