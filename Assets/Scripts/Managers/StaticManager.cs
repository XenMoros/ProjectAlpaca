using UnityEngine;
using UnityEditor;

public static class StaticManager
{
    [Range(-5,5)] public static float brightness = 0;
    public static bool exposureChange = false;
    [Range(0.1f, 10)] public static float sensibility = 5;
    public static float lastSensibility = 5;
    public static bool axisV = false, axisH = false;
    public static bool pause = true;
    public static bool cameraOptionsChanged = false;
    
    public static void ChangeAxisV()
    {
        axisV = !axisV;
        cameraOptionsChanged = true;
    }

    public static void ChangeAxisH()
    {
        axisH = !axisH;
        cameraOptionsChanged = true;
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
            cameraOptionsChanged = true;
        }
    }

    public static void ChangeBrightness(float bright)
    {
        brightness = bright;
        exposureChange = true;
    }

    public static void SetPause(bool newState)
    {
        if(newState != pause)
        {
            pause = newState;
        }

    }
}