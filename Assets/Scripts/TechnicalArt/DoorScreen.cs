﻿using UnityEngine;
using UnityEngine.Video;

public class DoorScreen : MonoBehaviour
{
    public VideoClip screen;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VideoPlayer>().clip = screen;
    }
}
