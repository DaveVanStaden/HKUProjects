using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject miner;
    private float minerTime;

    private bool spawnMinerAtStart = true;

    // Update is called once per frame
    void Update()
    {
        //When the game starts, there will be 1 miner spawned so that the exercise can immediatly start.
        if (spawnMinerAtStart)
        {
            minerTime = StaticSettings.spawnTime;
            spawnMinerAtStart = false;
        }
        //if the timer has passed the minertime, then a new miner will be spawned, otherwise the game hasnt started yet, so the first miner will be spawned.
        if (!StaticSettings.timeHasPassed)
        {
            SpawnMiner();
        }
        else
        {
            minerTime = 0;
            spawnMinerAtStart = true;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        //print(minerTime + " : " + StaticSettings.maxTime);
    }
    //A time will be set equal to a static setting, this can be changed in the main menu. After that time has passed, the object will be instantiated after what a parent will be set to not make the scene a mess.
    //After that the timer will be set back to 0 to restart the spawning, if the animations are off it will dissable the animations as well.
    private void SpawnMiner()
    {
        minerTime += Time.deltaTime;
        if (minerTime >= StaticSettings.spawnTime + Random.Range(0, 3))
        {
            var tempMiner = Instantiate(miner, new Vector2(-10, 3.22f), Quaternion.identity);
            tempMiner.transform.SetParent(gameObject.transform);
            minerTime = 0;

            if (!StaticSettings.animationOff)
            {
                tempMiner.GetComponentInChildren<Animator>().enabled = true;
                tempMiner.GetComponentInChildren<Animator>().speed = Random.Range(0.9f, 1.1f);
            }
            else
            {
                tempMiner.GetComponentInChildren<Animator>().enabled = false;
            }
        }
    }
}
