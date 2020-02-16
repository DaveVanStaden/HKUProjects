using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtterCollector : MonoBehaviour
{
    public int points;
    public float MaxPoints;

    [SerializeField] private GameObject foodStuff;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private Text pointText;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] SoundEffects;

    private bool hitCooldown = true;
    //the max points are set to the static vallue given from the menu.
    private void Start()
    {
        MaxPoints = StaticVariable.MaxScore;
        print(MaxPoints);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pointText.text = "= " + points.ToString();

        if (points <= 0)
        {
            points = 0;
        }
    }

    private void VictoryState()
    {
        gameObject.GetComponent<OtterMovement>().VictoryState();
    }
    //if the object has the tag food or twopointfood it points up, if its danger the points go down. 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Food")
        {
            var foodstuffs = Instantiate(foodStuff, new Vector2(Random.Range(-8.3f, -6.7f), Random.Range(4.5f, 3.25f)), Quaternion.Euler(0, 0, Random.Range(-180, 180)));
            foodstuffs.GetComponent<SpriteRenderer>().sprite = collider.GetComponent<SpriteRenderer>().sprite;
            foodstuffs.GetComponent<SpriteRenderer>().sortingOrder += points + 1;
            foodstuffs.transform.SetParent(itemBox.transform);

            if (collider.transform.name == "TwoPointFood")
            {
                points += 2;
                audioSource.clip = SoundEffects[1];
            }
            else
            {
                points++;
                audioSource.clip = SoundEffects[0];
            }
            audioSource.Play();
            Destroy(collider.gameObject);
        }

        if (collider.transform.tag == "Danger" && hitCooldown)
        {
            points -= 2;
            audioSource.clip = SoundEffects[3];
            audioSource.Play();
            hitCooldown = false;
            StartCoroutine("ResetCooldown");
        }
    }
    //when the otter jumps and catches a hoop, it will be given even more points and a different sound is played.
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "Hoop")
        {
            points += 3;
            collider.GetComponent<ParticleSystem>().Play();
            audioSource.clip = SoundEffects[2];
            audioSource.Play();
        }
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(2.5f);
        hitCooldown = true;
    }
}