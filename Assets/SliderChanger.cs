using UnityEngine;
using UnityEngine.UI;

public class SliderChanger : MonoBehaviour
{
    public Slider slider;

    public enum SliderType { Sensibility, Brightness};
    public SliderType sliderType;

    public virtual void OnSliderChange()
    {
        switch (sliderType)
        {
            case SliderType.Brightness:
                StaticManager.ChangeBrightness(slider.value);
                break;
            case SliderType.Sensibility:
                StaticManager.ChangeSensibility(slider.value,true);
                break;
            default:
                break;
        }
    }
}
