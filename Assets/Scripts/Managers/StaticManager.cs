
public static class StaticManager
{
    public delegate void StaticCanghesEvent();
    public static event StaticCanghesEvent OnCameraChange, OnBrightnessChange, OnPauseChange, OnMasterVolumeChange, OnMusicVolumeChange, OnEffectsVolumeChange, OnMenuVolumeChange;

    public static float brightness = -6f;
    public static float sensibility = 8f;

    public static float masterVolume = 1f;
    public static float musicVolume = 1f;
    public static float effectsVolume = 1f;
    public static float menuVolume = 1f;

    public static bool axisV = false, axisH = false;

    public static bool pause = true;

    
    public static void ChangeAxisV()
    {
        axisV = !axisV;
        OnCameraChange?.Invoke();
    }

    public static void ChangeAxisH()
    {
        axisH = !axisH;
        OnCameraChange?.Invoke();
    }

    public static void ChangeSensibility(float sens)
    {
        sensibility = sens;
        OnCameraChange?.Invoke();
    }

    internal static void ChangeVolume(float value, SliderChanger.SliderType sliderType)
    {
        switch(sliderType){
            case SliderChanger.SliderType.VolumeMaster:
                masterVolume = value;
                OnMasterVolumeChange?.Invoke();
                break;
            case SliderChanger.SliderType.VolumeMusic:
                musicVolume = value;
                OnMusicVolumeChange?.Invoke();
                break;
            case SliderChanger.SliderType.VolumeEffects:
                effectsVolume = value;
                OnEffectsVolumeChange?.Invoke();
                break;
            case SliderChanger.SliderType.VolumeMenu:
                menuVolume = value;
                OnMenuVolumeChange?.Invoke();
                break;
            default:
                break;
        }
    }

    public static void ChangeBrightness(float bright)
    {
        brightness = bright;
        OnBrightnessChange?.Invoke();
    }

    public static void SetPause(bool newState)
    {
        pause = newState;
        OnPauseChange?.Invoke();
    }
}