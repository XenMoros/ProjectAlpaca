﻿//RealToonGUI HDRP [Beta]
//MJQStudioWorks
//©2020

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

public class RealToonShaderGUI_HDRP_SRP : ShaderGUI
{

    #region foldout bools variable

    static bool ShowTextureColor;
    static bool ShowNormalMap;
    static bool ShowTransparency;
    static bool ShowMatCap;
    static bool ShowCutout;
    static bool ShowColorAdjustment;
    static bool ShowOutline;
    static bool ShowSelfLit;
    static bool ShowGloss;
    static bool ShowShadow;
    static bool ShowLighting;
    static bool ShowReflection;
    static bool ShowFReflection;
    static bool ShowRimLight;
    static bool ShowDepth;
    static bool ShowSeeThrough;
    static bool ShowDisableEnable;
    static bool ShowTessellation;
    static bool ShowUI = true;

    string LightBlendString = "Anime/Cartoon";
    static string ShowUIString = "Hide Settings";


    #endregion

    #region Variables

    string shader_type = "HDRP - Default";
    bool del_skw = false;
    static bool aruskw = false;

    #endregion

    #region Material Properties Variables

    MaterialProperty _Culling = null;
    MaterialProperty _TRANSMODE = null;

    MaterialProperty _MainTex = null;
    MaterialProperty _TexturePatternStyle = null;
    MaterialProperty _MainColor = null;
    MaterialProperty _MVCOL = null;
    MaterialProperty _MCIALO = null;

    MaterialProperty _MCapIntensity = null;
    MaterialProperty _MCap = null;
    MaterialProperty _SPECMODE = null;
    MaterialProperty _SPECIN = null;
    MaterialProperty _MCapMask = null;

    MaterialProperty _Cutout = null;
    MaterialProperty _UseSecondaryCutout = null;
    MaterialProperty _SecondaryCutout = null;
    MaterialProperty _AlphaBasedCutout = null;

    MaterialProperty _Opacity = null;
    MaterialProperty _TransparentThreshold = null;
    MaterialProperty _MaskTransparency = null;

    MaterialProperty _NormalMap = null;
    MaterialProperty _NormalMapIntensity = null;

    MaterialProperty _Saturation = null;

    MaterialProperty _OutlineWidth = null;
    MaterialProperty _OutlineWidthControl = null;

    MaterialProperty _N_F_O_NM = null;
    MaterialProperty _ONormMap = null;
    MaterialProperty _ONormMapInt = null;

    MaterialProperty _OutlineExtrudeMethod = null;
    MaterialProperty _OutlineOffset = null;
    MaterialProperty _OutlineZPostionInCamera = null;
    MaterialProperty _DoubleSidedOutline = null;
    MaterialProperty _OutlineColor = null;
    MaterialProperty _MixMainTexToOutline = null;
    MaterialProperty _NoisyOutlineIntensity = null;
    MaterialProperty _DynamicNoisyOutline = null;
    MaterialProperty _LightAffectOutlineColor = null;
    MaterialProperty _OutlineWidthAffectedByViewDistance = null;
    MaterialProperty _FarDistanceMaxWidth = null;
    MaterialProperty _VertexColorBlueAffectOutlineWitdh = null;
    MaterialProperty _TOAO = null;

    MaterialProperty _SelfLitIntensity = null;
    MaterialProperty _SelfLitColor = null;
    MaterialProperty _SelfLitPower = null;
    MaterialProperty _TEXMCOLINT = null;
    MaterialProperty _SelfLitHighContrast = null;
    MaterialProperty _MaskSelfLit = null;

    MaterialProperty _GlossIntensity = null;
    MaterialProperty _Glossiness = null;
    MaterialProperty _GlossSoftness = null;
    MaterialProperty _GlossColor = null;
    MaterialProperty _GlossColorPower = null;
    MaterialProperty _MaskGloss = null;

    MaterialProperty _GlossTexture = null;
    MaterialProperty _GlossTextureSoftness = null;
    MaterialProperty _PSGLOTEX = null; 
    MaterialProperty _GlossTextureRotate = null;
    MaterialProperty _GlossTextureFollowObjectRotation = null;
    MaterialProperty _GlossTextureFollowLight = null;

    MaterialProperty _OverallShadowColor = null;
    MaterialProperty _OverallShadowColorPower = null;
    MaterialProperty _SelfShadowShadowTAtViewDirection = null;

    MaterialProperty _ReduSha = null;
    MaterialProperty _ShadowHardness = null;

    MaterialProperty _HighlightColor = null;
    MaterialProperty _HighlightColorPower = null;

    MaterialProperty _SelfShadowRealtimeShadowIntensity = null;
    MaterialProperty _SelfShadowThreshold = null;
    MaterialProperty _VertexColorGreenControlSelfShadowThreshold = null;
    MaterialProperty _SelfShadowHardness = null;
    MaterialProperty _SelfShadowRealTimeShadowColor = null;
    MaterialProperty _SelfShadowRealTimeShadowColorPower = null;
    MaterialProperty _SelfShadowAffectedByLightShadowStrength = null;

    MaterialProperty _SmoothObjectNormal = null;
    MaterialProperty _VertexColorRedControlSmoothObjectNormal = null;
    MaterialProperty _XYZPosition = null;
    MaterialProperty _XYZHardness = null;
    MaterialProperty _ShowNormal = null;

    MaterialProperty _ShadowColorTexture = null;
    MaterialProperty _ShadowColorTexturePower = null;

    MaterialProperty _ShadowTIntensity = null;
    MaterialProperty _ShadowT = null;
    MaterialProperty _ShadowTLightThreshold = null;
    MaterialProperty _ShadowTShadowThreshold = null;
    MaterialProperty _ShadowTColor = null;
    MaterialProperty _ShadowTColorPower = null;
    MaterialProperty _ShadowTHardness = null;
    MaterialProperty _STIL = null;
    MaterialProperty _N_F_STIS = null;
    MaterialProperty _N_F_STIAL = null;
    MaterialProperty _ShowInAmbientLightShadowIntensity = null;
    MaterialProperty _ShowInAmbientLightShadowThreshold = null;
    MaterialProperty _LightFalloffAffectShadowT = null;

    MaterialProperty _PTexture = null;
    MaterialProperty _PTCol = null;
    MaterialProperty _PTexturePower = null;

    MaterialProperty _RELG = null;
    MaterialProperty _EnvironmentalLightingIntensity;

    MaterialProperty _GIFlatShade = null;
    MaterialProperty _GIShadeThreshold = null;
    MaterialProperty _LightAffectShadow = null;
    MaterialProperty _LightIntensity = null;

    MaterialProperty _UseTLB = null;
    MaterialProperty _N_F_PAL = null;
    MaterialProperty _N_F_AL = null;

    MaterialProperty _DirectionalLightIntensity = null;
    MaterialProperty _PointSpotlightIntensity = null;
    MaterialProperty _ALIntensity = null;
    MaterialProperty _N_F_ALSL = null;
    MaterialProperty _ALTuFo = null;
    MaterialProperty _LightFalloffSoftness = null;

    MaterialProperty _CustomLightDirectionIntensity = null;
    MaterialProperty _CustomLightDirectionFollowObjectRotation = null;
    MaterialProperty _CustomLightDirection = null;

    MaterialProperty _ReflectionIntensity = null;
    MaterialProperty _ReflectionRoughtness = null;
    MaterialProperty _RefMetallic = null;
    MaterialProperty _MaskReflection = null;
    MaterialProperty _FReflection = null;

    MaterialProperty _RimLigInt = null;
    MaterialProperty _RimLightUnfill = null;
    MaterialProperty _RimLightColor = null;
    MaterialProperty _RimLightColorPower = null;
    MaterialProperty _RimLightSoftness = null;
    MaterialProperty _RimLightInLight = null;
    MaterialProperty _LightAffectRimLightColor = null;

    //MaterialProperty _TessellationSmoothness;
    //MaterialProperty _TessellationTransition;
    //MaterialProperty _TessellationNear;
    //MaterialProperty _TessellationFar;

    MaterialProperty _RefVal = null;
    MaterialProperty _Oper = null;
    MaterialProperty _Compa = null;

    MaterialProperty _N_F_MC = null;
    MaterialProperty _N_F_NM = null;
    MaterialProperty _N_F_CO = null;
    MaterialProperty _N_F_O = null;
    MaterialProperty _N_F_CA = null;
    MaterialProperty _N_F_SL = null;
    MaterialProperty _N_F_GLO = null;
    MaterialProperty _N_F_GLOT = null;
    MaterialProperty _N_F_SS = null;
    MaterialProperty _N_F_SON = null;
    MaterialProperty _N_F_SCT = null;
    MaterialProperty _N_F_ST = null;
    MaterialProperty _N_F_PT = null;
    MaterialProperty _N_F_CLD = null;
    MaterialProperty _N_F_R = null;
    MaterialProperty _N_F_FR = null;
    MaterialProperty _N_F_RL = null;
    MaterialProperty _N_F_HDLS = null;
    MaterialProperty _N_F_HPSAS = null;
    MaterialProperty _ZWrite = null;
    MaterialProperty _ZTest = null;
    MaterialProperty _N_F_DCS = null;
    MaterialProperty _N_F_CS = null;
    MaterialProperty _N_F_NLASOBF = null;

    #endregion

    #region List of Toggle Keywords

    enum SFKW
    {
        N_F_TRANS_ON,
        N_F_USETLB_ON,
        N_F_STIS_ON,
        N_F_STIAL_ON,
        N_F_PAL_ON,
        N_F_AL_ON,
        N_F_ALSL_ON,
        N_F_MC_ON,
        N_F_NM_ON,
        N_F_CO_ON,
        N_F_O_ON,
        N_F_O_NM_ON,
        N_F_CA_ON,
        N_F_SL_ON,
        N_F_GLO_ON,
        N_F_GLOT_ON,
        N_F_SS_ON,
        N_F_SON_ON,
        N_F_SCT_ON,
        N_F_ST_ON,
        N_F_PT_ON,
        N_F_RELGI_ON,
        N_F_CLD_ON,
        N_F_R_ON,
        N_F_FR_ON,
        N_F_RL_ON,
        N_F_HDLS_ON,
        N_F_HPSS_ON,
        N_F_DCS_ON,
        N_F_NLASOBF_ON,
        N_F_CS_ON,
        _NORMALMAP_TANGENT_SPACE
    }

