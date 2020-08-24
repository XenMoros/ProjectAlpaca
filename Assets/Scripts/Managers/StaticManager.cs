using UnityEngine;
using UnityEditor;
using System;

public static class StaticManager
{
    public delegate void StaticCanghesEvent();
    public static event StaticCanghesEvent OnCameraChange, OnBrightnessChange, OnPauseChange, OnMasterVolumeChange, OnMusicVolumeChange, OnEffectsVolumeChange;

    public static float brightness = 0;
    public static float sensibility = 5;
    public static float masterVolume = 1;
    public static float musicVolume = 1;
    public static float effectsVolume = 1;
    public static bool axisV = false, axisH = false;
    public static bool pause = true;

    public static float lastSensibility = 5;
    
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
        if(newState != pause)
        {
            pause = newState;
            OnPauseChange?.Invoke();
        }

    }
}