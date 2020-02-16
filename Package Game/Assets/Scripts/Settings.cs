using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Text[] Indicators;

    [SerializeField] private float PackageSpeed;
    public static float PackageMovementSpeed;

    [SerializeField] private float SpawnSpeed;
    public static float PackageSpawnSpeed;
    /// <summary>
    /// Mostly an options menu for setting the speed and spawn rate of the boxes.
    /// </summary>

    private void Update()
    {
        Indicators[0].text = "Speed: " + Settings.PackageMovementSpeed;
        Indicators[1].text = "Spawns every: " + Settings.PackageSpawnSpeed + " seconds";
    }

    public void IncreasePackageSpeed()
    {
        if (PackageMovementSpeed <= 21.5f)
        {
            PackageMovementSpeed += 3;
        }
    }

    public void DecreasePackageSpeed()
    {
        if (PackageMovementSpeed >= 0)
        {
            PackageMovementSpeed -= 3;
        }
    }

    public void IncreaseSpawnSpeed()
    {
        PackageSpawnSpeed -= 2;
    }

    public void DecreaseSpawnSpeed()
    {
        PackageSpawnSpeed += 2;
    }
}