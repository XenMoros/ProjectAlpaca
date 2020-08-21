
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
        }
    }

    private void OnEnable()
    {
        StaticManager.OnBrightnessChange += ExposureChanged;
    }

    private void OnDisable()
    {
        StaticManager.OnBrightnessChange -= ExposureChanged;
    }

    void ExposureChanged()
    {
        exposicion.compensation = new FloatParameter(StaticManager.brightness);
    }
}
