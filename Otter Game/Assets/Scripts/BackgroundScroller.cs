using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgrounds;
    [SerializeField] private List<GameObject> collectableTemplates;
    static public float spawnPosition;
    private float spawnZ = 0.0f;

    [SerializeField] private float stopTime;
    private float startTime = 0f;
    private float scrollSpeed;

    private bool canSpawnItems = false;

    // Start is called before the first frame update, with it the variables are assigned and the first background spawned.
    void Start()
    {
        stopTime = StaticVariable.SpawnSpeed;
        scrollSpeed = StaticVariable.MovementSpeedSpawner;
        startTime += stopTime;
        spawnPosition = 1;
        SpawnBackground();
    }
    /// <summary>
    /// in spawn background, at the beginning of the game a wall gets spawned without the items, after that the items are also used in the spawns. 
    /// Aside from that the rigidbody is asked from the object so it can give a velocity towards the object.
    /// </summary>
    private void SpawnBackground()
    {
        var TempWall = Instantiate(backgrounds[Random.Range(0, backgrounds.Count)], new Vector2(spawnPosition,0), Quaternion.identity);
        var TempRigid = TempWall.GetComponent<Rigidbody2D>();
        TempRigid.velocity = new Vector2(-1f, 0) * scrollSpeed;

        if (canSpawnItems)
        {
            var TempCollect = Instantiate(collectableTemplates[Random.Range(0, collectableTemplates.Count)], new Vector2(spawnPosition, 0), Quaternion.identity);
            var TempCollectBody = TempCollect.GetComponent<Rigidbody2D>();
            TempCollectBody.velocity = new Vector2(-1f, 0) * scrollSpeed;
        }
    }

    public void SpawnWall()
    {
        SpawnBackground();
        //spawnPosition = 19.45f;
        canSpawnItems = true;
    }
}