    #endregion

    #region TOTIPS

    string[] TOTIPS =
    {

        //Culling [0]
        "Controls which sides of polygons should be culled (not drawn).\n\n\nBack: Don’t render polygons that are facing away from the viewer.\n\nFront: Don’t render polygons that are facing towards the viewer, Used for turning objects inside-out.\n\nOff: Disables culling - all faces are drawn, This also called Double Sided." ,

        //Texture [1]
        "Main or base texture." , 

        //Texture Pattern Style [2]
        "Turn the 'Main/Base Texture' into pattern style." ,

        //Main Color [3]
        "Main or base color." ,

        //Mix Vertex Color [4]
        "Mix or show vertex color." ,

        //Main Color in Ambient Light Only [5]
        "Put the 'Main/Base Color' into ambient light." ,

        //Highlight Color [6]
        "Highlight color." ,

        //Highlight Color Power [7]
        "'Highlight Color' power or intensity." ,

        //Intensity [8] [MatCap]
        "MatCap intensity." ,

        //MatCap [9] [MatCap]
        "MatCap texture." ,

        //Specualar Mode [10] [MatCap]
        "Turn MatCap into specular." ,

        //Specular Power [11] [MatCap]
        "Specular intensity or power." ,

        //Mask MatCap [12] [MatCap]
        "Mask MatCap.\n\nUse a Black and White texture map.\nWhite means visible matcap while Black is not." ,

        //Cutout [13]
        "Cutout value or threshold." ,

        //Alpha Based Cutout [14] 
        "It will use the alpha/transparent channel of the 'Main/Base Texture' to cutout." ,

        //Use Secondary Cutout [15]
        "This will use the 'Secondary Cutout' to do the cutout." ,

        //Secondary Cutout [16]
        "Secondary texture cutout.\n\nUse a Black and White texture map.\nWhite means not cut out while Black is cutout." ,

        //Opacity [17] [Transparency]
        "Transparent - Opaque" ,

        //Transparent Threshold [18]
        "'Main/Base Texture' transparency threshold." ,

        //Mask Transparency [19]
        "Mask Transparency.\n\nWhite means opaque while Black means transparent." ,

        //Normal Map [20]
        "Normal Map." ,

        //Normal Map Intensity [21]
        "'Normal Map' intensity." ,

        //Saturation [22] [Color Adjustment]
        "Color saturation of the object." ,

        //Width [23] [Outline]
        "Outline main width." ,

        //Width Control [24] [Outline]
        "Controls the 'Outline Width' using texture Map.\n\nUse a Black and White texture map.\nWhite means 1 while Black means 0.\nThis will not work if the Outline main width value is 0." ,

        //Outline Extrude Method [25]
        "Outline Extrude Methods.\n\nNormal - The outline extrusion will be based on normal direction.\n\nOrigin - The outline extrusion will be based on the center of the object." ,

        //Outline Offset [26]
        "Outline XYZ position." ,

        //Double Sided Outline [27]
        "Show the front side of the outline.\n\nUseful for plane object.\n'Outline Z Position In Camera' option is needed to be adjust to show the object." ,

        //Color [28] [Outline]
        "Outline color." ,

        //Mix Main Texture To Outline [29]
        "Mix 'Main/Base Texture' to oultine." ,

        //Noisy Outline Intensity [30]
        "The power/intensity of outline distortion or noise." ,

        //Dynamic Noisy Outline [31]
        "Moving noisy or distort outline." ,

        //Light Affect Outline Color [32]
        "Light (Brightness and Color) affect Outline color." ,

        //Outline Width Affected By View Distance [33]
        "'Outline Width' affected by view distance." ,

        //Far Distance Max Width [34] [Outline]
        "The maximum 'Outline Width' limit when moving far from the object." ,

        //Vertex Color Blue Affect Outline Width [35] [Outline]
        "'Vertex Color Blue' will affect the Outline Width.\n\nThis will not work if the Outline main width value is 0.\n\nBlue color means 1 while Black color means 0." ,

        //Intensity [36] [SelfLit]
        "'Self Lit' intensity." ,

        //Color [37] [SelfLit]
        "Self Lit color" ,

        //Power [38] [SelfLit]
        "'Self Lit Color' power or intensity." ,

        //Texture and Main Color Intensity [39] [SelfLit]
        "'Main/Base Texture' and 'Main/Base Color' intensity.\n\nAdjust this if the 'Main/Base Texture' and 'Main/Base Color' is too strong or too bright for Self Lit." ,

        //High Contrast [40] [SelfLit]
        "Turn Self Lit into high contrast colors and mix 'Base/Main Texture' twice." ,

        //Mask Self Lit [41]
        "Mask Self Lit.\n\nUse a Black and White texture map.\nWhite means visible Self Lit while Black is not." ,

        //Gloss Intensity [42]
        "Gloss intensity." ,

        //Glossiness [43]
        "Glossiness." ,

        //Softness [44] [Gloss]
        "The softness of the gloss." ,

        //Color [45] [Gloss]
        "Gloss color" ,

        //Power [46] [Gloss]
        "'Gloss Color' power or intensity." ,

        //Mask Gloss [47]
        "Mask Gloss.\n\nWhite means visible Gloss while black is not." ,

        //Gloss Texture [48]
        "A Black and White texture map to be used as gloss.\n\nWhite means gloss while Black is not." ,

        //Softness [49] [Gloss Texture]
        "The softness of the 'Gloss Texture'." ,

        //Pattern Style [50] [Gloss Texture]
        "Turn 'Gloss Texture' into pattern style." ,

        //Rotate [51] [Gloss Texture]
        "Rotate 'Gloss Texture'." ,

        //Follow Object Rotation [52] [Gloss Texture]
        "'Gloss Texture' will follow the object local rotation." ,

        //Follow Light [53] [Gloss Texture]
        "'Gloss Texture' will follow the light direction or position." ,

        //Overall Shadow Color [54]
        "Overall shadow color.\n\nThis will affect Realtime Shadow, Self Shadow/Shade and ShadowT." ,

        //Overall Shadow Color Power [55]
        "'Overall shadow Color' power or intensity." ,

        //Self Shadow & ShadowT At View Direction [56]
        "'Self Shadow' and 'ShadowT' follow your view or camera view direction." ,

        //Reduce Shadow [57]
        "The amount of reduce self cast shadow.\n\nThis will affect all lights." ,

        //Shadow Hardness [58]
        "Real time shadow hardness" ,

        //Threshold [59] [Self Shadow]
        "The amount of 'Self Shadow/Shade' on the object." ,

        //Vertex Color Green Control Self Shadow Threshold [60]
        "Controls 'Self Shadow Threshold' by using vertex color Green." ,

        //Hardness [61] [Self Shadow]
        "'Self Shadow/Shade' hardness." ,

        //Self Shadow & Real Time Shadow Color [62]
        "'Self Shadow and Real Time Shadow Color'.\n\nBefore you set/change this, Set 'Overall Shadow Color' to White." ,

        //Self Shadow & Real Time Shadow Color Power [63]
        "'Self Shadow and Real Time Shadow Color' power or intensity." ,

        //Self Shadow Affected By Light Shadow Strength [64]
        "Light shadow strength will affect self shadow visibility." ,

        //Smooth Object Normal [65]
        "The amount of smooth object normal." ,

        //Vertex Color Red Control Smooth Object Normal [66]
        "'Vertex color Red' controls the amount of smooth object normal.\n\nRed means 1 while Black means 0." ,

        //XYZ Position [67] [Smooth Object Normal]
        "Normal's XYZ positions." ,

        //XYZ Hardness [68] [Smooth Object Normal]
        "To harden the look of the Normal for easy adjustment." ,

        //Show Normal [69] [Smooth Object Normal]
        "Show the normal of the object." ,

        //Shadow Color Texture [70]
        "A texture to color shadow.\n\nThis includes (RealTime Shadow, Self Shadow/Shade and ShadowT.\nYou can also use your 'Main/Base Texture' and adjust 'Power' to make it dark." ,

        //Power [71] [Shadow Color Texture]
        "How strong or dark the 'Shadow Color Texture'." ,

        //Intensity [72] [ShadowT]
        "'ShadowT' intensity or visibility." ,

        //ShadowT [73]
        "ShadowT or Shadow Texture, shadows in texture form.\n\nUse Black or Gray and White Flat, Gradient and Smooth texture map.\nGray and White affected by light while Black is not.\n\nFor more info and how to use and make ShadowT texture maps, see 'Video Tutorials' and 'User Guide.pdf' at the bottom of this RealToon inspector.",

        //Light Threshold [74] [ShadowT]
        "The amount of light." ,

        //Shadow Threshold [75] [ShadowT]
        "The amount of ShadowT." ,

        //Hardness [76] [ShadowT]
        "'ShadowT' hardness." ,

        //Show In Shadow [77] [ShadowT]
        "Show 'ShadowT' in shadow.\n\nThis will only be visible if realtime shadow and self shadow/shade color is not Black." ,

        //Show In Ambient Light [78] [ShadowT]
        "Show 'ShadowT' in Ambient Light.\n\nThis will only be visible if there's an Ambient Light present or GI." ,

        //Show In Ambient Light & Shadow Intensity [79] [ShadowT]
        "'ShadowT' intensity or visibility in shadow and ambient light." ,

        //Show In Ambient Light & Shadow Threshold [80] [ShadowT]
        "'ShadowT' threshold in Ambient Light and shadow." ,

        //Light Falloff Affect ShadowT [81]
        "'Point light' and 'Spot Light' light falloff affect 'ShadowT'." ,

        //PTexture [82]
        "A Black and White texture to be used as pattern for shadow.\n\nBlack means pattern while White is nothing.\nThis will not be visible if the shadow color is Black." ,

        //Power [83] [PTexture]
        "How strong or dark the pattern is." ,

        //Receive Environmental Ligthing and GI [84] [Lighting]
        "Turn on or off receive 'Environmental Ligthing' or 'GI'." ,

        //Environmental Ligthing Intensity [85] [Lighting]
        "Ambient Light, GI or Environmental Ligthing intensity on the object." ,

        //GI Flat Shade [86] [Lighting]
        "Turn GI or SH lighting shade into flat shade." ,

        //GI Shade Threshold [87] [Lighting]
        "The amount of GI Shade on the object." ,

        //Light affect Shadow [88] [Lighting]
        "Light intensity and color affect shadows.\n\nThis will affect (RealTime shadow, Self Shadow and ShadowT)." ,

        //Directional Light Intensity [89] [Lighting]
        "Directional Light intensity received on the object." ,

        //Point and Spot Light Intensity [90] [Lighting]
        "Point and Spot light intensity received on the object." ,

        //Light Falloff Softness [91] [Lighting]
        "How soft is the point and spot light light falloff." ,

        //Intensity [92] [Custom Light Direction]
        "The amount of custom light direction." ,

        //Custom Light Direction [93] [Custom Light Direction]
        "XYZ light position." ,

        //Follow Object Rotation [94] [Custom Light Direction]
        "'Custom Light Direction' follow object rotation." ,

        //Intensity [95] [Reflection]
        "The amount reflection visibility." ,

        //Roughtness [96] [Reflection]
        "'Reflection' roughtness." ,
        
        //Metallic [97] [Reflection]
        "The amount of reflection metallic look." ,
        
        //Mask Reflection [98]
        "Mask Reflection.\n\nWhite means visible relfection while Black means reflection not visible." ,

        //FReflection [99]
        "A texture or image to be used as reflection." ,

        //Unfill [100] [Rim Light]
        "Unfill 'Rim Light' on the object." ,

        //Softness [101] [Rim Light]
        "'Rim Light' softness." ,

        //Light Affect Rim Light [102] [Rim Light]
        "Light (Brightness and Color) affect 'Rim Light'." ,

        //Color [103] [Rim Light]
        "'Rim Light' color." ,

        //Color Power [104] [Rim Light]
        "'Rim Light Color' power or intensity." ,

        //Rim Light In Light [105]
        "'Rim Light' will be visible in light only." ,

        //ID [106] [See Through]
        "ID or reference value." ,

        //Set A [107] [See Through]
        "'A' The see through object while 'B' is the object to be seen through A'." ,

        //Set B [108] [See Through]
        "'A' The see through object while 'B' is the object to be seen through A'." ,

        //No Light and Shadow On Backface [109]
        "No light and shadow will be visible on a back of a plane/flat object or face.\n\nThis will only take effect or visible if 'Culling' is Off or Front." ,

        //Hide Directional Light Shadow [110]
        "Hide received 'Directional Light' shadows on the object." ,

        //Hide Point, Spot & Area Lights Shadow [111]
        "Hide received 'Point, Spot and Area Lights' shadows on the object." ,

        //Disable Cast Shadow [112] 
        "Disable object cast shadow." ,

        //ZWrite [113]
        "Turn on or off ZWrite.\n\nLeave this On for solid look.\nLeave this Off for semitransparent effects." ,

        //Automatic Remove Unused Shader Keywords [114]
        "Remove unused shader keywords automatically in all materials with Realtoon Shader. This will take effect once this enabled and when the RealToon Inspector shown. Disable this if you experience too slow Inspector.\n\n(Warning: This will also remove stored previous shaders shader keywords.)",

        //Reduce Outline Backface [115] [Outline] 
        "Reduce outline backface." ,

        //Outline Z Position In Camera [116] [Outline]
        "Adjust the outline Z position in camera space." ,

        //RealTime Shadow Intensity [117] [Shadow]
        "Adjust the realtime shadow intensity." ,

        //Self Shadow Intensity [118] [Shadow] 
        "Adjust the 'Self Shadow' shadow intensity." ,

        //Self Shadow & RealTime Shadow Intensity [119] [Shadow]
        "Adjust the 'Self Shadow' and realtime shadow intensity." ,

        //Color [120] [ShadowT]
        "'ShadowT' color.\n\nBefore you set/change this, Set 'Overall Shadow Color' to White." ,

        //Color Power [121] [ShadowT]
        "'ShadowT' color power or intensity.",

        //Ignore Light [122] [ShadowT]
        "'ShadowT' ignore direction light or light position.",

        //Light Intensity [123] [Lighting]
        "Light intensity in shadow.",

        //Enable Punctual Lights [124] [Lighting]
        "Enable punctual lights like Point and Spot lights.",

        //Use Traditional Light Blend [125] [Lighting]
        "Use traditional light blend.\n\nIf enabled light blending will be in add mode, if not enabled the light blending will based on high or maximum light intensity and the blending will be similar to Anime or Cartoon.",

        //Enable Area Light [126] [Lighting]
        "Enable Area light.\n\nCurrent Issue And Notes:\n   *If the size of a Rect type arealight is too un-even like 'X:24 | Y:1' the light fall off will cause some noise.\n   *Current arealight light falloff is just a temporary solution.\n   These current issues only applies when 'Area Light Smooth Look' option disabled.",

        //Area Light Intensity [127] [Lighting]
        "Area Light intensity received on the object.",

        //Transparent Mode [128]
        "Setting the current mode from Opaque to Transparent.",

        //Transparent Opacity Affect Outline [129] [Outline]
        "Transparent opacity affect outline.",

        //Enchance outline using Normal Map [130] [Outline]
        "Enchance outline using Normal Map.\n\n Actually it will just enhance the normal direction using Normal Map.",

        //Normal Map [131] [Outline]
        "Normal Map", 

        //Normal Map Intensity [132] [Outline]
        "Normal Map Intensity",

        //Color [133] [PTexture]
        "'PTexture' color.",

        //Area Light Intensity [134] [PTexture]
        "Area light intensity received on the object.",

        //Tube light falloff [135] [Arealight]
        "Arealight (Tube Type) light falloff.\n\nThis will adjust the light falloff of a tube type area light.\nThis is a temporary option for now.",

        //Rim Light Intensity [136] [Rimlight]
        "'Rim Light' intensity.",

        //Hide Contact Shadow [137]
        "Hide contact shadows on the object.",

        //ZTest [138]
        "For more info about this, see Unity's manual",

        //Area Light Smooth Look [139]
        "Making the object to look smooth when using Area Light.\n\n*'Tube light falloff' will be disabled and 'Light Falloff Softness' will not affect Area Light light falloff."

    };

