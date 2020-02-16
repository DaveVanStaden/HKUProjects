using System.Collections.Generic;
using UnityEngine;

public class PackageMovement : MonoBehaviour
{
    public float Speed;

    public SpriteRenderer PackageRenderer;
    [SerializeField] private Sprite[] PackageSprites;

    private void Awake()
    {
        PackageRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        Vector2 velocity = new Vector2(Speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
