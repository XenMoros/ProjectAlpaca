using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agujero : MonoBehaviour
{
    public LayerMask layerMask;
    public MeshCollider sueloCollider;


    // Update is called once per frame
    void Update()
    {
        
        if(Physics.OverlapBox(transform.position, Vector3.one*0.2f,transform.rotation,layerMask)[0].CompareTag("Caja"))
        {
            Debug.Log("LOLASO");
            sueloCollider.enabled = false;
        }
        else if(!sueloCollider.enabled)
        {
            sueloCollider.enabled = true;
        }

    }    
}
