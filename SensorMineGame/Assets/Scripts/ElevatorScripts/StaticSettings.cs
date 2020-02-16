using UnityEngine;

public class StaticSettings : MonoBehaviour
{
    //All static settings that the game has are saved here. 
    public static bool timeHasPassed = true;
    public static float maxTime = 120;
    public static float spawnTime = 10;


    public static int movement;
    public static int elevatorLevelLeft;
    public static int elevatorLevelRight;

    public static bool elevatorIsMoving = false;

    public static int score;

    public static bool animationOff;
}
