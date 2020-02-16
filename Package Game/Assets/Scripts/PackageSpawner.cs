using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnPoint;
    [SerializeField] private GameObject[] Packages;

    private float FirstSpawnTime;
    private float SecondSpawnTime;


    [SerializeField] private Text TimeIndicator;

    // Sets the spawn time for the bottom and top conveyor belt.
    void Start()
    {
        FirstSpawnTime += 10; //10
        SecondSpawnTime += 10; //10
    }
    /// <summary>
    /// This has 2 timers, one for the top belt and one for the lower belt.
    /// Thats also why it has to if statements for both a different spawn, the second spawn rate allways spawns atleast 2 seconds later then the first spawn.
    /// 
    /// </summary>
    void Update()
    {
        FirstSpawnTime += Time.deltaTime;
        SecondSpawnTime += Time.deltaTime;

        TimeIndicator.text = Mathf.Round(FirstSpawnTime) + " seconds until spawn";

        if (FirstSpawnTime > Settings.PackageSpawnSpeed)
        {
            Instantiate(Packages[Random.Range(0, 3)], new Vector2(SpawnPoint[0].transform.position.x, SpawnPoint[0].transform.position.y), Quaternion.identity);
            GameObject.FindObjectOfType<PackageMovement2>().Speed = Settings.PackageMovementSpeed; //0.5
            FirstSpawnTime = 0;
        }

        if (SecondSpawnTime > Settings.PackageSpawnSpeed + 2)
        {
            Instantiate(Packages[Random.Range(0, 3)], new Vector2(SpawnPoint[1].transform.position.x, SpawnPoint[1].transform.position.y), Quaternion.identity);
            GameObject.FindObjectOfType<PackageMovement2>().Speed = -Settings.PackageMovementSpeed; //0.5
            SecondSpawnTime = 0;
        }
    }

    public void IncreaseSpawnSpeed()
    {
        //if the speed goes higher then this number, it gets lowered.
        if (Settings.PackageSpawnSpeed >= 13)
        {
            Settings.PackageSpawnSpeed -= 0.2f; //0.2
        }
    }
}