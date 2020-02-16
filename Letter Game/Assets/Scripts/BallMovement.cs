using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;

    private WordAlgorithm wordAlgorythm;

    [SerializeField] private GameObject Camera;

    private List<string> RemoveLetterList;

    private SpriteRenderer LetterBoxSprite;

    private int ZigZagSpeed;
    [Header("TimerRelated")]
    private float spawnTimerLimit = 9;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        wordAlgorythm = Camera.GetComponent<WordAlgorithm>();
        RemoveLetterList = wordAlgorythm.GetComponent<WordAlgorithm>().ObjectsInScene;
        LetterBoxSprite = GetComponent<SpriteRenderer>();
        ZigZagSpeed = Random.Range(2, 4);
    }
    //For each position there is a  certain velocity to give to the object.
    void Update()
    {
        if (transform.position.x == 0 && transform.position.y == -6)
        {
            velocityY = 1;
            LetterBoxSprite.color = Color.magenta;
        }

        if (transform.position.x == -10 && transform.position.y == 6)
        {
            velocityY = -1;
            LetterBoxSprite.color = Color.red;
        }

        if (transform.position.x == 10 && transform.position.y == 6)
        {
            velocityY = -1;
            LetterBoxSprite.color = Color.green;
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTimerLimit)
        {
            wordAlgorythm.GetComponent<WordAlgorithm>().ObjectsInScene.Remove(this.gameObject.transform.GetChild(0).name);
            spawnTimer = 0;
        }
        StartCoroutine("DestroyThySelf");

        Vector3 pos = transform.position;
    }

    IEnumerator DestroyThySelf()
    {
        yield return new WaitForSeconds(11);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 0.5f));
    }
}