using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private Sprite[] landedSprites;
    public static bool gameEnded;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (StaticVariable.points <= 0)
        {
            StaticVariable.points = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.name == "LandBiome" && collision.transform.tag == "Land" && collision.transform.tag != "HitAnimal" && collision.transform.tag != "Landed")
        {
            CorrectBiomeActions(collision);
            print("Land     Good");
        }
        if (transform.name == "LandBiome" && collision.transform.tag != "Land" && collision.transform.tag != "HitAnimal" && collision.transform.tag != "Landed")
        {
            IncorrectBiomeActions(collision);
            print("Land     Bad");
        }

        if (transform.name == "SeaBiome" && collision.transform.tag == "Sea" && collision.transform.tag != "HitAnimal" && collision.transform.tag != "Landed")
        {
            CorrectBiomeActions(collision);
            print("Sea      Good");
        }
        if (transform.name == "SeaBiome" && collision.transform.tag != "Sea" && collision.transform.tag != "HitAnimal" && collision.transform.tag != "Landed")
        {
            IncorrectBiomeActions(collision);
            print("Sea      Bad");
        }

        if (collision.transform.tag == "HitAnimal")
        {
            CorrectBiomeActions(collision);
            print("Both      Good");
        }
    }

    private void CorrectBiomeActions(Collision2D collision)
    {
        if (gameEnded == false)
        {
            StaticVariable.points++;
        }
        collision.transform.tag = "Landed";
        collision.transform.GetChild(0).GetComponent<Animator>().speed = 0.5f;
        collision.transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimStates", 2);
        collision.transform.GetChild(0).GetComponent<Animator>().SetBool("IncorrectIsTrue", false);
        audioSource.Play();

        var sprite = collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        var name = sprite.name.ToLower();


        switch (name)
        {
            case "animal1":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[0];
                break;
            case "animal2":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[1];
                break;
            case "animal3":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[2];
                break;
            case "animal4":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[3];
                break;
            case "animal5":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[4];
                break;
            case "beaver":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[5];
                break;
            case "crocodile":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[6];
                break;
            case "penguin":
                collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = landedSprites[7];
                break;
            default:
                print("Hello World");
                break;
        }
    }

    private void IncorrectBiomeActions(Collision2D collision)
    {
        //StaticVariable.points--;
        collision.transform.tag = "Landed";
        collision.transform.GetChild(0).GetComponent<Animator>().speed = 1f;
        collision.transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimStates", 1);
        collision.transform.GetChild(0).GetComponent<Animator>().SetBool("IncorrectIsTrue", true);

        audioSource.Play();
        if (gameEnded == true)
        {
            Timer.badHits += 1;
        }
    }
}
