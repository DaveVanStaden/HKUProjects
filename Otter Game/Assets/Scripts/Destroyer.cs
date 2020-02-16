using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // After a while the obstacles/foods destroy themselves to perserve memory.
    void Start()
    {
        StartCoroutine("DestroyThySelf");
    }

    IEnumerator DestroyThySelf()
    {
        yield return new WaitForSeconds(25);
        Destroy(gameObject);
    }
}