    #endregion

    #region TOTIPS for EnDisFeatures

    string[] TOTIPSEDF =
    {
        //MatCap [0]
        "MatCap or Material Capture.",

        //Normal Map [1]
        "Normal Map.",

        //Outline [2]
        "Outline.\n\nCurrent Issue and Notes:\n     *In normal/opaque state far fog will overlap but if there is an opaque object behind, far fog will not overlap.\n   *In 'Transparent Mode' far fog will not overlap.",

        //Cutout [3]
        "Cutout.\n\n*You need to enable 'Transparent Mode' to be able to use this.\n*If this enabled the 'Transparent - Opacity' option will be disabled.",

        //Color Adjustment [4]
        "Adjust the color of the object.",

        //SelfLit [5]
        "Own light or Emission.",

        //Gloss [6]
        "Gloss.",

        //Gloss Texture [7]
        "Gloss in texture form.\n\nUse a Black and White texture map.\nWhite means gloss while Black is not.",

        //Self Shadow [8]
        "Self Shadow or Shade.",

        //Smooth Object Normal [9]
        "Smooth object normal or ignore object normal.",

        //Shadow Color Texture [10]
        "Color shadow using texture.\n\nBefore you use this, Set 'Overall Shadow Color' to White.",

        //ShadowT [11]
        "ShadowT or Shadow Texture, shadows in texture form.\n\nUse Black or Gray and White Flat, Gradient and Smooth texture map.\nGray and White affected by light while Black is not.\n\nFor more info and how to use and make ShadowT texture maps, see 'Video Tutorials' and 'User Guide.pdf' at the bottom of this RealToon inspector.",

        //PTexture [12]
        "PTexture or Pattern Texture.\n\nA Black and White texture to be used as pattern for shadow.\n\nBlack means pattern while White is nothing.\nThis will not be visible if the shadow color is Black.\n\nBefore you use this, Set 'Overall Shadow Color' to White.",

        //Custom Light Direction [13]
        "Custom light direction.",

        //Reflection [14]
        "Reflection.",

        //FReflection [15]
        "FReflection or Fake Reflection.\n\nUse any texture or image as reflection.\n\n*You need to enable reflection feature first before you can use this.",

        //Rim Light [16]
        "Rim light or fresnel effect.",

    };

    #endregion

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        //This Material
        Material targetMat = materialEditor.target as Material;

        //Settings
        materialEditor.SetDefaultGUIWidths();

        //Content

        //Might remove it soon.
        #region Type Name Switch

        //switch (targetMat.renderQueue)
        //{
        //    case 2000:
        //        shader_name = "default_d";
        //        shader_type = "HDRP - Default (In Development)";
        //        break;
        //    case 3000:
        //        shader_name = "default_ft";
        //        shader_type = "HDRP - Fade Transperancy"; 
        //        break;
        //    default:
        //        shader_name = string.Empty;
        //        shader_type = string.Empty;
        //        break;
        //}


        #endregion

        #region Material Properties

        _UseTLB = ShaderGUI.FindProperty("_UseTLB", properties);
        _Culling = ShaderGUI.FindProperty("_Culling", properties);
        _TRANSMODE = ShaderGUI.FindProperty("_TRANSMODE", properties);

        _MainTex = ShaderGUI.FindProperty("_MainTex", properties);
        _TexturePatternStyle = ShaderGUI.FindProperty("_TexturePatternStyle", properties);

        _MainColor = ShaderGUI.FindProperty("_MainColor", properties);

        _MVCOL = ShaderGUI.FindProperty("_MVCOL", properties);
        _MCIALO = ShaderGUI.FindProperty("_MCIALO", properties);

        _MCapIntensity = ShaderGUI.FindProperty("_MCapIntensity", properties);
        _MCap = ShaderGUI.FindProperty("_MCap", properties);
        _SPECMODE = ShaderGUI.FindProperty("_SPECMODE", properties);
        _SPECIN = ShaderGUI.FindProperty("_SPECIN", properties);
        _MCapMask = ShaderGUI.FindProperty("_MCapMask", properties);

