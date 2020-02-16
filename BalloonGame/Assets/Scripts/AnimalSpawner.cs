using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> animals;
    [SerializeField] private List<float> xSpawnPosition;
    [SerializeField] private List<float> ySpawnPosition;

    [SerializeField] private float stopTime;
    private float startTime = 0f;
    private float startSpeed;
    [SerializeField] private float maxTime; //2
    public static float animalSpeed = 2.5f;
    private Vector2 SpawnPosition;

    private float biomeNumber;
    [SerializeField] private List<Sprite> animalSprites;
    [SerializeField] private List<Sprite> garbageSprites;

    static public int rubbishNumber;

    // Start is called before the first frame update, with it the variables are assigned and the first background spawned.                                      
    void Start()
    {
        startTime += stopTime;
        startSpeed = animalSpeed;
    }
    private void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= maxTime && !StaticVariable.gameIsOver)
        {
            SpawnCritter();
            startTime = 0f;
        }
    }
    /// <summary>
    /// A random number gets generated to select a random animal, to make the chance for a  land or sea animal igger, they are set there twice.
    /// after that the objects gets given a rigidbody, a velocity , and the X gets flipped if it spawns on the right.
    /// </summary>
    private void SpawnCritter()
    {
        GenerateVector();
        var tempAnimal = Instantiate(animals[Random.Range(0, animals.Count)], SpawnPosition, Quaternion.identity);
        biomeNumber = Random.Range(1, rubbishNumber); 

        switch (biomeNumber)
        {
            case 6:
                tempAnimal.tag = "Danger";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = garbageSprites[0]; 
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                animalSpeed = startSpeed;
                break;
            case 5:
                tempAnimal.tag = "Land";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(0, 5)]; 
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green; 
                animalSpeed = startSpeed;
                break;
            case 4:
                tempAnimal.tag = "Sea";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(5, 10)];
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;  
                animalSpeed = startSpeed;
                break;
            case 3:
                tempAnimal.tag = "Land";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(0, 5)];
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green; 
                animalSpeed = startSpeed;
                break;
            case 2:
                tempAnimal.tag = "Sea";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(5, 10)];
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue; 
                animalSpeed = startSpeed;
                break;
            case 1:
                tempAnimal.tag = "Both";
                tempAnimal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(10, 13)]; 
                tempAnimal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
                animalSpeed = startSpeed / 2;
                break;
            default:
                print("How did this happen?");
                break;
        }

        var TempRigid = tempAnimal.GetComponent<Rigidbody2D>();

        if (tempAnimal.transform.position.x >= 10)
        {
            TempRigid.velocity = new Vector2(-1f, 0) * animalSpeed;
            tempAnimal.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            TempRigid.velocity = new Vector2(-1f, 0) * -animalSpeed;
        }
    }

    private void GenerateVector()
    {
        int XNumber = Random.Range(0, 2);
        float XValue = xSpawnPosition[XNumber];
        float YValue = Random.Range(ySpawnPosition[0], ySpawnPosition[1]);
        SpawnPosition = new Vector2(XValue, YValue);
    }
}