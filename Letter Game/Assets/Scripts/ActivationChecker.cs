using UnityEngine;

public class ActivationChecker : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<WordAlgorithm>().GetComponent<WordAlgorithm>().AddPoint();
        //print("WELL WELL WELL");
    }
}