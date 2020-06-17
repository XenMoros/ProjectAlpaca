//RealToon - Sobel Outline Effect (HDRP - Post Processing)
//MJQStudioWorks
//2020

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/RealToon/Sobel Outline")]
public sealed class SobelOutline : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;
    const string kShaderName = "Hidden/HDRP/RealToon/Effects/SobelOutline";

    [Space(10)]

    [Header("[RealToon Effects - Sobel Outline]")]

    [Space(10)]

    [Tooltip("Outline Width")]
    public MinFloatParameter OutlineWidth = new MinFloatParameter(0f, 0, true);

    [Tooltip("Outline Threshold")]
    public MinFloatParameter OutlineThreshold = new MinFloatParameter(50f, 0, true);

    [Space(10), Tooltip("Outline Color")]
    public ColorParameter OutlineColor = new ColorParameter(Color.black,true);

    [Tooltip("Outline Color Intensity")]
    public FloatParameter ColorIntensity = new FloatParameter(1f, true);

    [Tooltip("Mix Full Screen Color To Outline Color")]
    public BoolParameter MixFullScreenColor = new BoolParameter(true);

    [Space(10)]

    [Tooltip("Show the Outline only.")]
    public BoolParameter ShowOutlineOnly = new BoolParameter(false);


    public Material m_Material;

    public bool IsActive() => m_Material != null && OutlineWidth.value > 0f;

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        m_Material.SetFloat("_OutlineWidth", OutlineWidth.value);
        m_Material.SetFloat("_OutlineThreshold", OutlineThreshold.value);
        m_Material.SetColor("_OutlineColor", OutlineColor.value);
        m_Material.SetFloat("_OutlineColorIntensity", ColorIntensity.value);
        m_Material.SetFloat("_ColOutMiSel", MixFullScreenColor.value ? 1 : 0);
        m_Material.SetFloat("_OutOnSel", ShowOutlineOnly.value ? 1 : 0);
        m_Material.SetTexture("_InputTexture", source);
        
        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }

    public override void Cleanup() => CoreUtils.Destroy(m_Material);
}
