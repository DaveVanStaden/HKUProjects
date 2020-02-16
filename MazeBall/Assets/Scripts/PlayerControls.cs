using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Summary: This is a very basic set of controls for the player. I thought it would be fun to push the player, rather than move the player.
    //Besides the movement controls, this code also gives the player the ability to switch between a top down camera perspective and a third person one.

    private float force = 60;
    private Rigidbody playerBody;

    public Camera cam1;
    public Camera cam2;

    [SerializeField] private GameObject rotObject;

    private void Start()
    {
        //Makes sure we start with the main camera instead of the player camera.
        cam1.enabled = true;
        cam2.enabled = false;

        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Makes the rotation of the player equel to that of the rotObject.
        transform.rotation = rotObject.transform.rotation;

        //Moves the player forward after the following is true: The left mouse is pressed (touch also counts), the mouse is close enough to the movement pad and finally, if the game isn't over yet
        if (Input.GetMouseButton(0) && rotObject.GetComponent<RotationScript>().canMove && !EndGame.gameHasEnded)
        {
            playerBody.AddForce(transform.forward * force);
        }

        //Cancels any velocity the player might have after the game has ended
        if (EndGame.gameHasEnded)
        {
            playerBody.velocity = Vector3.zero;
        }

        //Switches between the main camera and the player camera.
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    cam1.enabled = !cam1.enabled;
        //    cam2.enabled = !cam2.enabled;
        //}
    }
}
