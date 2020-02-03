using UnityEngine;

public class Billboard : MonoBehaviour
{
    //Per que els Sprites del escupitajo mirin sempre a camara
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
