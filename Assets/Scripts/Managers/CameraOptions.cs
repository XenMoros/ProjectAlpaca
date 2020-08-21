﻿using UnityEngine;
using Cinemachine;

public class CameraOptions : MonoBehaviour
{
    public CinemachineFreeLook cmCamara;
    public float defVelV = 5f, defVelH = 100f;
    public bool defInvertV, defInvertH;

    private void OnEnable()
    {
        StaticManager.OnCameraChange += ChangeCameraOptions;
    }

    private void OnDisable()
    {
        StaticManager.OnCameraChange -= ChangeCameraOptions;
    }

    private void ChangeCameraOptions()
    {
            cmCamara.m_XAxis.m_InvertInput = StaticManager.axisH;
            cmCamara.m_YAxis.m_InvertInput = StaticManager.axisV;

            cmCamara.m_XAxis.m_MaxSpeed = defVelH / defVelV * StaticManager.sensibility;
            cmCamara.m_YAxis.m_MaxSpeed =  StaticManager.sensibility;

    }
}
