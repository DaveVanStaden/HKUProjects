using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for the movement of the miners. It does so by switching between several States and checking certain parameters.
    /// It's also a bit of a bloated mess, my apologies.
    /// </summary>


    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform ElevatorLeft;
    [SerializeField] private Transform ElevatorRight;
    private Vector3 pos;
    private Vector3 randX;
    private Vector3 minerPos;

    private int desiredLevel;

    [SerializeField] private Sprite[] minerSprites;
    //With the enum the miner will switch between states to determine where to go to and when to stand still.
    private enum MovementState
    {
        Walk,
        ElevatorIn,
        MoveToMine,
        Mine,
        MoveOutOfMine,
        ElevatorOut,
        GoHome
    }
    private MovementState myState;

    // Turns the target into the Left elevator, determines a random value to change the position upon the elevator, generates a level the miner wants to go and sets the movementState as 'Walk'
    void Start()
    {
        ElevatorLeft = GameObject.Find("ElevatorLeft").transform;
        ElevatorRight = GameObject.Find("ElevatorRight").transform;
        target = ElevatorLeft;
        randX = new Vector3(Random.Range(0, 0.5f), 0, 0);
        desiredLevel = Random.Range(1, 4);
        myState = MovementState.Walk;

        switch (desiredLevel)
        {
            case 1:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[0];
                break;
            case 2:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[1];
                break;
            case 3:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        minerPos = transform.localPosition;

        //Checks if the miner has arrived at the left elevator, if the eleveator is at the right height and if myState equals walk.
        if (transform.localPosition != pos && target.position.y == 3.8f && myState == MovementState.Walk)
        {
            MoveTowardsElevator();
            //print("Going to Elevator");
        }

        //If the miner is at the left elevator, it is only there for a couple of frames. Therefore, the state is set to ElevatorIn and triggers when that state is such.
        if (transform.localPosition == pos || myState == MovementState.ElevatorIn) //|| transform.localPosition.x > -6.7f)
        {
            ElevatorState();
            myState = MovementState.ElevatorIn;
            //print("Is in the elevator");
        }

        //Sees if the desired level of the miner is the same as the current level of the Left elevator, if the elevator is currently moving and the previous state was ElevatorIn. If so, change the state to MoveToMine and move towards the desired mineshaft. It also checks if the miner isn't standing on the top level before it goes down.
        if (desiredLevel == StaticSettings.elevatorLevelLeft && !StaticSettings.elevatorIsMoving && myState == MovementState.ElevatorIn && transform.localPosition.y < 2 || desiredLevel == StaticSettings.elevatorLevelLeft && !StaticSettings.elevatorIsMoving && myState == MovementState.Walk && transform.localPosition.y < 2 || myState == MovementState.MoveToMine)
        {
            myState = MovementState.MoveToMine;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x + 3, transform.localPosition.y), speed * Time.deltaTime);
            randX = new Vector3(Random.Range(-3, 3), 0, 0);
        }

        //Sees if the miner has moved a random value towards the right, that the miner is not at the upper level and that the state is neither MoveOutofMine, nor ElevatorOutl; This is to prevent the MiningTime coroutine from accidentally starting.
        if (transform.localPosition.x > Random.Range(-3, 30) && transform.localPosition.y < 2 && myState != MovementState.MoveOutOfMine && myState != MovementState.ElevatorOut)
        {
            myState = MovementState.Mine;
            transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimStates", 1);
            StartCoroutine("MiningTime");
        }

        //Checks if the state is MoveOutOFMine, if the desiredlevel is equal to the level of the Right elevator and if the state is not ElevatorOut. 
        //If so, it changes the 'target' from the left elevator to the right elevator. Afterwards the miner moves towards the right elevator and the MiningTime coroutine is stopped. Also changes randX to have a value of 0, this is so that the miner will neatly stand in the centre of the elevator.
        if (myState == MovementState.MoveOutOfMine && desiredLevel == StaticSettings.elevatorLevelRight && !StaticSettings.elevatorIsMoving && myState != MovementState.ElevatorOut)
        {
            target = ElevatorRight;
            randX = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            MoveTowardsElevator();
            StopCoroutine("MiningTime");
        }
        //This here code moves the miner to the right in order to wait for the elevator.
        else if (myState == MovementState.MoveOutOfMine && desiredLevel != StaticSettings.elevatorLevelRight && myState != MovementState.ElevatorOut)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(3.5f, transform.localPosition.y), speed * Time.deltaTime);
        }

        //Basically the same checks when the miner initially moves towards the elevator. If the miner is close enough to the elevator, it changes its state to ElevatorOut and proceeds to follow the elevator.
        if (transform.localPosition == pos || myState == MovementState.ElevatorOut) // || transform.localPosition.x > 6)
        {
            ElevatorState();
            myState = MovementState.ElevatorOut;
        }


        //When the miner has reached the upper level again by way of the right elevator, it moves towards the right.
        if (transform.localPosition.y == 3.22f && myState == MovementState.ElevatorOut || transform.localPosition.y == 3.22f && myState == MovementState.MoveOutOfMine || myState == MovementState.GoHome)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(11, transform.localPosition.y), speed * Time.deltaTime);
            myState = MovementState.GoHome;
        }

        //When the miner is back on the surface and the state equals GoHome, the miner is destroyed.
        if (transform.localPosition == new Vector3(11, 3.22f, 0) && myState == MovementState.GoHome)
        {
            StaticSettings.score++;
            Destroy(gameObject);
        }
        //print(myState);
        //print(desiredLevel);// + " : " + StaticSettings.elevatorLevel);
    }


    //Moves the gameobject towards the target. It also gives pos a slight random change, in order to prevent the miners from all sharing the same space.
    private void MoveTowardsElevator()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(target.position.x + randX.x, target.position.y - 0.58f), speed * Time.deltaTime);
        pos = new Vector3(target.position.x + randX.x, target.position.y - 0.58f, 0);
    }

    //Changes the Y of the miners to the Y of the target
    private void ElevatorState()
    {
        if (myState != MovementState.GoHome)
        {
            minerPos.y = target.position.y - 0.58f;
            transform.localPosition = minerPos;
        }
    }

    //Waits for a couple of seconds and then changes the state to MoveOutOfMine
    IEnumerator MiningTime()
    {
        yield return new WaitForSeconds(Random.Range(6, 12));
        myState = MovementState.MoveOutOfMine;

        transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimStates", 0);


        switch (desiredLevel)
        {
            case 1:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[3];
                break;
            case 2:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[4];
                break;
            case 3:
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = minerSprites[5];
                break;
        }
    }

    //Sees if the miner collides with either the left or the right elevator. If they do so, they can move alongside them without the miner to be in the target posistion.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ElevatorInCol" && myState != MovementState.MoveToMine) // && StaticSettings.elevatorIsMoving == true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, collision.transform.position.y - 0.58f, 0);
        }
        if (collision.tag == "ElevatorOutCol" && myState != MovementState.GoHome)// && StaticSettings.elevatorIsMoving == true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, collision.transform.position.y - 0.58f, 0);
        }
    }
}