using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The paths the generator can take.
public enum Direction
{
    Start,
    Right,
    Front,
    Left,
    Back,
};

//Booleans that keep track of each cell whilst the maze is being generated.
public class MazeCell
{
    public bool IsVisted = false;
    public bool WallRight = false;
    public bool WallFront = false;
    public bool WallLeft = false;
    public bool WallBack = false;
    public bool IsGoal = false;
}