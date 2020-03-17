using UnityEngine;
using UnityEditor;

public static class StaticManager
{
    [Range(-5,5)] public static sbyte brightness = 0;
    [Range(0.2f, 10)] public static float sensibility = 1;
    public static bool axisV = false, axisH = false;
    
    public static void ChangeAxisV()
    {
        axisV = !axisV;
    }

    public static void ChangeAxisH()
    {
        axisH = !axisH;
    }

    public static void ChangeSensibility(float sens)
    {
        sensibility = sens;
    }
    public static void ChangeBrightness(int bright)
    {
        brightness = (sbyte) bright;
    }
}