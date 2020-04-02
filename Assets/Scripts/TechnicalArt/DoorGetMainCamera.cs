using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGetMainCamera : MonoBehaviour
{
    public Canvas canvas;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        canvas.worldCamera = mainCamera;
    }
}