        _Cutout = ShaderGUI.FindProperty("_Cutout", properties);
        _UseSecondaryCutout = ShaderGUI.FindProperty("_UseSecondaryCutout", properties);
        _SecondaryCutout = ShaderGUI.FindProperty("_SecondaryCutout", properties);
        _AlphaBasedCutout = ShaderGUI.FindProperty("_AlphaBasedCutout", properties);

        _Opacity = ShaderGUI.FindProperty("_Opacity", properties);
        _TransparentThreshold = ShaderGUI.FindProperty("_TransparentThreshold", properties);
        _MaskTransparency = ShaderGUI.FindProperty("_MaskTransparency", properties);

        _NormalMap = ShaderGUI.FindProperty("_NormalMap", properties);
        _NormalMapIntensity = ShaderGUI.FindProperty("_NormalMapIntensity", properties);

        _Saturation = ShaderGUI.FindProperty("_Saturation", properties);

        _OutlineWidth = ShaderGUI.FindProperty("_OutlineWidth", properties);
        _OutlineWidthControl = ShaderGUI.FindProperty("_OutlineWidthControl", properties);
        _OutlineExtrudeMethod = ShaderGUI.FindProperty("_OutlineExtrudeMethod", properties);

        _N_F_O_NM = ShaderGUI.FindProperty("_N_F_O_NM", properties);
        _ONormMap = ShaderGUI.FindProperty("_ONormMap", properties);
        _ONormMapInt = ShaderGUI.FindProperty("_ONormMapInt", properties);

        _OutlineOffset = ShaderGUI.FindProperty("_OutlineOffset", properties);
        _OutlineZPostionInCamera = ShaderGUI.FindProperty("_OutlineZPostionInCamera", properties);
        _DoubleSidedOutline = ShaderGUI.FindProperty("_DoubleSidedOutline", properties);
        _OutlineColor = ShaderGUI.FindProperty("_OutlineColor", properties);
        _MixMainTexToOutline = ShaderGUI.FindProperty("_MixMainTexToOutline", properties);
        _NoisyOutlineIntensity = ShaderGUI.FindProperty("_NoisyOutlineIntensity", properties);
        _DynamicNoisyOutline = ShaderGUI.FindProperty("_DynamicNoisyOutline", properties);
        _LightAffectOutlineColor = ShaderGUI.FindProperty("_LightAffectOutlineColor", properties);
        _OutlineWidthAffectedByViewDistance = ShaderGUI.FindProperty("_OutlineWidthAffectedByViewDistance", properties);
        _FarDistanceMaxWidth = ShaderGUI.FindProperty("_FarDistanceMaxWidth", properties);
        _TOAO = ShaderGUI.FindProperty("_TOAO", properties);
        _VertexColorBlueAffectOutlineWitdh = ShaderGUI.FindProperty("_VertexColorBlueAffectOutlineWitdh", properties);

        _SelfLitIntensity = ShaderGUI.FindProperty("_SelfLitIntensity", properties);
        _SelfLitColor = ShaderGUI.FindProperty("_SelfLitColor", properties);
        _SelfLitPower = ShaderGUI.FindProperty("_SelfLitPower", properties);
        _TEXMCOLINT = ShaderGUI.FindProperty("_TEXMCOLINT", properties);
        _SelfLitHighContrast = ShaderGUI.FindProperty("_SelfLitHighContrast", properties);
        _MaskSelfLit = ShaderGUI.FindProperty("_MaskSelfLit", properties);

        _GlossIntensity = ShaderGUI.FindProperty("_GlossIntensity", properties);
        _Glossiness = ShaderGUI.FindProperty("_Glossiness", properties);
        _GlossSoftness = ShaderGUI.FindProperty("_GlossSoftness", properties);
        _GlossColor = ShaderGUI.FindProperty("_GlossColor", properties);
        _GlossColorPower = ShaderGUI.FindProperty("_GlossColorPower", properties);
        _MaskGloss = ShaderGUI.FindProperty("_MaskGloss", properties);

        _GlossTexture = ShaderGUI.FindProperty("_GlossTexture", properties);
        _GlossTextureSoftness = ShaderGUI.FindProperty("_GlossTextureSoftness", properties);
        _PSGLOTEX = ShaderGUI.FindProperty("_PSGLOTEX", properties);
        _GlossTextureRotate = ShaderGUI.FindProperty("_GlossTextureRotate", properties);
        _GlossTextureFollowObjectRotation = ShaderGUI.FindProperty("_GlossTextureFollowObjectRotation", properties);
        _GlossTextureFollowLight = ShaderGUI.FindProperty("_GlossTextureFollowLight", properties);

        _OverallShadowColor = ShaderGUI.FindProperty("_OverallShadowColor", properties);
        _OverallShadowColorPower = ShaderGUI.FindProperty("_OverallShadowColorPower", properties);
        _SelfShadowShadowTAtViewDirection = ShaderGUI.FindProperty("_SelfShadowShadowTAtViewDirection", properties);

        _HighlightColor = ShaderGUI.FindProperty("_HighlightColor", properties);
        _HighlightColorPower = ShaderGUI.FindProperty("_HighlightColorPower", properties);

        _SelfShadowThreshold = ShaderGUI.FindProperty("_SelfShadowThreshold", properties);
        _VertexColorGreenControlSelfShadowThreshold = ShaderGUI.FindProperty("_VertexColorGreenControlSelfShadowThreshold", properties);
        _SelfShadowHardness = ShaderGUI.FindProperty("_SelfShadowHardness", properties);

        _SelfShadowRealtimeShadowIntensity = ShaderGUI.FindProperty("_SelfShadowRealtimeShadowIntensity", properties);

        _SelfShadowRealTimeShadowColor = ShaderGUI.FindProperty("_SelfShadowRealTimeShadowColor", properties);
        _SelfShadowRealTimeShadowColorPower = ShaderGUI.FindProperty("_SelfShadowRealTimeShadowColorPower", properties);

        _SelfShadowAffectedByLightShadowStrength = ShaderGUI.FindProperty("_SelfShadowAffectedByLightShadowStrength", properties);

        _SmoothObjectNormal = ShaderGUI.FindProperty("_SmoothObjectNormal", properties);
        _VertexColorRedControlSmoothObjectNormal = ShaderGUI.FindProperty("_VertexColorRedControlSmoothObjectNormal", properties);
        _XYZPosition = ShaderGUI.FindProperty("_XYZPosition", properties);
        _XYZHardness = ShaderGUI.FindProperty("_XYZHardness", properties);
        _ShowNormal = ShaderGUI.FindProperty("_ShowNormal", properties);

        _ShadowColorTexture = ShaderGUI.FindProperty("_ShadowColorTexture", properties);
        _ShadowColorTexturePower = ShaderGUI.FindProperty("_ShadowColorTexturePower", properties);

        _ShadowTIntensity = ShaderGUI.FindProperty("_ShadowTIntensity", properties);
        _ShadowT = ShaderGUI.FindProperty("_ShadowT", properties);
        _ShadowTLightThreshold = ShaderGUI.FindProperty("_ShadowTLightThreshold", properties);
        _ShadowTShadowThreshold = ShaderGUI.FindProperty("_ShadowTShadowThreshold", properties);
        _ShadowTColor = ShaderGUI.FindProperty("_ShadowTColor", properties);
        _ShadowTColorPower = ShaderGUI.FindProperty("_ShadowTColorPower", properties);
        _ShadowTHardness = ShaderGUI.FindProperty("_ShadowTHardness", properties);
        _STIL = ShaderGUI.FindProperty("_STIL", properties);
        _N_F_STIS = ShaderGUI.FindProperty("_N_F_STIS", properties);
        _N_F_STIAL = ShaderGUI.FindProperty("_N_F_STIAL", properties);
        _ShowInAmbientLightShadowIntensity = ShaderGUI.FindProperty("_ShowInAmbientLightShadowIntensity", properties);
        _ShowInAmbientLightShadowThreshold = ShaderGUI.FindProperty("_ShowInAmbientLightShadowThreshold", properties);

        _LightFalloffAffectShadowT = ShaderGUI.FindProperty("_LightFalloffAffectShadowT", properties);

        _PTexture = ShaderGUI.FindProperty("_PTexture", properties);
        _PTCol = ShaderGUI.FindProperty("_PTCol", properties);
        _PTexturePower = ShaderGUI.FindProperty("_PTexturePower", properties);

        _EnvironmentalLightingIntensity = ShaderGUI.FindProperty("_EnvironmentalLightingIntensity", properties);
        _RELG = ShaderGUI.FindProperty("_RELG", properties);

        _GIFlatShade = ShaderGUI.FindProperty("_GIFlatShade", properties);
        _GIShadeThreshold = ShaderGUI.FindProperty("_GIShadeThreshold", properties);
        _LightAffectShadow = ShaderGUI.FindProperty("_LightAffectShadow", properties);
        _LightIntensity = ShaderGUI.FindProperty("_LightIntensity", properties);

        _N_F_PAL = ShaderGUI.FindProperty("_N_F_PAL", properties);
        _N_F_AL = ShaderGUI.FindProperty("_N_F_AL", properties);

        _DirectionalLightIntensity = ShaderGUI.FindProperty("_DirectionalLightIntensity", properties);
        _PointSpotlightIntensity = ShaderGUI.FindProperty("_PointSpotlightIntensity", properties);
        _ALIntensity = ShaderGUI.FindProperty("_ALIntensity", properties);
        _N_F_ALSL = ShaderGUI.FindProperty("_N_F_ALSL", properties);
        _ALTuFo = ShaderGUI.FindProperty("_ALTuFo", properties);
        _LightFalloffSoftness = ShaderGUI.FindProperty("_LightFalloffSoftness", properties);

        _ReduSha = ShaderGUI.FindProperty("_ReduSha", properties);
        _ShadowHardness = ShaderGUI.FindProperty("_ShadowHardness", properties);

        _CustomLightDirectionIntensity = ShaderGUI.FindProperty("_CustomLightDirectionIntensity", properties);
        _CustomLightDirectionFollowObjectRotation = ShaderGUI.FindProperty("_CustomLightDirectionFollowObjectRotation", properties);
        _CustomLightDirection = ShaderGUI.FindProperty("_CustomLightDirection", properties);

