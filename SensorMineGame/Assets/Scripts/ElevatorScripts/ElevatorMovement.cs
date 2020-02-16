using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    // A script used to move the elevator without the arduino.
    [SerializeField] private int movement;
    [SerializeField] private float speed;
    //Through this Enum the elevator will switch stages, to go down to a certain destination. 
    public enum Elevator
    {
        FirstSTage,
        SecondStage,
        ThirdStage,
        FourthStage
    }
    private Elevator elevator;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Transform target;
    void Start()
    {
        this.elevator = Elevator.FirstSTage;

    }
    //mostly as a test for the real script the elevator has a int to switch through the states, with this for each stage it has a position to go towards. 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement++;
            MoveElevator();
        }
        else
   if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement--;
            MoveElevator();
        }
        if (movement >= 4)
        {
            movement = 3;
        }
        else if (movement <= -1)
        {
            movement = 0;
        }
        switch (this.elevator)
        {
            case Elevator.FourthStage:
                target = targets[3].transform;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.ThirdStage:
                target = targets[2].transform;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.SecondStage:
                target = targets[1].transform;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.FirstSTage:
                target = targets[0].transform;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                break;
        }

    }
    //here it checks for the int to see what stage it needs to go to/is on.
    void MoveElevator()
    {
        switch (movement)
        {
            case 0:
                this.elevator = Elevator.FirstSTage;
                break;
            case 1:
                this.elevator = Elevator.SecondStage;
                break;
            case 2:
                this.elevator = Elevator.ThirdStage;
                break;
            case 3:
                this.elevator = Elevator.FourthStage;
                break;
        }
    }
}
