using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static int sizeNumber = 0;
    private Vector3 ActualMaze;

    [SerializeField] private GameObject victoryScreen;

    public static bool gameHasEnded;

    // Start is called before the first frame update
    private void Awake()
    {
        DeterminePosistion();
        //print("transformposition");
        gameHasEnded = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeterminePosistion();
        }
    }

    private void DeterminePosistion()
    {
        if (sizeNumber == 0)
        {
            int smallMazeNumber = Random.Range(0, 3);
            switch (smallMazeNumber)
            {
                case 0:
                    ActualMaze = new Vector3(27.8f, 0, 27.6f);
                    break;
                case 1:
                    ActualMaze = new Vector3(0, 0, 27.6f);
                    break;
                case 2:
                    ActualMaze = new Vector3(27.8f, 0, 0);
                    break;
            }
        }
        if (sizeNumber == 1)
        {
            int mediumMazeNumber = Random.Range(0, 3);
            switch (mediumMazeNumber)
            {
                case 0:
                    ActualMaze = new Vector3(45f, 0, 44f);
                    break;
                case 1:
                    ActualMaze = new Vector3(0, 0, 44f);
                    break;
                case 2:
                    ActualMaze = new Vector3(45f, 0, 0);
                    break;
            }
        }
        if (sizeNumber == 2)
        {
            int largeMazeNumber = Random.Range(0, 3);
            switch (largeMazeNumber)
            {
                case 0:
                    ActualMaze = new Vector3(60f, 0, 60f);
                    break;
                case 1:
                    ActualMaze = new Vector3(60f, 0, 0);
                    break;
                case 2:
                    ActualMaze = new Vector3(0, 0, 60f);
                    break;
            }
        }
        transform.position = ActualMaze;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            victoryScreen.SetActive(true);
            gameHasEnded = true;
        }
    }
}