        _ReflectionIntensity = ShaderGUI.FindProperty("_ReflectionIntensity", properties);
        _ReflectionRoughtness = ShaderGUI.FindProperty("_ReflectionRoughtness", properties);
        _RefMetallic = ShaderGUI.FindProperty("_RefMetallic", properties);
        _MaskReflection = ShaderGUI.FindProperty("_MaskReflection", properties);
        _FReflection = ShaderGUI.FindProperty("_FReflection", properties);

        _RimLigInt = ShaderGUI.FindProperty("_RimLigInt", properties);
        _RimLightUnfill = ShaderGUI.FindProperty("_RimLightUnfill", properties);
        _RimLightColor = ShaderGUI.FindProperty("_RimLightColor", properties);
        _RimLightColorPower = ShaderGUI.FindProperty("_RimLightColorPower", properties);
        _RimLightSoftness = ShaderGUI.FindProperty("_RimLightSoftness", properties);
        _RimLightInLight = ShaderGUI.FindProperty("_RimLightInLight", properties);
        _LightAffectRimLightColor = ShaderGUI.FindProperty("_LightAffectRimLightColor", properties);

        //_TessellationSmoothness = ShaderGUI.FindProperty("_TessellationSmoothness", properties);
        //_TessellationTransition = ShaderGUI.FindProperty("_TessellationTransition", properties);
        //_TessellationNear = ShaderGUI.FindProperty("_TessellationNear", properties);
        //_TessellationFar = ShaderGUI.FindProperty("_TessellationFar", properties);
 
        _RefVal = ShaderGUI.FindProperty("_RefVal", properties);
        _Oper = ShaderGUI.FindProperty("_Oper", properties);
        _Compa = ShaderGUI.FindProperty("_Compa", properties);

        _N_F_MC = ShaderGUI.FindProperty("_N_F_MC", properties);
        _N_F_NM = ShaderGUI.FindProperty("_N_F_NM", properties);
        _N_F_CO = ShaderGUI.FindProperty("_N_F_CO", properties);
        _N_F_O = ShaderGUI.FindProperty("_N_F_O", properties);
        _N_F_CA = ShaderGUI.FindProperty("_N_F_CA", properties);
        _N_F_SL = ShaderGUI.FindProperty("_N_F_SL", properties);
        _N_F_GLO = ShaderGUI.FindProperty("_N_F_GLO", properties);
        _N_F_GLOT = ShaderGUI.FindProperty("_N_F_GLOT", properties);
        _N_F_SS = ShaderGUI.FindProperty("_N_F_SS", properties);
        _N_F_SON = ShaderGUI.FindProperty("_N_F_SON", properties);
        _N_F_SCT = ShaderGUI.FindProperty("_N_F_SCT", properties);
        _N_F_ST = ShaderGUI.FindProperty("_N_F_ST", properties);
        _N_F_PT = ShaderGUI.FindProperty("_N_F_PT", properties);
        _N_F_CLD = ShaderGUI.FindProperty("_N_F_CLD", properties);
        _N_F_R = ShaderGUI.FindProperty("_N_F_R", properties);
        _N_F_FR = ShaderGUI.FindProperty("_N_F_FR", properties);
        _N_F_RL = ShaderGUI.FindProperty("_N_F_RL", properties);

        _N_F_HDLS = ShaderGUI.FindProperty("_N_F_HDLS", properties);
        _N_F_HPSAS = ShaderGUI.FindProperty("_N_F_HPSAS", properties);
        _N_F_CS = ShaderGUI.FindProperty("_N_F_CS", properties);

        _N_F_DCS = ShaderGUI.FindProperty("_N_F_DCS", properties);

        _ZWrite = ShaderGUI.FindProperty("_ZWrite", properties);
        _ZTest = ShaderGUI.FindProperty("_ZTest", properties);

        _N_F_NLASOBF = ShaderGUI.FindProperty("_N_F_NLASOBF", properties);

        #endregion

        //UI

        #region UI

        //Header
        Rect r_header = EditorGUILayout.BeginVertical("HelpBox");
        EditorGUILayout.LabelField("RealToon", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("(" + shader_type + ") [Beta]", EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        if (ShowUI == true)
        {

            GUILayout.Space(20);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            //Light Blend

            #region Light Blend

            Rect r_lightblend = EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Light Blend Style: " + LightBlendString);
            EditorGUILayout.EndVertical();

            switch ((int)_UseTLB.floatValue)
            {
                case 0:
                    LightBlendString = "Anime/Cartoon";
                    break;
                case 1:
                    LightBlendString = "Traditional";
                    break;
                default:
                    break;
            }

            #endregion

            //Culling

            #region Culling

            Rect r_culling = EditorGUILayout.BeginVertical("HelpBox");
            materialEditor.ShaderProperty(_Culling, new GUIContent(_Culling.displayName, TOTIPS[0]));
            EditorGUILayout.EndVertical();

            #endregion

            GUILayout.Space(20);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            //Render Queue

            #region Transparent Mode

            Rect r_renderqueue = EditorGUILayout.BeginVertical("HelpBox");

            EditorGUI.BeginChangeCheck();

            materialEditor.ShaderProperty(_TRANSMODE, new GUIContent(_TRANSMODE.displayName, TOTIPS[128]));

            if (EditorGUI.EndChangeCheck())
            {
                foreach (Material m in materialEditor.targets)
                {
                    switch (_TRANSMODE.floatValue)
                    {
                        case 0:
                            m.renderQueue = 2000;
                            shader_type = "HDRP - Default";
                            break;
                        case 1:
                            m.renderQueue = 3000;
                            shader_type = "HDRP - Fade Transperancy";
                            break;
                        default:
                            break;
                    }
                }

            }

            EditorGUILayout.EndVertical();

            #endregion

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            GUILayout.Space(20);

            //Texture - Color

            #region Texture - Color

            Rect r_texturecolor = EditorGUILayout.BeginVertical("Button");

            ShowTextureColor = EditorGUILayout.Foldout(ShowTextureColor, "(Texture - Color)", true, EditorStyles.foldout);
         
            if (ShowTextureColor)
            {
                GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MainTex, new GUIContent(_MainTex.displayName, TOTIPS[1]));
                    materialEditor.ShaderProperty(_TexturePatternStyle, new GUIContent(_TexturePatternStyle.displayName, TOTIPS[2]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MainColor, new GUIContent(_MainColor.displayName, TOTIPS[3]));
                    materialEditor.ShaderProperty(_MVCOL, new GUIContent(_MVCOL.displayName, TOTIPS[4]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_MCIALO, new GUIContent(_MCIALO.displayName, TOTIPS[5]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_HighlightColor, new GUIContent(_HighlightColor.displayName, TOTIPS[6]));
                    materialEditor.ShaderProperty(_HighlightColorPower, new GUIContent(_HighlightColorPower.displayName, TOTIPS[7]));

                    GUILayout.Space(10);

            }

            EditorGUILayout.EndVertical();

            #endregion

            //MatCap

            #region MatCap

            if (_N_F_MC.floatValue == 1)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_matcap = EditorGUILayout.BeginVertical("Button");
                ShowMatCap = EditorGUILayout.Foldout(ShowMatCap, "(MatCap)", true, EditorStyles.foldout);

                if (ShowMatCap)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MCapIntensity, new GUIContent(_MCapIntensity.displayName, TOTIPS[8]));
                    materialEditor.ShaderProperty(_MCap, new GUIContent(_MCap.displayName, TOTIPS[9])); 

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_SPECMODE, new GUIContent(_SPECMODE.displayName, TOTIPS[10]));
                    EditorGUI.BeginDisabledGroup(_SPECMODE.floatValue == 0);
                    materialEditor.ShaderProperty(_SPECIN, new GUIContent(_SPECIN.displayName, TOTIPS[11]));
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MCapMask, new GUIContent(_MCapMask.displayName, TOTIPS[12]));

                }

                EditorGUILayout.EndVertical();
            }

            #endregion

            //Transperancy

            #region Transperancy

            if (_TRANSMODE.floatValue == 1)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                EditorGUI.BeginDisabledGroup(_N_F_CO.floatValue == 1);

                Rect r_transparency = EditorGUILayout.BeginVertical("Button");
                ShowTransparency = EditorGUILayout.Foldout(ShowTransparency, "(Transparency)", true, EditorStyles.foldout);

