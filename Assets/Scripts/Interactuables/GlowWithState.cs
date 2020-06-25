using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Interactuable))]
public class GlowWithState : MonoBehaviour
{
    public Interactuable interactuable;
    public List<Renderer> objectRenderer;
    Material[] materials;

    float glow = 1f;

    private void Start()
    {
        materials = new Material[objectRenderer.Count];
        for(int i=0; i< objectRenderer.Count; i++)
        {
            materials[i] = objectRenderer[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(interactuable.active == true)
        {
            glow = Mathf.Clamp(glow + Time.deltaTime, 0.1f, 1f);
            
        }
        else
        {
            glow = Mathf.Clamp(glow - Time.deltaTime, 0.1f, 1f);
        }

        foreach (Material mat in materials)
        {
            mat.SetFloat("_SelfLitIntensity", glow);
        }
    }
}
