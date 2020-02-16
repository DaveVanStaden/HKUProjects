using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{
    //Summary: This script is responsible for generating the actual maze itself. It places the floor and the walls in the scene. It also makes sure that the camera is in the right position and on the right scale.


    public enum MazeGeneratorType
    {
        RecursiveDivision,
        PureRecursive
    }

    public MazeGeneratorType Algorithm = MazeGeneratorType.PureRecursive;

    public bool FullRandom = false;
    public int RandomSeed = 12345;

    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject player = null;
    [SerializeField] private Material[] floorMaterials;

    public static int rowColManager = 5;
    private int Rows;
    private int Columns;

    private int RowsHalf;
    private int ColumnsHalf;

    public float CellWidth = 5f;
    public float CellHeight = 5f;
    public bool AddGaps = true;

    private MazeGeneratorBasic mMazeGenerator = null;

    public Text _rowtext, _columntext, _timer;

    public Camera _camera;

    [SerializeField] private GameObject rotObject;

    public static float mouseDistanceTolerance;

    private void Start()
    {
        switch (MenuManager.sizeIndicator)
        {
            case 0:
                Rows = 8;
                Columns = 8;
                break;
            case 1:
                Rows = 12;
                Columns = 12;
                break;
            case 2:
                Rows = 16;
                Columns = 16;
                break;
            default:
                Rows = 8;
                Columns = 8;
                break;
        }

        GenerateTheMaze();
    }

    private void Update()
    {
        //Rows = rowColManager;
        //Columns = rowColManager;
        //When the spacebar is pressed, it will generate the maze.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTheMaze();
        }
        if (Input.GetKey(KeyCode.C))
        {
            MenuManager.leftOrRight = true;
        }
        if (!MenuManager.leftOrRight)
        {
            switch (Rows)
            {
                case 8:
                    _camera.transform.position = new Vector3(2.5f, 45, 14.2f); //Posistion of the main camera
                    _camera.orthographicSize = 16.5f;                          //Size of the main camera
                    rotObject.transform.position = new Vector3(-15, 0, 15);    //Posistion of the rotationOBject
                    rotObject.transform.localScale = new Vector3(1, 1, 1);     //Scale of the rotationObject
                    mouseDistanceTolerance = 45.5f;                            //Distance between mouse and rotationObject before the rotationObject starts rotating
                    _timer.transform.   position = new Vector3(165, 100, 0);      //Position of the Timer
                    break;
                case 12:
                    _camera.transform.position = new Vector3(3, 45, 22);
                    _camera.orthographicSize = 25f;
                    rotObject.transform.position = new Vector3(-22, 0, 22);
                    rotObject.transform.localScale = new Vector3(1.5f, 1, 1.5f);
                    mouseDistanceTolerance = 47.5f;
                    _timer.transform.position = new Vector3(165, 100, 0);
                    break;
                case 16:
                    _camera.transform.position = new Vector3(5, 45, 30);
                    _camera.orthographicSize = 33f;
                    rotObject.transform.position = new Vector3(-30, 0, 30);
                    rotObject.transform.localScale = new Vector3(2, 1, 2);
                    mouseDistanceTolerance = 49.5f;
                    _timer.transform.position = new Vector3(165, 100, 0);
                    break;
                default:
                    _camera.transform.position = new Vector3(2.5f, 45, 14.2f);
                    _camera.orthographicSize = 16.5f;
                    rotObject.transform.position = new Vector3(-15, 0, 15);
                    rotObject.transform.localScale = new Vector3(1, 1, 1);
                    mouseDistanceTolerance = 45.5f;
                    _timer.transform.position = new Vector3(165, 100, 0);
                    break;
            }
        }
        else
        {
            switch (Rows)
            {
                case 8:
                    _camera.transform.position = new Vector3(25f, 45, 14.2f); //Posistion of the main camera
                    _camera.orthographicSize = 16.5f;                          //Size of the main camera
                    rotObject.transform.position = new Vector3(42, 0, 15);    //Posistion of the rotationOBject
                    rotObject.transform.localScale = new Vector3(1, 1, 1);     //Scale of the rotationObject
                    mouseDistanceTolerance = 45.5f;                            //Distance between mouse and rotationObject before the rotationObject starts rotating
                    _timer.transform.position = new Vector3(1756, 100, 0);      //Position of the Timer
                    break;
                case 12:
                    _camera.transform.position = new Vector3(42f, 45, 22);
                    _camera.orthographicSize = 25f;
                    rotObject.transform.position = new Vector3(65, 0, 22);
                    rotObject.transform.localScale = new Vector3(1.5f, 1, 1.5f);
                    mouseDistanceTolerance = 47.5f;
                    _timer.transform.position = new Vector3(1756, 100, 0);
                    break;
                case 16:
                    _camera.transform.position = new Vector3(55f, 45, 30);
                    _camera.orthographicSize = 33f;
                    rotObject.transform.position = new Vector3(85f, 0, 30);
                    rotObject.transform.localScale = new Vector3(2, 1, 2);
                    mouseDistanceTolerance = 49.5f;
                    _timer.transform.position = new Vector3(1756, 100, 0);
                    break;
                default:
                    _camera.transform.position = new Vector3(25f, 45, 14.2f);
                    _camera.orthographicSize = 16.5f;
                    rotObject.transform.position = new Vector3(42, 0, 15);
                    rotObject.transform.localScale = new Vector3(1, 1, 1);
                    mouseDistanceTolerance = 45.5f;
                    _timer.transform.position = new Vector3(1756, 100, 0);
                    break;
            }
        }
        
    }

    //bool ReadInput()
    //{
    //    //Turns the input of the input field into data for the Columns. Also helps finding the center of the maze by noting what half the of the Columns is.
    //    if (int.TryParse(_columntext.text, out Columns))
    //    {
    //        print("colum succeeded");
    //        ColumnsHalf = Columns / 2;
    //    }
    //    else
    //    {
    //        //Let's us know if reterieving the Column input data failed.
    //        print("column failed");
    //        return false;
    //    }

    //    //Turns the input of the input field into data for the Rows. Also helps finding the center of the maze by noting what half the of the Rows is.
    //    if (int.TryParse(_rowtext.text, out Rows))
    //    {
    //        print("row succeeded");
    //        RowsHalf = Rows / 2;
    //    }
    //    else
    //    {
    //        //Let's us know if reterieving the row input data failed.
    //        print("row failed");
    //        return false;
    //    }
    //    return true;
    //}

    //This let's us scale the camera with the maze. It takes the highest number of either columns or rows and multiplies that with two.
    //Then it adds one to make it fit a bit better. It works best when the maze has a definitive center.
    //private void CameraScaler()
    //{
    //    Debug.Log(Mathf.Max(Columns, Rows));
    //    _camera.orthographicSize = Mathf.Max(Columns, Rows) * 2 + 1;
    //}

    public void GenerateTheMaze()
    {
        //Destroys the previous maze before generating a new one.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Resets the rotation of the rotObject to 0, 0, 0
        rotObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Makes sure we have valid data to generate a maze from the input.
        //if (!ReadInput())
        //{
        //    //return;
        //}

        //Scales the camera to the maze.
        //CameraScaler();

        //If we aren't generating a non-random maze, makes sure that our maze is actually random.
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }

        //This lets us decide what kind of algorithm is used to generate the maze. Whilst the basis of the code that I inspired my own code on did came with more algorithms to choose from, I decided against using them. I felt it would be dishonest to use too much code that I couldn't edit or alter to make my own.
        switch (Algorithm)
        {
            case MazeGeneratorType.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }

        mMazeGenerator.GenerateMaze();

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                //Creates a floor that the maze can stand on. It also makes sure that the maze is a child of the mazeGenerator itself.
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(90, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                tmp.GetComponent<MeshRenderer>().material = floorMaterials[Random.Range(0, 2)];

                //Creates a wall that is oriented towards the right. With a Y rotation of 90.
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                    tmp.transform.name = "right";
                }

                //Creates a wall that is oriented towards the front. With a Y rotation of 0.
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                    tmp.transform.name = "front";
                }

                //Creates a wall that is oriented towards the left. With a Y rotation of 270.
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                    tmp.transform.name = "left";
                }

                //Creates a wall that is oriented towards the back. With a Y rotation of 180.
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                    tmp.transform.name = "back";
                }

                //This changes the position of the camera to that of the middle of the maze. It does this by looking at the most middle of both the Columns and Rows and placing the camera there.
                //if (row == RowsHalf && column == ColumnsHalf)
                //{
                //    _camera.transform.position = new Vector3(x, 44, z);
                //}
            }
        }

        //Moves the player back to the start when a new maze is generated. Also removes any momentum it might have.
        player.transform.position = new Vector3(0f, 0.6f, 0f);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
