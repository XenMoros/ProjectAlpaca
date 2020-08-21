using UnityEngine;
using UnityEditor;

public static class StaticManager
{
    public delegate void StaticCanghesEvent();
    public static event StaticCanghesEvent OnCameraChange, OnBrightnessChange, OnPauseChange1;

    [Range(-5,5)] public static float brightness = 0;
    [Range(0.1f, 10)] public static float sensibility = 5;
    public static float lastSensibility = 5;
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

    public static void ChangeSensibility(float sens, bool slider = false)
    {
        if (slider)
        {
            lastSensibility = sens;
        }
        else
        {
            if (sens == 0 && sensibility != 0) lastSensibility = sensibility;
            sensibility = sens;
            OnCameraChange?.Invoke();
        }
    }

    public static void ChangeBrightness(float bright)
    {
        brightness = bright;
        OnBrightnessChange?.Invoke();
    }

    public static void SetPause(bool newState)
    {
        if(newState != pause)
        {
            pause = newState;
            OnPauseChange1?.Invoke();
        }

    }
}