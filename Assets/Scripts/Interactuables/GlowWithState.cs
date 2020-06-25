using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Interactuable))]
public class GlowWithState : MonoBehaviour
{
    public Interactuable interactuable;
    public List<Renderer> objectRenderer;
    Material[] materials;

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
            foreach(Material mat in materials)
            {
                mat.SetFloat("_SelfLitIntensity", 1f);
            }
        }
        else
        {
            foreach (Material mat in materials)
            {
                mat.SetFloat("_SelfLitIntensity", 0.1f);
            }
        }
    }
}
