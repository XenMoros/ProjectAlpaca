using UnityEngine;
using UnityEditor;

public static class StaticManager
{
    [Range(-5,5)] public static sbyte brightness = 0;
    [Range(0.1f, 10)] public static float sensibility = 1;
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

    public static void ChangeSensibility(float sens)
    {
        sensibility = sens;
        cameraOptionsChanged = true;
    }
    public static void ChangeBrightness(int bright)
    {
        brightness = (sbyte) bright;
    }

    public static void SetPause(bool newState)
    {
        if(newState != pause)
        {
            pause = newState;
        }

    }
}