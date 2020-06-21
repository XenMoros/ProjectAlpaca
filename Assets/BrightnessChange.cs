using UnityEngine;
using UnityEngine.UI;

public class BrightnessChange : MonoBehaviour
{
    public Slider slider;

    public void OnSliderChange()
    {
        StaticManager.ChangeBrightness(slider.value);
    }
}
