using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Text[] optionTexts;

    public bool canSpawnObstacles = false;

    private void Update()
    {
        optionTexts[0].text = "The time is " + Timer.timeLeft.ToString() + " seconden";
        optionTexts[1].text = "The size is " + (ObjectSize.ActualSize).ToString();
        optionTexts[2].text = "The speed is " + AnimalSpawner.animalSpeed;

        boundaries();

        if (canSpawnObstacles)
        {
            AnimalSpawner.rubbishNumber = 7;
        }
        else
        {
            AnimalSpawner.rubbishNumber = 6;
        }
    }
    /// <summary>
    /// Overall all settings for objects are set in stone here. from setting the time/size/ speed to if it can spawn obstacles.
    /// </summary>
    public void AddSeconds()
    {
        Timer.timeLeft += 10;
    }
    public void DeductSeconds()
    {
        Timer.timeLeft -= 10;
    }
    public void IncreaseSize()
    {
        ObjectSize.ActualSize += 0.01f;
    }
    public void DecreaseSize()
    {
        ObjectSize.ActualSize -= 0.01f;
    }
    public void IncreaseSpeed()
    {
        AnimalSpawner.animalSpeed += 0.5f;
    }
    public void DecreaseSpeed()
    {
        AnimalSpawner.animalSpeed -= 0.5f;
    }

    public void SpawnObstacles()
    {
        canSpawnObstacles = !canSpawnObstacles;
    }

    private void boundaries()
    {
        if (Timer.timeLeft <= 10)
        {
            Timer.timeLeft = 10;
        }
        if (ObjectSize.ActualSize >= 0.15f)
        {
            ObjectSize.ActualSize = 0.15f;
        }
        if (ObjectSize.ActualSize <= 0.01f)
        {
            ObjectSize.ActualSize = 0.01f;
        }
        if (AnimalSpawner.animalSpeed >= 7.5f)
        {
            AnimalSpawner.animalSpeed = 7.5f;
        }
        if (AnimalSpawner.animalSpeed <= 0.5f)
        {
            AnimalSpawner.animalSpeed = 0.5f;
        }
    }
}
