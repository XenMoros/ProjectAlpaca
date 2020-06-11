using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class CinematicByText : CinematicManager
{

    public TextAsset file;

    internal override void Start()
    {
        base.Start();
        
    }
    internal override void Update()
    {
        base.Update();
    }

    internal override void LateUpdate()
    {
        base.LateUpdate();
    }

    internal override void RellenarArrayAnimaciones()
    {
        string[] fLines = Regex.Split(file.text, "\n");

        arrayAnimaciones = new AnimationGuide[fLines.Length];

        for (int i = 0; i < fLines.Length; i++)
        {
            string valueLine = fLines[i];
            string[] values = Regex.Split(valueLine, ";");

            string name = values[0];
            float time =  float.Parse(values[1]);
            bool moving;

            if (int.Parse(values[2]) == 0) moving = false;
            else moving = true;

            float delay = float.Parse(values[3]);
            float finish = float.Parse(values[4]);

            arrayAnimaciones[i] = new AnimationGuide(name, time, moving, delay, finish);
        }

    }
}