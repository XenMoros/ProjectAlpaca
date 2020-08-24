using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderChanger : MonoBehaviour, IMoveHandler, IEndDragHandler
{
    public Slider slider;
    public TextMeshProUGUI sliderValueText;

    public float step = 0.1f;
    private float previousSliderValue;

    public enum SliderType { Sensibility, Brightness, VolumeMaster, VolumeMusic, VolumeEffects};
    public SliderType sliderType;

    private void Awake()
    {
        switch (sliderType)
        {
            case SliderType.Brightness:
            case SliderType.Sensibility:
                sliderValueText.text = slider.value.ToString("0.0");
                break;
            case SliderType.VolumeMaster:
            case SliderType.VolumeMusic:
            case SliderType.VolumeEffects:
                sliderValueText.text = Mathf.Round(slider.value * 100).ToString();
                break;
            default:
                break;
        }

        previousSliderValue = slider.value;
    }

    public virtual void OnSliderChange()
    {
        switch (sliderType)
        {
            case SliderType.Brightness:
                StaticManager.ChangeBrightness(slider.value);
                sliderValueText.text = slider.value.ToString("0.0");
                break;
            case SliderType.Sensibility:
                StaticManager.ChangeSensibility(slider.value,true);
                sliderValueText.text = slider.value.ToString("0.0");
                break;
            case SliderType.VolumeMaster:
            case SliderType.VolumeMusic:
            case SliderType.VolumeEffects:
                StaticManager.ChangeVolume(slider.value, sliderType);
                sliderValueText.text = Mathf.Round(slider.value * 100).ToString();
                break;
            default:
                break;
        }

    }

    public void OnMove(AxisEventData eventData)
    {
        
        switch(eventData.moveDir)
        {
            case MoveDirection.Left:
                slider.value = previousSliderValue - step;
                break;
            case MoveDirection.Right:
                slider.value = previousSliderValue + step;
                break;
            default:
                break;
        }

        previousSliderValue = slider.value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        previousSliderValue = slider.value;
    }
}
