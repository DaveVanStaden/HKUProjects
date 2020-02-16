using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScroller : MonoBehaviour
{
    // Scroll main texture based on time
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * Settings.PackageMovementSpeed / 10;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}