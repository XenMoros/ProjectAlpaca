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

    public enum SliderType { Sensibility, Brightness, VolumeMaster, VolumeMusic, VolumeEffects, VolumeMenu};
    public SliderType sliderType;

    private void Awake()
    {
        switch (sliderType)
        {
            case SliderType.Brightness:
                slider.value = StaticManager.brightness;
                sliderValueText.text = StaticManager.brightness.ToString("0.0");
                break;
            case SliderType.Sensibility:
                slider.value = StaticManager.sensibility;
                sliderValueText.text = StaticManager.sensibility.ToString("0.0");
                break;
            case SliderType.VolumeMaster:
                slider.value = StaticManager.masterVolume;
                sliderValueText.text = Mathf.Round(StaticManager.masterVolume * 100).ToString();
                break;
            case SliderType.VolumeMusic:
                slider.value = StaticManager.musicVolume;
                sliderValueText.text = Mathf.Round(StaticManager.musicVolume * 100).ToString();
                break;
            case SliderType.VolumeEffects:
                slider.value = StaticManager.effectsVolume;
                sliderValueText.text = Mathf.Round(StaticManager.effectsVolume * 100).ToString();
                break;
            case SliderType.VolumeMenu:
                slider.value = StaticManager.menuVolume;
                sliderValueText.text = Mathf.Round(StaticManager.menuVolume * 100).ToString();
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
                sliderValueText.text = StaticManager.brightness.ToString("0.0");
                break;
            case SliderType.Sensibility:
                StaticManager.ChangeSensibility(slider.value);
                sliderValueText.text = StaticManager.sensibility.ToString("0.0");
                break;
            case SliderType.VolumeMaster:
            case SliderType.VolumeMusic:
            case SliderType.VolumeEffects:
            case SliderType.VolumeMenu:
                StaticManager.ChangeVolume(slider.value, sliderType);
                sliderValueText.text = Mathf.Round(slider.value * 100).ToString();
                break;
            default:
                break;
        }

    }

    public void OnMove(AxisEventData eventData)
    {
        if (StaticManager.pause)
        {
            switch (eventData.moveDir)
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (StaticManager.pause)
        {
            previousSliderValue = slider.value;
        }
    }
}
