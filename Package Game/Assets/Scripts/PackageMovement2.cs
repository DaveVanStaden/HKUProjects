using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageMovement2 : MonoBehaviour
{
    public float Speed;

    private void Start()
    {
        transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));
    }

    void Update()
    {
        if (Speed < 0.1)
        {
            Speed = -Settings.PackageMovementSpeed;
        }
        else
        {
            Speed = Settings.PackageMovementSpeed;
        }

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