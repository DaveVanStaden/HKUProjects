using UnityEngine;

public class FlyObjectManager : MonoBehaviour
{
    private ParticleSystem paricle;
    private Animator animator;

    private void Start()
    {
        paricle = GetComponent<ParticleSystem>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }
    /// <summary>
    /// In this script the balloon gets destroyed when it goes out of bounds to avoid performance issues.
    /// </summary>
    private void Update()
    {
        if (transform.GetChild(0).GetChild(0).localScale.x <= 0.75f)
        {
            paricle.Play();
        }

        if (transform.position.x >= 12 || transform.position.x <= -12 || transform.GetChild(0).GetChild(0).localScale.x <= 0)
        {
            Destroy(gameObject);
        }

        if (StaticVariable.gameIsOver && gameObject.tag == "Landed")
        {
            animator.speed = Random.Range(0.2f,1.5f);
            animator.SetInteger("AnimStates", 3);
        }
    }
}
