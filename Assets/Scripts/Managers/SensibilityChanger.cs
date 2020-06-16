using UnityEngine;
using UnityEngine.UI;

public class SensibilityChanger : MonoBehaviour
{
    public Slider slider;

    public void OnSliderChange()
    {
        StaticManager.ChangeSensibility(slider.value);
    }
}