                if (ShowTransparency)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_Opacity, new GUIContent(_Opacity.displayName, TOTIPS[17]));
                    materialEditor.ShaderProperty(_TransparentThreshold, new GUIContent(_TransparentThreshold.displayName, TOTIPS[18]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MaskTransparency, new GUIContent(_MaskTransparency.displayName, TOTIPS[19]));
                }

                EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndVertical();
            }

            #endregion

            //Cutout

            #region Cutout

            if (_TRANSMODE.floatValue == 1)
            {
                if (_N_F_CO.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    EditorGUI.BeginDisabledGroup(_N_F_CO.floatValue == 0);

                    Rect r_cutout = EditorGUILayout.BeginVertical("Button");
                    ShowCutout = EditorGUILayout.Foldout(ShowCutout, "(Cutout)", true, EditorStyles.foldout);

                    if (ShowCutout)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_Cutout, new GUIContent(_Cutout.displayName, TOTIPS[13]));
                        materialEditor.ShaderProperty(_AlphaBasedCutout, new GUIContent(_AlphaBasedCutout.displayName, TOTIPS[14]));

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_UseSecondaryCutout, new GUIContent(_UseSecondaryCutout.displayName, TOTIPS[15]));
                        materialEditor.ShaderProperty(_SecondaryCutout, new GUIContent(_SecondaryCutout.displayName, TOTIPS[16]));
                    }

                    EditorGUILayout.EndVertical();

                    EditorGUI.EndDisabledGroup();
                }
            }

            #endregion

            //Normal Map

            #region Normal Map

            if (_N_F_NM.floatValue == 1)
            {

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_normalmap = EditorGUILayout.BeginVertical("Button");
                ShowNormalMap = EditorGUILayout.Foldout(ShowNormalMap, "(Normal Map)", true, EditorStyles.foldout);

                if (ShowNormalMap)
                {
                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_NormalMap, new GUIContent(_NormalMap.displayName, TOTIPS[20]));

                    EditorGUI.BeginDisabledGroup(_NormalMap.textureValue == null);
                    materialEditor.ShaderProperty(_NormalMapIntensity, new GUIContent(_NormalMapIntensity.displayName, TOTIPS[21]));
                    EditorGUI.EndDisabledGroup();
                }

                EditorGUILayout.EndVertical();

            }
            #endregion

            //Color Adjustment

            #region Color Adjustment

            if (_N_F_CA.floatValue == 1)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_cadjustment = EditorGUILayout.BeginVertical("Button");
                ShowColorAdjustment = EditorGUILayout.Foldout(ShowColorAdjustment, "Color Adjustment", true, EditorStyles.foldout);

                if (ShowColorAdjustment)
                {

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_Saturation, new GUIContent(_Saturation.displayName, TOTIPS[22]));

                }

                EditorGUILayout.EndVertical();

            }

            #endregion

            //Outline

            #region Outline

            if (_N_F_O.floatValue == 1)
            {

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_outline = EditorGUILayout.BeginVertical("Button");
                ShowOutline = EditorGUILayout.Foldout(ShowOutline, "(Outline)", true, EditorStyles.foldout);


                if (ShowOutline)
                {

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineWidth, new GUIContent(_OutlineWidth.displayName, TOTIPS[23]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineWidthControl, new GUIContent(_OutlineWidthControl.displayName, TOTIPS[24]));


                    EditorGUI.BeginDisabledGroup(_OutlineExtrudeMethod.floatValue == 1);

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_N_F_O_NM, new GUIContent(_N_F_O_NM.displayName, TOTIPS[130]));

                    EditorGUI.BeginDisabledGroup(_N_F_O_NM.floatValue == 0);

                    materialEditor.ShaderProperty(_ONormMap, new GUIContent(_ONormMap.displayName, TOTIPS[131]));
                    materialEditor.ShaderProperty(_ONormMapInt, new GUIContent(_ONormMapInt.displayName, TOTIPS[132]));

                    EditorGUI.EndDisabledGroup();

                    EditorGUI.EndDisabledGroup();


                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineExtrudeMethod, new GUIContent(_OutlineExtrudeMethod.displayName, TOTIPS[25]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineOffset, new GUIContent(_OutlineOffset.displayName, TOTIPS[26]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineZPostionInCamera, new GUIContent(_OutlineZPostionInCamera.displayName, TOTIPS[116]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_DoubleSidedOutline, new GUIContent(_DoubleSidedOutline.displayName, TOTIPS[27]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineColor,  new GUIContent(_OutlineColor.displayName, TOTIPS[28]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_MixMainTexToOutline, new GUIContent(_MixMainTexToOutline.displayName, TOTIPS[29]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_NoisyOutlineIntensity, new GUIContent(_NoisyOutlineIntensity.displayName, TOTIPS[30]));
                    materialEditor.ShaderProperty(_DynamicNoisyOutline, new GUIContent(_DynamicNoisyOutline.displayName, TOTIPS[31]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_LightAffectOutlineColor, new GUIContent(_LightAffectOutlineColor.displayName, TOTIPS[32]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_OutlineWidthAffectedByViewDistance, new GUIContent(_OutlineWidthAffectedByViewDistance.displayName, TOTIPS[33]));
                    EditorGUI.BeginDisabledGroup(_OutlineWidthAffectedByViewDistance.floatValue == 0);
                    materialEditor.ShaderProperty(_FarDistanceMaxWidth, new GUIContent(_FarDistanceMaxWidth.displayName, TOTIPS[34]));
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Space(10);
                    EditorGUI.BeginDisabledGroup(_TRANSMODE.floatValue == 0);
                    materialEditor.ShaderProperty(_TOAO, new GUIContent(_TOAO.displayName, TOTIPS[129]));
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_VertexColorBlueAffectOutlineWitdh, new GUIContent(_VertexColorBlueAffectOutlineWitdh.displayName, TOTIPS[35]));

                }

                EditorGUILayout.EndVertical();

            }

            #endregion

            //Self Lit

            #region SelfLit

            if (_N_F_SL.floatValue == 1)
            {

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_selflit = EditorGUILayout.BeginVertical("Button");
                ShowSelfLit = EditorGUILayout.Foldout(ShowSelfLit, "(Self Lit)", true, EditorStyles.foldout);

                if (ShowSelfLit)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_SelfLitIntensity, new GUIContent(_SelfLitIntensity.displayName, TOTIPS[36]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_SelfLitColor, new GUIContent(_SelfLitColor.displayName, TOTIPS[37]));
                    materialEditor.ShaderProperty(_SelfLitPower, new GUIContent(_SelfLitPower.displayName, TOTIPS[38]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_TEXMCOLINT, new GUIContent(_TEXMCOLINT.displayName, TOTIPS[39]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_SelfLitHighContrast, new GUIContent(_SelfLitHighContrast.displayName, TOTIPS[40]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MaskSelfLit, new GUIContent(_MaskSelfLit.displayName, TOTIPS[41]));

                }

                EditorGUILayout.EndVertical();

            }
            #endregion

            //Gloss

            #region Gloss

            if (_N_F_GLO.floatValue == 1)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_gloss = EditorGUILayout.BeginVertical("Button");
                ShowGloss = EditorGUILayout.Foldout(ShowGloss, "(Gloss)", true, EditorStyles.foldout);

                if (ShowGloss)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_GlossIntensity, new GUIContent(_GlossIntensity.displayName, TOTIPS[42]));
                    EditorGUI.BeginDisabledGroup(_N_F_GLOT.floatValue == 1);
                    materialEditor.ShaderProperty(_Glossiness, new GUIContent(_Glossiness.displayName, TOTIPS[43]));
                    materialEditor.ShaderProperty(_GlossSoftness, new GUIContent(_GlossSoftness.displayName, TOTIPS[44]));
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_GlossColor, new GUIContent(_GlossColor.displayName, TOTIPS[45]));
                    materialEditor.ShaderProperty(_GlossColorPower, new GUIContent(_GlossColorPower.displayName, TOTIPS[46]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MaskGloss, new GUIContent(_MaskGloss.displayName, TOTIPS[47]));

                    GUILayout.Space(10);

                    //Gloss Texture

                    #region Gloss Texture

                    if (_N_F_GLOT.floatValue == 1)
                    {

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        Rect r_glosstexture = EditorGUILayout.BeginVertical("Button");
                        GUILayout.Label("Gloss Texture", EditorStyles.boldLabel);
                        EditorGUILayout.EndVertical();

                        if (_N_F_GLOT.floatValue == 1)
                        {

                            GUILayout.Space(10);

                            materialEditor.ShaderProperty(_GlossTexture, new GUIContent(_GlossTexture.displayName, TOTIPS[48]));

                            GUILayout.Space(10);
                            EditorGUI.BeginDisabledGroup(_GlossTexture.textureValue == null);
                            materialEditor.ShaderProperty(_GlossTextureSoftness, new GUIContent(_GlossTextureSoftness.displayName, TOTIPS[49]));

                            GUILayout.Space(10);

                            materialEditor.ShaderProperty(_PSGLOTEX, new GUIContent(_PSGLOTEX.displayName, TOTIPS[50]));

                            GUILayout.Space(10);

                            EditorGUI.BeginDisabledGroup(_PSGLOTEX.floatValue == 1);
                            materialEditor.ShaderProperty(_GlossTextureRotate,new GUIContent(_GlossTextureRotate.displayName, TOTIPS[51]));
                            materialEditor.ShaderProperty(_GlossTextureFollowObjectRotation, new GUIContent(_GlossTextureFollowObjectRotation.displayName, TOTIPS[52]));
                            materialEditor.ShaderProperty(_GlossTextureFollowLight, new GUIContent(_GlossTextureFollowLight.displayName, TOTIPS[53]));
                            EditorGUI.EndDisabledGroup();

                            EditorGUI.EndDisabledGroup();

                        }

                    }
                    #endregion

                }

                EditorGUILayout.EndVertical();

            }

            #endregion

            //Shadow

            #region Shadow

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            Rect r_shadow = EditorGUILayout.BeginVertical("Button");
            ShowShadow = EditorGUILayout.Foldout(ShowShadow, "(Shadow)", true, EditorStyles.foldout);

            if (ShowShadow)
            {

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_OverallShadowColor, new GUIContent(_OverallShadowColor.displayName, TOTIPS[54]));
                materialEditor.ShaderProperty(_OverallShadowColorPower, new GUIContent(_OverallShadowColorPower.displayName, TOTIPS[55]));

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_SelfShadowShadowTAtViewDirection, new GUIContent(_SelfShadowShadowTAtViewDirection.displayName, TOTIPS[56]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_ReduSha, new GUIContent(_ReduSha.displayName, TOTIPS[57]));

                    if (_N_F_HDLS.floatValue == 0 || _N_F_HPSAS.floatValue == 0)
                    {
                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_ShadowHardness, new GUIContent(_ShadowHardness.displayName, TOTIPS[58]));
                    }

                        materialEditor.ShaderProperty(_SelfShadowRealtimeShadowIntensity, new GUIContent(_SelfShadowRealtimeShadowIntensity.displayName, TOTIPS[119]));

                //Self Shadow

                #region Self Shadow

                if (_N_F_SS.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_selfshadow = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("Self Shadow", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_SS.floatValue == 1)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_SelfShadowThreshold, new GUIContent(_SelfShadowThreshold.displayName, TOTIPS[59]));
                        materialEditor.ShaderProperty(_VertexColorGreenControlSelfShadowThreshold, new GUIContent(_VertexColorGreenControlSelfShadowThreshold.displayName, TOTIPS[60]));
                        materialEditor.ShaderProperty(_SelfShadowHardness, new GUIContent(_SelfShadowHardness.displayName, TOTIPS[61]));

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_SelfShadowRealTimeShadowColor, new GUIContent(_SelfShadowRealTimeShadowColor.displayName, TOTIPS[62]));
                        materialEditor.ShaderProperty(_SelfShadowRealTimeShadowColorPower, new GUIContent(_SelfShadowRealTimeShadowColorPower.displayName, TOTIPS[63]));
    
                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_SelfShadowAffectedByLightShadowStrength, new GUIContent(_SelfShadowAffectedByLightShadowStrength.displayName, TOTIPS[64]));

                    }

                }
                #endregion

                //Smooth Object Normal

                #region Smooth Object normal

                if (_N_F_SON.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    if (_N_F_SS.floatValue == 0)
                    {
                        _N_F_SON.floatValue = 0;
                        targetMat.DisableKeyword("F_SS_ON");
                        _ShowNormal.floatValue = 0;
                    }

                    Rect r_smoothobjectnormal = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("Smooth Object Normal", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_SON.floatValue == 1)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_SmoothObjectNormal, new GUIContent(_SmoothObjectNormal.displayName, TOTIPS[65]));
                        materialEditor.ShaderProperty(_VertexColorRedControlSmoothObjectNormal, new GUIContent(_VertexColorRedControlSmoothObjectNormal.displayName, TOTIPS[66]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_XYZPosition, new GUIContent(_XYZPosition.displayName, TOTIPS[67]));
                        materialEditor.ShaderProperty(_XYZHardness, new GUIContent(_XYZHardness.displayName, TOTIPS[68]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_ShowNormal, new GUIContent(_ShowNormal.displayName, TOTIPS[69]));

                    }

                }
                #endregion

                //Shadow Color Texture

                #region Shadow Color Texture

                if (_N_F_SCT.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_shadowcolortexture = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("Shadow Color Texture", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_SCT.floatValue == 1)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_ShadowColorTexture, new GUIContent(_ShadowColorTexture.displayName, TOTIPS[70]));
                        materialEditor.ShaderProperty(_ShadowColorTexturePower, new GUIContent(_ShadowColorTexturePower.displayName, TOTIPS[71]));
                    }

                }

                #endregion

                //ShadowT

                #region ShadowT

                if (_N_F_ST.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_shadowt = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("ShadowT", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_ST.floatValue == 1)
                    {
                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_ShadowTIntensity, new GUIContent(_ShadowTIntensity.displayName, TOTIPS[72]));
                        materialEditor.ShaderProperty(_ShadowT, new GUIContent(_ShadowT.displayName, TOTIPS[73]));
                        materialEditor.ShaderProperty(_ShadowTLightThreshold, new GUIContent(_ShadowTLightThreshold.displayName, TOTIPS[74]));
                        materialEditor.ShaderProperty(_ShadowTShadowThreshold, new GUIContent(_ShadowTShadowThreshold.displayName, TOTIPS[75]));
                        materialEditor.ShaderProperty(_ShadowTHardness, new GUIContent(_ShadowTHardness.displayName, TOTIPS[76]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_ShadowTColor, new GUIContent(_ShadowTColor.displayName, TOTIPS[120]));
                        materialEditor.ShaderProperty(_ShadowTColorPower, new GUIContent(_ShadowTColorPower.displayName, TOTIPS[121]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_STIL, new GUIContent(_STIL.displayName, TOTIPS[122]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_N_F_STIS, new GUIContent(_N_F_STIS.displayName, TOTIPS[77]));
                        materialEditor.ShaderProperty(_N_F_STIAL, new GUIContent(_N_F_STIAL.displayName, TOTIPS[78]));

                        EditorGUI.BeginDisabledGroup(_N_F_STIAL.floatValue == 0 && _N_F_STIS.floatValue == 0);
                        materialEditor.ShaderProperty(_ShowInAmbientLightShadowIntensity, new GUIContent(_ShowInAmbientLightShadowIntensity.displayName, TOTIPS[79]));
                        EditorGUI.EndDisabledGroup();

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_ShowInAmbientLightShadowThreshold, new GUIContent(_ShowInAmbientLightShadowThreshold.displayName, TOTIPS[80]));

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_LightFalloffAffectShadowT, new GUIContent(_LightFalloffAffectShadowT.displayName, TOTIPS[81]));

                    }

                }

                #endregion

                //Shadow PTexture

                #region PTexture

                if (_N_F_PT.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_ptexture = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("PTexture", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_PT.floatValue == 1)
                    {
                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_PTexture,new GUIContent(_PTexture.displayName, TOTIPS[82]));
                        materialEditor.ShaderProperty(_PTexturePower, new GUIContent(_PTexturePower.displayName, TOTIPS[83]));

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_PTCol, new GUIContent(_PTCol.displayName, TOTIPS[133]));
                    }

                }

                #endregion

            }

            EditorGUILayout.EndVertical();

            #endregion

            //Lighting

            #region Lighting

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            Rect r_lighting = EditorGUILayout.BeginVertical("Button");
            ShowLighting = EditorGUILayout.Foldout(ShowLighting, "(Lighting)", true, EditorStyles.foldout);

            if (ShowLighting)
            {

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_RELG, new GUIContent(_RELG.displayName, TOTIPS[84]));
                EditorGUI.BeginDisabledGroup(_RELG.floatValue == 0);
                materialEditor.ShaderProperty(_EnvironmentalLightingIntensity, new GUIContent(_EnvironmentalLightingIntensity.displayName, TOTIPS[85]));

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_GIFlatShade,new GUIContent(_GIFlatShade.displayName, TOTIPS[86]));
                materialEditor.ShaderProperty(_GIShadeThreshold, new GUIContent(_GIShadeThreshold.displayName, TOTIPS[87]));
                EditorGUI.EndDisabledGroup();

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_LightAffectShadow, new GUIContent(_LightAffectShadow.displayName, TOTIPS[88]));
                EditorGUI.BeginDisabledGroup(_LightAffectShadow.floatValue == 0);
                materialEditor.ShaderProperty(_LightIntensity, new GUIContent(_LightIntensity.displayName, TOTIPS[123]));
                EditorGUI.EndDisabledGroup();

                GUILayout.Space(10);
                materialEditor.ShaderProperty(_UseTLB, new GUIContent(_UseTLB.displayName, TOTIPS[125]));
                materialEditor.ShaderProperty(_N_F_PAL, new GUIContent(_N_F_PAL.displayName, TOTIPS[124]));
                materialEditor.ShaderProperty(_N_F_AL, new GUIContent(_N_F_AL.displayName, TOTIPS[126]));

                GUILayout.Space(10);
                materialEditor.ShaderProperty(_DirectionalLightIntensity, new GUIContent(_DirectionalLightIntensity.displayName, TOTIPS[89]));

                EditorGUI.BeginDisabledGroup(_N_F_PAL.floatValue == 0);
                materialEditor.ShaderProperty(_PointSpotlightIntensity, new GUIContent(_PointSpotlightIntensity.displayName, TOTIPS[90]));
                EditorGUI.EndDisabledGroup();

                GUILayout.Space(10);

                EditorGUI.BeginDisabledGroup(_N_F_AL.floatValue == 0);
                    materialEditor.ShaderProperty(_ALIntensity, new GUIContent(_ALIntensity.displayName, TOTIPS[134]));
                        EditorGUI.BeginDisabledGroup(_N_F_ALSL.floatValue == 1);
                            materialEditor.ShaderProperty(_ALTuFo, new GUIContent(_ALTuFo.displayName, TOTIPS[135]));
                        EditorGUI.EndDisabledGroup();
                    materialEditor.ShaderProperty(_N_F_ALSL, new GUIContent(_N_F_ALSL.displayName, TOTIPS[139]));
                EditorGUI.EndDisabledGroup();

                GUILayout.Space(10);
                EditorGUI.BeginDisabledGroup(_N_F_PAL.floatValue == 0 && _N_F_AL.floatValue == 0);
                materialEditor.ShaderProperty(_LightFalloffSoftness, new GUIContent(_LightFalloffSoftness.displayName, TOTIPS[91]));
                EditorGUI.EndDisabledGroup();


                //Custom Light Direction

                #region Custom Light Direction

                if (_N_F_CLD.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    EditorGUI.BeginDisabledGroup(_N_F_CLD.floatValue == 0);

                    Rect r_customlightdirection = EditorGUILayout.BeginVertical("Button");
                    GUILayout.Label("Custom Light Direction", EditorStyles.boldLabel);
                    EditorGUILayout.EndVertical();

                    if (_N_F_CLD.floatValue == 1)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_CustomLightDirectionIntensity, new GUIContent(_CustomLightDirectionIntensity.displayName, TOTIPS[92]));
                        materialEditor.ShaderProperty(_CustomLightDirection, new GUIContent(_CustomLightDirection.displayName, TOTIPS[93]));
                        materialEditor.ShaderProperty(_CustomLightDirectionFollowObjectRotation, new GUIContent(_CustomLightDirectionFollowObjectRotation.displayName, TOTIPS[94]));

                    }

                    EditorGUI.EndDisabledGroup();

                }

                #endregion
            }

            EditorGUILayout.EndVertical();

            #endregion

            //Reflection

            #region Reflection

            if (_N_F_R.floatValue == 1)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_reflection = EditorGUILayout.BeginVertical("Button");
                ShowReflection = EditorGUILayout.Foldout(ShowReflection, "(Reflection)", true, EditorStyles.foldout);

                if (ShowReflection)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_ReflectionIntensity, new GUIContent(_ReflectionIntensity.displayName, TOTIPS[95]));
                    materialEditor.ShaderProperty(_ReflectionRoughtness, new GUIContent(_ReflectionRoughtness.displayName, TOTIPS[96]));
                    materialEditor.ShaderProperty(_RefMetallic, new GUIContent(_RefMetallic.displayName, TOTIPS[97]));

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MaskReflection, new GUIContent(_MaskReflection.displayName, TOTIPS[98]));

                    GUILayout.Space(10);

                    //FReflection

                    #region FReflection

                    if (_N_F_FR.floatValue == 1)
                    {

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        EditorGUI.BeginDisabledGroup(_N_F_FR.floatValue == 0);

                        Rect r_freflection = EditorGUILayout.BeginVertical("Button");
                        GUILayout.Label("FReflection", EditorStyles.boldLabel);
                        EditorGUILayout.EndVertical();

                        materialEditor.ShaderProperty(_FReflection, new GUIContent(_FReflection.displayName, TOTIPS[99]));

                        EditorGUI.EndDisabledGroup();
                    }

                }

                #endregion

                EditorGUILayout.EndVertical();
            }

            #endregion

            // Rim Light

            #region Rim Light

            if (_N_F_RL.floatValue == 1)
            {

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_rimlight = EditorGUILayout.BeginVertical("Button");
                ShowRimLight = EditorGUILayout.Foldout(ShowRimLight, "(Rim Light)", true, EditorStyles.foldout);

                if (ShowRimLight)
                {

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_RimLigInt, new GUIContent(_RimLigInt.displayName, TOTIPS[136]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_RimLightUnfill, new GUIContent(_RimLightUnfill.displayName, TOTIPS[100]));
                    materialEditor.ShaderProperty(_RimLightSoftness, new GUIContent(_RimLightSoftness.displayName, TOTIPS[101]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_RimLightColor, new GUIContent(_RimLightColor.displayName, TOTIPS[103]));
                    materialEditor.ShaderProperty(_RimLightColorPower, new GUIContent(_RimLightColorPower.displayName, TOTIPS[104]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_LightAffectRimLightColor, new GUIContent(_LightAffectRimLightColor.displayName, TOTIPS[102]));

                    GUILayout.Space(10);
                    materialEditor.ShaderProperty(_RimLightInLight, new GUIContent(_RimLightInLight.displayName, TOTIPS[105]));

                }

                EditorGUILayout.EndVertical();

            }

            #endregion

            //Tessellation (Still in development)

            #region Tessellation

                //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                //Rect r_tessellation = EditorGUILayout.BeginVertical("Button");
                //ShowTessellation = EditorGUILayout.Foldout(ShowTessellation, "(Tessellation)", true, EditorStyles.foldout);

                //if (ShowTessellation)
               //{

                   // GUILayout.Space(10);

                    //materialEditor.ShaderProperty(_TessellationSmoothness, _TessellationSmoothness.displayName);
                    //materialEditor.ShaderProperty(_TessellationTransition, _TessellationTransition.displayName);
                    //materialEditor.ShaderProperty(_TessellationNear, _TessellationNear.displayName);
                   //materialEditor.ShaderProperty(_TessellationFar, _TessellationFar.displayName);

                //}

                //EditorGUILayout.EndVertical();

            #endregion 

            //See Through

            #region See Through

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            Rect r_seethrough = EditorGUILayout.BeginVertical("Button");
            ShowSeeThrough = EditorGUILayout.Foldout(ShowSeeThrough, "(See Through)", true, EditorStyles.foldout);

            if (ShowSeeThrough)
            {

                GUILayout.Space(10);

                materialEditor.ShaderProperty(_RefVal, new GUIContent(_RefVal.displayName, TOTIPS[106]));
                materialEditor.ShaderProperty(_Oper, new GUIContent(_Oper.displayName, TOTIPS[107]));
                materialEditor.ShaderProperty(_Compa, new GUIContent(_Compa.displayName, TOTIPS[108]));

            }

            EditorGUILayout.EndVertical();

            GUILayout.Space(20);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            #endregion

            //Disable/Enable Features

            #region Disable/Enable Features

            Rect r_disableenablefeature = EditorGUILayout.BeginVertical("Button");
            ShowDisableEnable = EditorGUILayout.Foldout(ShowDisableEnable, "(Disable/Enable Features)", true, EditorStyles.foldout);

            if (ShowDisableEnable)
            {

                Rect r_mc = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_MC, new GUIContent(_N_F_MC.displayName, TOTIPSEDF[0]));
                EditorGUILayout.EndVertical();

                Rect r_nm = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_NM, new GUIContent(_N_F_NM.displayName, TOTIPSEDF[1]));
                EditorGUILayout.EndVertical();

                Rect r_ou = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_O, new GUIContent(_N_F_O.displayName, TOTIPSEDF[2]));
                EditorGUILayout.EndVertical();

                EditorGUI.BeginDisabledGroup(_TRANSMODE.floatValue == 0);

                    Rect r_co = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_CO, new GUIContent(_N_F_CO.displayName, TOTIPSEDF[3]));
                    EditorGUILayout.EndVertical();

                EditorGUI.EndDisabledGroup();

                Rect r_ca = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_CA, new GUIContent(_N_F_CA.displayName, TOTIPSEDF[4]));
                EditorGUILayout.EndVertical();

                Rect r_sl = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_SL, new GUIContent(_N_F_SL.displayName, TOTIPSEDF[5]));
                EditorGUILayout.EndVertical();

                Rect r_o = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_GLO, new GUIContent(_N_F_GLO.displayName, TOTIPSEDF[6]));
                EditorGUILayout.EndVertical();

                EditorGUI.BeginDisabledGroup(_N_F_GLO.floatValue == 0);

                    Rect r_glot = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_GLOT, new GUIContent(_N_F_GLOT.displayName, TOTIPSEDF[7]));
                    EditorGUILayout.EndVertical();

                EditorGUI.EndDisabledGroup();

                Rect r_ss = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_SS, new GUIContent(_N_F_SS.displayName, TOTIPSEDF[8]));
                EditorGUILayout.EndVertical();

                Rect r_son = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_SON, new GUIContent(_N_F_SON.displayName, TOTIPSEDF[9]));
                EditorGUILayout.EndVertical();

                if (_N_F_SS.floatValue == 0)
                {
                    _N_F_SON.floatValue = 0;
                }

                Rect r_sct = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_SCT, new GUIContent(_N_F_SCT.displayName, TOTIPSEDF[10]));
                EditorGUILayout.EndVertical();

                Rect r_st = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_ST, new GUIContent(_N_F_ST.displayName, TOTIPSEDF[11]));
                EditorGUILayout.EndVertical();

                Rect r_pt = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_PT, new GUIContent(_N_F_PT.displayName, TOTIPSEDF[12]));
                EditorGUILayout.EndVertical();

                Rect r_cld = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_CLD, new GUIContent(_N_F_CLD.displayName, TOTIPSEDF[13]));
                EditorGUILayout.EndVertical();

                Rect r_r = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_R, new GUIContent(_N_F_R.displayName, TOTIPSEDF[14]));
                EditorGUILayout.EndVertical();

                EditorGUI.BeginDisabledGroup(_N_F_R.floatValue == 0);

                    Rect r_fr = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_FR, new GUIContent(_N_F_FR.displayName, TOTIPSEDF[15]));
                    EditorGUILayout.EndVertical();

                EditorGUI.EndDisabledGroup();

                Rect r_rl = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_N_F_RL, new GUIContent(_N_F_RL.displayName, TOTIPSEDF[16]));
                EditorGUILayout.EndVertical();

            }

            EditorGUILayout.EndVertical();

            #endregion

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            GUILayout.Space(10);

            materialEditor.ShaderProperty(_N_F_HDLS, new GUIContent(_N_F_HDLS.displayName, TOTIPS[110]));
            materialEditor.ShaderProperty(_N_F_HPSAS, new GUIContent(_N_F_HPSAS.displayName, TOTIPS[111]));
            materialEditor.ShaderProperty(_N_F_CS, new GUIContent(_N_F_CS.displayName, TOTIPS[137]));

            EditorGUI.BeginChangeCheck();

            materialEditor.ShaderProperty(_N_F_DCS, new GUIContent(_N_F_DCS.displayName, TOTIPS[112]));

            if (EditorGUI.EndChangeCheck())
            {
                int f_hcs_int = (int)_N_F_DCS.floatValue;
                foreach (Material m in materialEditor.targets)
                {
                    switch (f_hcs_int)
                    {
                        case 0:
                            m.SetShaderPassEnabled("ShadowCaster", true);
                            break;
                        case 1:
                            m.SetShaderPassEnabled("ShadowCaster", false);
                            break;
                        default:
                            break;
                    }
                }

            }

            materialEditor.ShaderProperty(_N_F_NLASOBF, new GUIContent(_N_F_NLASOBF.displayName, TOTIPS[109]));

            GUILayout.Space(10);

            materialEditor.ShaderProperty(_ZWrite, new GUIContent(_ZWrite.displayName, TOTIPS[113]));
            materialEditor.ShaderProperty(_ZTest, new GUIContent(_ZTest.displayName, TOTIPS[138]));

            GUILayout.Space(10);

            materialEditor.RenderQueueField();

            GUILayout.Space(10);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            GUILayout.Space(10);

            materialEditor.EnableInstancingField();

            aruskw = EditorGUILayout.Toggle(new GUIContent("Automatic Remove Unused Shader Keywords (Global)", TOTIPS[114]), aruskw);

            GUILayout.Space(10);

        }


        #region Automatic Remove UorOSKW
        if (aruskw == true)
        {
            foreach (Material m1 in materialEditor.targets)
            {
                for (int x = 0; x < m1.shaderKeywords.Length; x++)
                {
                    if (m1.shaderKeywords[x] != String.Empty)
                    {
                        for (int y = 0; y < Enum.GetValues(typeof(SFKW)).Length; y++)
                        {
                            if (m1.shaderKeywords[x] == Enum.GetValues(typeof(SFKW)).GetValue(y).ToString())
                            {
                                del_skw = false;
                                break;
                            }
                            else
                            {
                                del_skw = true;
                            }
                        }

                        if (del_skw == true)
                        {
                            m1.DisableKeyword(m1.shaderKeywords[x]);
                            del_skw = false;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        #endregion

        //Footbar
        #region Footbar

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        Rect r_footbar = EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Video Tutorials", "Toolbar"))
        {
            Application.OpenURL("www.youtube.com/playlist?list=PL0M1m9smMVPJ4qEkJnZObqJE5mU9uz6SY");
        }

        GUILayout.Space(5);

        if (GUILayout.Button("RealToon (User Guide).pdf", "Toolbar"))
        {
            Application.OpenURL(Application.dataPath + "/RealToon/RealToon (User Guide).pdf");
        }

        GUILayout.Space(5);

        if (GUILayout.Button(ShowUIString + " (Global)", "Toolbar"))
        {
            if (ShowUI == false)
            {
                ShowUI = true;
                ShowUIString = "Hide UI";
            }
            else
            {
                ShowUI = false;
                ShowUIString = "Show UI";
            }
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        #endregion

    }

}

#endif