using UnityEngine;

public class PersonManager : MonoBehaviour
{
    /// <summary>
    /// Simple script that randomizes the people sitting in the restaurant. Also makes sure that there are no duplicates sitting at a table.
    /// </summary>

    [SerializeField] private Sprite[] peopleFrontSprites;
    [SerializeField] private Sprite[] peopleBackSprites;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePeople();
    }

    private void Update()
    {

        //Makes sure there are no duplicates sitting at the same table. It does this by checking per table if two of the front and/or back sitting people has the same sprite name as the other. 
        //If so, one of the sprites changes. It does this until they aren't the same anymore.
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().sprite.name == transform.GetChild(i).GetChild(2).GetComponent<SpriteRenderer>().sprite.name)
            {
                transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().sprite = peopleFrontSprites[Random.Range(0, peopleFrontSprites.Length)];
            }

            if (transform.GetChild(i).GetChild(3).GetComponent<SpriteRenderer>().sprite.name == transform.GetChild(i).GetChild(4).GetComponent<SpriteRenderer>().sprite.name)
            {
                transform.GetChild(i).GetChild(3).GetComponent<SpriteRenderer>().sprite = peopleBackSprites[Random.Range(0, peopleBackSprites.Length)];
            }
        }
    }

    //Selects all the childeren of the Tables object in the scene. It then Randomly Assigns a sprite to the childeren of table.
    private void GeneratePeople()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().sprite = peopleFrontSprites[Random.Range(0, peopleFrontSprites.Length)];
            transform.GetChild(i).GetChild(2).GetComponent<SpriteRenderer>().sprite = peopleFrontSprites[Random.Range(0, peopleFrontSprites.Length)];

            transform.GetChild(i).GetChild(3).GetComponent<SpriteRenderer>().sprite = peopleBackSprites[Random.Range(0, peopleBackSprites.Length)];
            transform.GetChild(i).GetChild(4).GetComponent<SpriteRenderer>().sprite = peopleBackSprites[Random.Range(0, peopleBackSprites.Length)];
        }
    }
}