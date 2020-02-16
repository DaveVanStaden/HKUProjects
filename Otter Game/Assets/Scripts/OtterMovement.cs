using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtterMovement : MonoBehaviour
{
    private Rigidbody2D otterBody;

    private bool canMove = true;
    private bool jumpOnExitRaycast = false;
    private bool jumpOnce = true;
    private bool gameMovement = true;
    private bool fishFall = true;

    [SerializeField] private int jumpHeight;
    [SerializeField] private float jumpTimer;

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject pointText;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private GameObject itemBowl;

    [SerializeField] private Text victoryText;

    // Start is called before the first frame update
    void Start()
    {
        otterBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMovement)
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                MoveOtter();
                otterBody.velocity = Vector2.zero;
            }
            else
            {
                if (jumpOnExitRaycast)
                {
                    StartCoroutine("JumpCoolDown");
                }
                jumpOnExitRaycast = false;
            }

            boundaries();
        }

        if (transform.GetChild(1).GetComponent<OtterCollector>().points >= transform.GetChild(1).GetComponent<OtterCollector>().MaxPoints)
        {
            VictoryState();
        }
    }
    //the otter will move towards the location of input. the is made by the controller(or finger if testing)
    private void MoveOtter()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (Input.touchCount != 0) // && hit.collider != null)
        {
            //(hit.transform.name);
            //if (hit.transform.tag == "Otter")
            //{
            var pos = Camera.main.ScreenToWorldPoint(new Vector2((float)Input.GetTouch(0).position.x, (float)Input.GetTouch(0).position.y));
            transform.position = new Vector2(pos.x, pos.y + 1f);

            if (transform.position.y > 0.1)
            {
                transform.position = new Vector2(transform.position.x, 0.1f);
            }
            Turning();
            jumpOnExitRaycast = true;
            jumpOnce = true;
            transform.GetComponentInChildren<Animator>().SetInteger("AnimStates", 1);
            //}
        }
    }
    // The ottter will jump by increasing its velocity.
    private void OtterJump()
    {
        if (jumpOnce)
        {
            otterBody.velocity = new Vector2(jumpHeight / 2, jumpHeight);
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 18);
            transform.GetComponentInChildren<Animator>().SetInteger("AnimStates", 2); //2
            jumpOnce = false;
        }
    }
    //when the otter is turning the rotation gets slitghtly adjusted.
    private void Turning()
    {
        if (Input.touches[0].deltaPosition.y >= 0.01f && transform.GetChild(0).transform.localRotation.z <= 0.1f)
        {
            transform.GetChild(0).Rotate(0, 0, 0.2f * Input.touches[0].deltaPosition.y);
        }

        if (Input.touches[0].deltaPosition.y <= 0.01f && transform.GetChild(0).transform.localRotation.z >= -0.1f)
        {
            transform.GetChild(0).Rotate(0, 0, -0.2f * -Input.touches[0].deltaPosition.y);
        }
    }
    //there are boundrary's set for moving so that the player cant get off screen or above water without jumping.
    private void boundaries()
    {
        if (transform.position.y < -4.1)
        {
            transform.position = new Vector2(transform.position.x, -4.1f);
        }

        if (transform.position.y > 0.15)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (transform.position.y > 2.6)
        {
            StartCoroutine("StatusQuo");
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -18);
        }

        if (transform.position.x < -8.35)
        {
            transform.position = new Vector2(-8.35f, transform.position.y);
        }

        if (transform.position.x > 8.35)
        {
            transform.position = new Vector2(8.35f, transform.position.y);
        }

        if (transform.position.y > 2f)
        {
            otterBody.gravityScale = 1;
        }
        else
        {
            otterBody.gravityScale = 0;
        }
    }
    //if the game is over most of its functions will be deactivated before showing the victory screen.
    public void VictoryState()
    {
        transform.GetComponentInChildren<Animator>().SetInteger("AnimStates", 3);
        transform.GetChild(1).gameObject.SetActive(false);
        pointText.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine("DisplayVictory");
        gameMovement = false;
        otterBody.velocity = Vector2.zero;
    }
    //the jump from the otter has a cooldown to not get glitches.
    IEnumerator JumpCoolDown()
    {
        float endTime = Time.time + jumpTimer;

        while (Time.time < endTime)
        {
            if (Input.anyKey)
            {
                StopCoroutine("JumpCoolDown");
                yield break;
            }
            yield return null;
        }
        OtterJump();
    }

    IEnumerator StatusQuo()
    {
        yield return new WaitForSeconds(1.75f);
        otterBody.velocity = Vector2.zero;
        transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
        transform.GetComponentInChildren<Animator>().SetInteger("AnimStates", 0);
        otterBody.AddForce(transform.right * -350);
    }
    //after the game is over the victory screen will show/animation will activate.
    IEnumerator DisplayVictory()
    {
        yield return new WaitForSeconds(2.5f);
        victoryScreen.SetActive(true);
        victoryText.text = "Het is je gelukt om " + (transform.GetChild(1).GetComponent<OtterCollector>().points + " punten te behalen!");

        if (fishFall)
        {
            itemBowl.SetActive(true);
            for (int i = 0; i < itemBox.transform.childCount; i++)
            {
                itemBox.transform.GetChild(0 + i).transform.position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(7, 20));
                itemBox.transform.GetChild(0 + i).GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                itemBox.transform.GetChild(0 + i).GetComponent<Collider2D>().enabled = true;
            }
            fishFall = false;
        }
    }
}