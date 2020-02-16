using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSizeAssigner : MonoBehaviour
{
    private float ActualSize;
    void Start()
    {
        //changes the object size of specific objects.
        ActualSize = StaticVariable.ObjectSize;
        gameObject.transform.localScale = new Vector2(ActualSize, ActualSize);

        if (gameObject.tag == "Danger")
        {
            gameObject.transform.localScale = new Vector2(0.15f - ActualSize + 0.15f, 0.15f - ActualSize + 0.15f);
        }

        if (gameObject.name == "TwoPointFood")
        {
            gameObject.transform.localScale = new Vector2(ActualSize + 0.15f, ActualSize + 0.15f);
        }
    }
}
