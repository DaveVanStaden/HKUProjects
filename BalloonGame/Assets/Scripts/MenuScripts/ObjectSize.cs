using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    public static float ActualSize = 0.05f;

    void Start()
    {
        gameObject.transform.GetChild(1).transform.localScale = new Vector2(ActualSize, ActualSize);

        //print(ActualSize);
    }
}