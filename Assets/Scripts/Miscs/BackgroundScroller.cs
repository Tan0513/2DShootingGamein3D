using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    
    [SerializeField] Vector2 scrollvelocity;
    Material material;


    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        material.mainTextureOffset += scrollvelocity * Time.deltaTime;
    }
}
