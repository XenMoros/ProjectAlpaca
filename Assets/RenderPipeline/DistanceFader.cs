using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFader : MonoBehaviour
{
    Material material;
    Transform alpacaPosition;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        alpacaPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        material.SetVector("_Alpaca_Pos", alpacaPosition.position);
    }
}
