using UnityEngine;
using UnityEngine.UI;

public class SensibilityChanger : SliderChanger
{

    public override void OnSliderChange()
    {
        StaticManager.ChangeSensibility(slider.value,true);
    }
}
