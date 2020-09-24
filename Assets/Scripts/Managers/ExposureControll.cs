using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Text.RegularExpressions;

public class ExposureControll : MonoBehaviour
{
    public Volume volumeSettings;
    Exposure exposure;

    private void Awake()
    {
        for(int i=0; i < volumeSettings.profile.components.Count; i++)
        {
            if(Regex.IsMatch(volumeSettings.profile.components[i].name,"Exposure"))
            {
                exposure = (Exposure)volumeSettings.profile.components[i];
            }
        }

        StaticManager.OnBrightnessChange += ExposureChanged;
    }

    private void OnDisable()
    {
        StaticManager.OnBrightnessChange -= ExposureChanged;
    }

    void ExposureChanged()
    {
       exposure.fixedExposure.SetValue(new FloatParameter(StaticManager.brightness));
    }
}
