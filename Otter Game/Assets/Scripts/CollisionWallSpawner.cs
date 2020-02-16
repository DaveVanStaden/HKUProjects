using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWallSpawner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private BackgroundScroller controller;
    private float spawnPos;
    private float colliderObject;

    private void Start()
    {
        controller = camera.GetComponent<BackgroundScroller>();
    }
    //IT checks if it hist an object with the tag wall to get removed, this is to make the performance better.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            colliderObject = collider.GetComponent<Collider2D>().bounds.size.x;
            //spawnPos = transform.position.x + 20;
            BackgroundScroller.spawnPosition = colliderObject;
            controller.SpawnWall();
        }
    }
}
