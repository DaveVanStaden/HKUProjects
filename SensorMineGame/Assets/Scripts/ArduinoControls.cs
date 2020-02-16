using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArduinoControls : MonoBehaviour
{
    /// <summary>
    /// This script uses several other scripts in order to register input from the Arduino pressure sensors and translate those to controls for the game.
    /// </summary>


    public GameObject elevetorObject;

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

    [SerializeField] private bool isLeft;

    [SerializeField] private Color[] indicationColors;


    void Start()
    {
        this.elevator = Elevator.FirstSTage;
    }

    void Update()
    {
        //Makes sure that the elevator doesn't move out of bounds
        if (StaticSettings.movement >= 4)
        {
            StaticSettings.movement = 3;
        }
        else if (StaticSettings.movement <= -1 || StaticSettings.timeHasPassed)
        {
            StaticSettings.movement = 0;
        }

        //If the case is a certain level, the elevator will move towards the appointed target.
        switch (this.elevator)
        {
            case Elevator.FourthStage:
                target = targets[3].transform;
                elevetorObject.transform.position = Vector2.MoveTowards(elevetorObject.transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.ThirdStage:
                target = targets[2].transform;
                elevetorObject.transform.position = Vector2.MoveTowards(elevetorObject.transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.SecondStage:
                target = targets[1].transform;
                elevetorObject.transform.position = Vector2.MoveTowards(elevetorObject.transform.position, target.position, speed * Time.deltaTime);
                break;
            case Elevator.FirstSTage:
                target = targets[0].transform;
                elevetorObject.transform.position = Vector2.MoveTowards(elevetorObject.transform.position, target.position, speed * Time.deltaTime);
                break;
        }
        //print(StaticSettings.elevatorIsMoving);

        //The elevator is always moving towards a target. If the Y of the elevator isn't the same as the target, it means the elevator is moving.
        if (elevetorObject.transform.position.y != target.transform.position.y)
        {
            StaticSettings.elevatorIsMoving = true;
        }
        else
        {
            StaticSettings.elevatorIsMoving = false;
        }
        //print(target.name);

        //If it is the left elevator and it is at the right Y coordinate, it will tell us if it at its current floor on the left. If it is the right one, it will tell us if the right elevator is at the right posistion.
        if (isLeft)
        {
            switch (elevetorObject.transform.position.y)
            {
                case 3.8f:
                    StaticSettings.elevatorLevelLeft = 0;
                    break;
                case 1.7f:
                    StaticSettings.elevatorLevelLeft = 1;
                    break;
                case -1:
                    StaticSettings.elevatorLevelLeft = 2;
                    break;
                case -3.5f:
                    StaticSettings.elevatorLevelLeft = 3;
                    break;
            }
        }
        else
        {
            switch (elevetorObject.transform.position.y)
            {
                case 3.8f:
                    StaticSettings.elevatorLevelRight = 0;
                    break;
                case 1.7f:
                    StaticSettings.elevatorLevelRight = 1;
                    break;
                case -1:
                    StaticSettings.elevatorLevelRight = 2;
                    break;
                case -3.5f:
                    StaticSettings.elevatorLevelRight = 3;
                    break;
            }
        }
    }

    //If the movement is a certain number, the elevator will change its target.
    void MoveElevator()
    {
        switch (StaticSettings.movement)
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

    //Here is where the interesting stuff happens. _input is created by the amount of pressure the Arduino sensor experience. If there is no input, the elevator will go to the top floor.
    //If there is a bit more then zero, the elevator will move towards the second level. It also checks if the time hasn't passed and if not too much pressure is given. The rest of the else ifs work the same.
    public void UpAndDown(float _input)
    {
        if (_input <= 0)
        {
            this.elevator = Elevator.FirstSTage;
            elevetorObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            //print("First Level");
        }
        else if (_input > 0 && _input < 0.7f && !StaticSettings.timeHasPassed)
        {
            this.elevator = Elevator.SecondStage;
            elevetorObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = indicationColors[0];
            //print("Second Level");
        }
        else if (_input >= 0.7f && _input < 1f && !StaticSettings.timeHasPassed)
        {
            this.elevator = Elevator.ThirdStage;
            elevetorObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = indicationColors[1];
            //print("Third Level");
        }
        else if (_input >= 1f && !StaticSettings.timeHasPassed)
        {
            this.elevator = Elevator.FourthStage;
            elevetorObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = indicationColors[2];
            //print("Forth Level");
        }
        //print(_input);
    }
}