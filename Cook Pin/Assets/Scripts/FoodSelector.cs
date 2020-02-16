using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    private int points = -1;

    [SerializeField] private GameObject[] tables;
    [SerializeField] private GameObject kitchen;
    [SerializeField] private GameObject beginText;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject timerObject;

    //[SerializeField] private Text pointText;
    [SerializeField] private Text victoryPointText;

    private bool isAtKitchen;
    static public bool gameIsRunning = false;
    private bool newFoodLimit = true;

    [SerializeField] private Sprite[] foodSprites;
    [SerializeField] private Sprite emptyPlateSprite;

    private AudioSource dinnerBell;
    private Sprite foodSprite;

    [SerializeField] private Color transparencyColor;
    [SerializeField] private Color fullColor;

    // Start is called before the first frame update
    void Start()
    {
        isAtKitchen = false;
        gameIsRunning = true;
        //kitchen.transform.GetChild(0).gameObject.SetActive(false);

        dinnerBell = GetComponent<AudioSource>();
        foodSprite = foodSprites[Random.Range(0, foodSprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTable();
        }

        //Only works if gameIsRunning is true. This is mostly just here to stop the script from giving errors when it wants to disable the animator, but the object is already disabled.
        if (gameIsRunning)
        {
            if (isAtKitchen)
            {
                kitchen.GetComponent<Collider2D>().enabled = false;
                //kitchen.GetComponent<SpriteRenderer>().color = Color.white;
                //Turns off the animator of the food in the kitchen
                kitchen.GetComponentInChildren<Animator>().enabled = false;
                //Makes the food in the kitchen its max size
                kitchen.transform.GetChild(0).transform.localScale = new Vector3(0.2f, 0.2f, 1);
                //Makes the food in the kitchen transparent
                kitchen.transform.GetChild(0).GetComponent<SpriteRenderer>().color = transparencyColor;

                newFoodLimit = true;
            }
            else
            {
                kitchen.GetComponent<Collider2D>().enabled = true;
                //kitchen.GetComponent<SpriteRenderer>().color = Color.magenta;
                kitchen.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = foodSprite;
                kitchen.GetComponentInChildren<Animator>().enabled = true;
                kitchen.transform.GetChild(0).GetComponent<SpriteRenderer>().color = fullColor;
            }
        }

        if (!isAtKitchen && newFoodLimit)
        {
            foodSprite = foodSprites[Random.Range(0, foodSprites.Length)];
            newFoodLimit = false;
        }


        //pointText.text = points.ToString() + " / " + StaticSettings.maxPoints;
        victoryPointText.text = points.ToString();

        //Stops the timer from coutning, deactivates all the colliders and child sprites of the tables and kitchen and enables the victory screen object
        if (timerObject.GetComponent<Timer>().timeHasPassed) // || points >= StaticSettings.maxPoints)
        {
            gameIsRunning = false;
            victoryScreen.SetActive(true);

            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].GetComponent<Collider2D>().enabled = false;
                tables[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            kitchen.GetComponent<Collider2D>().enabled = false;
            kitchen.transform.GetChild(0).gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// Simple function that generates a random sprite for the food and assigns it to a table. It also resets all the tables every time it is used.
    /// </summary>
    private void GenerateFood()
    {
        //foodSprite = foodSprites[Random.Range(0, foodSprites.Length)];
        var tempNextFoodSprite = foodSprites[Random.Range(0, foodSprites.Length)];

        //Resets all the tables, turning off their foodItem and disabling their collider
        for (int i = 0; i < tables.Length; i++)
        {
            //tables[i].GetComponent<SpriteRenderer>().color = Color.white;

            tables[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = foodSprite;
            tables[i].transform.GetChild(0).gameObject.SetActive(false);
            tables[i].GetComponent<Collider2D>().enabled = false;

            kitchen.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = foodSprite;
        }


        //Randomly selects a table and then enables it's child that displays the food
        var tempTable = tables[Random.Range(0, 9)];
        //tempTable.GetComponent<SpriteRenderer>().color = Color.green;
        tempTable.transform.GetChild(0).gameObject.SetActive(true);
        kitchen.transform.GetChild(0).gameObject.SetActive(true);

        tempTable.GetComponent<Collider2D>().enabled = true;

        //Changes the position of the foodsprite back, enables the foodcloud sprite and turns the animator component of the sprite on.
        tempTable.transform.GetChild(0).transform.localPosition = new Vector3(-0.25f, 1, 0);
        tempTable.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        tempTable.transform.GetChild(0).GetComponent<Animator>().enabled = true;
    }

    /// <summary>
    /// Raycast function. Casts a ray in order to find either the kitchen or a table. Doesn't cast when there is no collider to hit.
    /// </summary>
    private void SelectTable()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (hit.collider != null)
        {
            //print(hit.transform.name);

            //When the player clicks on the kitchen, tells the game that the player is at the kitchen, start the GenerateFood function and adds a point to the player's score.
            if (hit.transform.name == "Kitchen" && gameIsRunning)
            {
                isAtKitchen = true;
                GenerateFood();
                dinnerBell.Play();
                points++;
            }

            //This only works for when the player clicks on the kitchen for the first time. Besides also generating food and telling the game the player is at the kitchen, it also disables a text element and starts the timer.
            //if (hit.transform.name == "Kitchen" && !gameIsRunning)
            //{
            //    gameIsRunning = true;
            //    beginText.gameObject.SetActive(false);
            //    GenerateFood();
            //    isAtKitchen = true;
            //}

            //Tells the game that the player isn't at the kitchen. It also disables the table's collider in order to prevent bugs.
            //Lastly it moves the food sprite on the table, removes the cloud sprite, stops the animation and resizes the foodsprite.
            if (hit.transform.tag == "Table")
            {
                isAtKitchen = false;
                hit.transform.GetComponent<Collider2D>().enabled = false;
                //hit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptyPlateSprite;
                hit.transform.GetChild(0).transform.localPosition = new Vector3(0, 0.4f, 0);
                hit.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                hit.transform.GetChild(0).GetComponent<Animator>().enabled = false;
                hit.transform.GetChild(0).transform.localScale = new Vector3(0.6f, 0.6f, 0);
            }
        }
    }
}