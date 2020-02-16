using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer rend;

    [SerializeField] private Color colorChange = Color.white;

    public void ChangeMyColor()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorChange;

    }

}
