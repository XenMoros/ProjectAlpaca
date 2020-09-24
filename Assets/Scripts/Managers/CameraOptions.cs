using UnityEngine;
using Cinemachine;

public class CameraOptions : MonoBehaviour
{
    public CinemachineFreeLook cmCamara;
    public float defVelV = 5f, defVelH = 100f;
    public bool defInvertV, defInvertH;
    public int pauseActive = 0;

    private void Awake()
    {
        CameraPause();
    }

    private void OnEnable()
    {
        StaticManager.OnCameraChange += ChangeCameraOptions;
        StaticManager.OnPauseChange += CameraPause;
    }

    private void OnDisable()
    {
        StaticManager.OnCameraChange -= ChangeCameraOptions;
        StaticManager.OnPauseChange -= CameraPause;
    }

    private void ChangeCameraOptions()
    {
            cmCamara.m_XAxis.m_InvertInput = StaticManager.axisH;
            cmCamara.m_YAxis.m_InvertInput = StaticManager.axisV;

            cmCamara.m_XAxis.m_MaxSpeed = pauseActive * (defVelH / defVelV * StaticManager.sensibility);
            cmCamara.m_YAxis.m_MaxSpeed = pauseActive * (StaticManager.sensibility);
    }

    private void CameraPause()
    {
        pauseActive = StaticManager.pause ? 0: 1;
        ChangeCameraOptions();
    }
}
