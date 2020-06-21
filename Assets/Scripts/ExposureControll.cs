
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ExposureControll : MonoBehaviour
{
    Exposure exposicion;

    void Start()
    {
        Volume volume = GetComponent<Volume>();
        if(volume.profile.TryGet<Exposure>(out Exposure exp))
        {
            exposicion = exp;
        }
        
        if(exposicion == null)
        {
            Debug.Log("ERRRROR");
        }
    }

    void Update()
    {
        if (StaticManager.exposureChange)
        {
            exposicion.compensation = new FloatParameter(StaticManager.brightness);
            StaticManager.exposureChange = false;
        }
    }
}
