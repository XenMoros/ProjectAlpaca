using UnityEngine;
using UnityEngine.UI;

public class BrightnessChange : SliderChanger
{

    public override void OnSliderChange()
    {
        StaticManager.ChangeBrightness(slider.value);
    }
}
