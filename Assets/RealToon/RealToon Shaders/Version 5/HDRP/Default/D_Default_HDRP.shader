//RealToon V5.0.7 (HDRP) [Beta]
//MJQStudioWorks
//©2020

//Note:
//
//		Hi, Thank you again for using RealToon.
//		I spent alot of time/i work hard in making this RealToon HDRP Version, 
//		i started working on this last year 2019 and finally finish it this year 2020 april.
//
//		I hope you respect this work =).
//		Have fun using the shader and i hope to see what you can make.
//
//		This is still in beta means it is not final and there will be some changes will be made,
//		but you can use this now in making games, animations/film and arts without errors.
//		You can also use this together with unity's HDRP Shaders, means Realistic + Anime/Toon look.
//
//		( MJQ Studio Works [PH] )
//

Shader "HDRP/RealToon/Version 5/Default"
{
    Properties
    {

		[Enum(UnityEngine.Rendering.CullMode)] _Culling ("Culling", int) = 2

		[Toggle(N_F_TRANS_ON)] _TRANSMODE ("Transparent Mode", Float ) = 0

        _MainTex ("Texture", 2D) = "white" {}
        [ToggleUI] _TexturePatternStyle ("Texture Pattern Style", Float ) = 0
        [HDR] _MainColor ("Main Color", Color) = (0.2352941,0.2352941,0.2352941,1)

		[ToggleUI] _MVCOL ("Mix Vertex Color", Float ) = 0

		[ToggleUI] _MCIALO ("Main Color In Ambient Light Only", Float ) = 0

		[HDR] _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _HighlightColorPower ("Highlight Color Power", Float ) = 1

        [ToggleUI] _EnableTextureTransparent ("Enable Texture Transparent", Float ) = 0

		_MCapIntensity ("Intensity", Range(0, 1)) = 1
		_MCap ("MatCap", 2D) = "white" {}
		[ToggleUI] _SPECMODE ("Specular Mode", Float ) = 0
		_SPECIN ("Specular Power", Float ) = 1
		_MCapMask ("Mask MatCap", 2D) = "white" {}

        _Cutout ("Cutout", Range(0, 1)) = 0
		[ToggleUI] _AlphaBasedCutout ("Alpha Based Cutout", Float ) = 1
        [ToggleUI] _UseSecondaryCutout ("Use Secondary Cutout", Float ) = 0
        _SecondaryCutout ("Secondary Cutout", 2D) = "white" {}

		_Opacity ("Opacity", Range(0, 1)) = 1
		_TransparentThreshold ("Transparent Threshold", Float ) = 0
        _MaskTransparency ("Mask Transparency", 2D) = "black" {}

        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalMapIntensity ("Normal Map Intensity", Float ) = 1

        _Saturation ("Saturation", Range(0, 2)) = 1

        _OutlineWidth ("Width", Float ) = 0.5
        _OutlineWidthControl ("Width Control", 2D) = "white" {}

		[Toggle(N_F_O_NM_ON)] _N_F_O_NM ("Enhance outline using normal map", Float ) = 0

		_ONormMap ("Normal Map", 2D) = "bump" {}
        _ONormMapInt ("Normal Map Intensity", Float ) = 1

		[Enum(Normal,0,Origin,1)] _OutlineExtrudeMethod("Outline Extrude Method", int) = 0

		_OutlineOffset ("Outline Offset", Vector) = (0,0,0)
		_OutlineZPostionInCamera ("Outline Z Position In Camera", Float) = 0

		[Enum(Off,1,On,0)] _DoubleSidedOutline("Double Sided Outline", int) = 1

        [HDR] _OutlineColor ("Color", Color) = (0,0,0,1)

		[ToggleUI] _MixMainTexToOutline ("Mix Main Texture To Outline", Float ) = 0

        _NoisyOutlineIntensity ("Noisy Outline Intensity", Range(0, 1)) = 0
        [ToggleUI] _DynamicNoisyOutline ("Dynamic Noisy Outline", Float ) = 0
        [ToggleUI] _LightAffectOutlineColor ("Light Affect Outline Color", Float ) = 0

        [ToggleUI] _OutlineWidthAffectedByViewDistance ("Outline Width Affected By View Distance", Float ) = 0
		_FarDistanceMaxWidth ("Far Distance Max Width", Float ) = 10

		[ToggleUI] _TOAO ("Transparent Opacity Affect Outline", Float ) = 1
        [ToggleUI] _VertexColorBlueAffectOutlineWitdh ("Vertex Color Blue Affect Outline Witdh", Float ) = 0

        _SelfLitIntensity ("Intensity", Range(0, 1)) = 0
        [HDR] _SelfLitColor ("Color", Color) = (1,1,1,1)
        _SelfLitPower ("Power", Float ) = 2
		_TEXMCOLINT ("Texture and Main Color Intensity", Float ) = 1
        [ToggleUI] _SelfLitHighContrast ("High Contrast", Float ) = 1
        _MaskSelfLit ("Mask Self Lit", 2D) = "white" {}

        _GlossIntensity ("Gloss Intensity", Range(0, 1)) = 1
        _Glossiness ("Glossiness", Range(0, 1)) = 0.6
        _GlossSoftness ("Softness", Range(0, 1)) = 0
        [HDR] _GlossColor ("Color", Color) = (1,1,1,1)
        _GlossColorPower ("Color Power", Float ) = 1
        _MaskGloss ("Mask Gloss", 2D) = "white" {}

        _GlossTexture ("Gloss Texture", 2D) = "black" {}
        _GlossTextureSoftness ("Softness", Float ) = 0
		[ToggleUI] _PSGLOTEX ("Pattern Style", Float ) = 0
        _GlossTextureRotate ("Rotate", Float ) = 0
        [ToggleUI] _GlossTextureFollowObjectRotation ("Follow Object Rotation", Float ) = 0
        _GlossTextureFollowLight ("Follow Light", Range(0, 1)) = 0

        [HDR] _OverallShadowColor ("Overall Shadow Color", Color) = (0,0,0,1)
        _OverallShadowColorPower ("Overall Shadow Color Power", Float ) = 1

        [ToggleUI] _SelfShadowShadowTAtViewDirection ("Self Shadow & ShadowT At View Direction", Float ) = 0

		_ReduSha ("Reduce Shadow", float ) = 0
		_ShadowHardness ("Shadow Hardness", Range(0, 1)) = 0

        _SelfShadowRealtimeShadowIntensity ("Self Shadow & Realtime Shadow Intensity", Range(0, 1)) = 1
        _SelfShadowThreshold ("Threshold", Range(0, 1)) = 0.930
        [ToggleUI] _VertexColorGreenControlSelfShadowThreshold ("Vertex Color Green Control Self Shadow Threshold", Float ) = 0
        _SelfShadowHardness ("Hardness", Range(0, 1)) = 1
        [HDR] _SelfShadowRealTimeShadowColor ("Self Shadow & Real Time Shadow Color", Color) = (1,1,1,1)
        _SelfShadowRealTimeShadowColorPower ("Self Shadow & Real Time Shadow Color Power", Float ) = 1
		[ToggleUI] _SelfShadowAffectedByLightShadowStrength ("Self Shadow Affected By Light Shadow Strength", Float ) = 0

        _SmoothObjectNormal ("Smooth Object Normal", Range(0, 1)) = 0
        [ToggleUI] _VertexColorRedControlSmoothObjectNormal ("Vertex Color Red Control Smooth Object Normal", Float ) = 0
        _XYZPosition ("XYZ Position", Vector) = (0,0,0,0)
        _XYZHardness ("XYZ Hardness", Float ) = 14
        [ToggleUI] _ShowNormal ("Show Normal", Float ) = 0

        _ShadowColorTexture ("Shadow Color Texture", 2D) = "white" {}
        _ShadowColorTexturePower ("Power", Float ) = 0

        _ShadowTIntensity ("ShadowT Intensity", Range(0, 1)) = 1
        _ShadowT ("ShadowT", 2D) = "white" {}
        _ShadowTLightThreshold ("Light Threshold", Float ) = 50
        _ShadowTShadowThreshold ("Shadow Threshold", Float ) = 0
		_ShadowTHardness ("Hardness", Range(0, 1)) = 1
        [HDR] _ShadowTColor ("Color", Color) = (1,1,1,1)
        _ShadowTColorPower ("Color Power", Float ) = 1

		[ToggleUI] _STIL ("Ignore Light", Float ) = 0

		[Toggle(N_F_STIS_ON)] _N_F_STIS ("Show In Shadow", Float ) = 0

		[Toggle(N_F_STIAL_ON )] _N_F_STIAL ("Show In Ambient Light", Float ) = 0
        _ShowInAmbientLightShadowIntensity ("Show In Ambient Light & Shadow Intensity", Range(0, 1)) = 1
        _ShowInAmbientLightShadowThreshold ("Show In Ambient Light & Shadow Threshold", Float ) = 0.4

        [ToggleUI] _LightFalloffAffectShadowT ("Light Falloff Affect ShadowT", Float ) = 0

        _PTexture ("PTexture", 2D) = "white" {}
		_PTCol ("Color", Color) = (0,0,0,1)
        _PTexturePower ("Power", Float ) = 1

		[Toggle(N_F_RELGI_ON)] _RELG ("Receive Environmental Lighting and GI", Float ) = 1
        _EnvironmentalLightingIntensity ("Environmental Lighting Intensity", Float ) = 1

        [ToggleUI] _GIFlatShade ("GI Flat Shade", Float ) = 0
        _GIShadeThreshold ("GI Shade Threshold", Range(0, 1)) = 0

        [ToggleUI] _LightAffectShadow ("Light Affect Shadow", Float ) = 0
        _LightIntensity ("Light Intensity", Float ) = 1

		[Toggle(N_F_USETLB_ON)] _UseTLB ("Use Traditional Light Blend", Float ) = 0 
		[Toggle(N_F_PAL_ON)] _N_F_PAL ("Enable Punctual Lights", Float ) = 1
		[Toggle(N_F_AL_ON)] _N_F_AL ("Enable Area Light [Beta]", Float ) = 0

		_DirectionalLightIntensity ("Directional Light Intensity", Float ) = 0
		_PointSpotlightIntensity ("Point and Spot Light Intensity", Float ) = 0
		
		_ALIntensity ("Area Light Intensity", Float ) = 0
		[Toggle(N_F_ALSL_ON)] _N_F_ALSL ("Area Light Smooth Look", Float ) = 0
		_ALTuFo ("Tube Light Falloff (Temp Option)", Float ) = 20

		_LightFalloffSoftness ("Light Falloff Softness", Range(0, 1)) = 1

        _CustomLightDirectionIntensity ("Intensity", Range(0, 1)) = 0
        [ToggleUI] _CustomLightDirectionFollowObjectRotation ("Follow Object Rotation", Float ) = 0
        _CustomLightDirection ("Custom Light Direction", Vector) = (0,0,10,0)

        _ReflectionIntensity ("Intensity", Range(0, 1)) = 0
        _ReflectionRoughtness ("Roughtness", Float ) = 0
		_RefMetallic ("Metallic", Range(0, 1) ) = 0

        _MaskReflection ("Mask Reflection", 2D) = "white" {}

        _FReflection ("FReflection", 2D) = "black" {}

		_RimLigInt ("Rim Light Intensity", Range(0, 1)) = 1
        _RimLightUnfill ("Unfill", Float ) = 1.5
        [HDR] _RimLightColor ("Color", Color) = (1,1,1,1)
        _RimLightColorPower ("Color Power", Float ) = 1
        _RimLightSoftness ("Softness", Range(0, 1)) = 1
        [ToggleUI] _RimLightInLight ("Rim Light In Light", Float ) = 1
        [ToggleUI] _LightAffectRimLightColor ("Light Affect Rim Light Color", Float ) = 0


		//Tessellation is still in development
		//_TessellationSmoothness ("Smoothness", Range(0, 1)) = 0.5
		//_TessellationTransition ("Tessellation Transition", Range(0, 1)) = 0.8
        //_TessellationNear ("Tessellation Near", Float ) = 1
        //_TessellationFar ("Tessellation Far", Float ) = 1
		//====================================


		_RefVal ("ID", int ) = 0
        [Enum(Blank,8,A,0,B,2)] _Oper("Set 1", int) = 0
        [Enum(Blank,8,None,4,A,6,B,7)] _Compa("Set 2", int) = 4

		[Toggle(N_F_MC_ON)] _N_F_MC ("MatCap", Float ) = 0
		[Toggle(N_F_NM_ON)] _N_F_NM ("Normal Map", Float ) = 0
		[Toggle(N_F_CO_ON)] _N_F_CO ("Cutout", Float ) = 0
		[Toggle(N_F_O_ON)] _N_F_O ("Outline", Float ) = 1
		[Toggle(N_F_CA_ON)] _N_F_CA ("Color Adjustment", Float ) = 0
		[Toggle(N_F_SL_ON)] _N_F_SL ("Self Lit", Float ) = 0
		[Toggle(N_F_GLO_ON)] _N_F_GLO ("Gloss", Float ) = 0
		[Toggle(N_F_GLOT_ON)] _N_F_GLOT ("Gloss Texture", Float ) = 0
		[Toggle(N_F_SS_ON)] _N_F_SS ("Self Shadow", Float ) = 1
		[Toggle(N_F_SON_ON)] _N_F_SON ("Smooth Object Normal", Float ) = 0
		[Toggle(N_F_SCT_ON)] _N_F_SCT ("Shadow Color Texture", Float ) = 0
		[Toggle(N_F_ST_ON)] _N_F_ST ("ShadowT", Float ) = 0
		[Toggle(N_F_PT_ON)] _N_F_PT ("PTexture", Float ) = 0
		[Toggle(N_F_CLD_ON)] _N_F_CLD ("Custom Light Direction", Float ) = 0
		[Toggle(N_F_R_ON)] _N_F_R ("Relfection", Float ) = 0
		[Toggle(N_F_FR_ON)] _N_F_FR ("FRelfection", Float ) = 0
		[Toggle(N_F_RL_ON)] _N_F_RL ("Rim Light", Float ) = 0

		[Enum(On,1,Off,0)] _ZWrite("ZWrite", int) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", int) = 4

		[Toggle(N_F_HDLS_ON)] _N_F_HDLS ("Hide Directional Light Shadow", Float ) = 0
		[Toggle(N_F_HPSAS_ON)] _N_F_HPSAS ("Hide Point, Spot & Area Light Shadows", Float ) = 0 

		[Toggle(N_F_CS_ON)] _N_F_CS ("Hide Contact Shadow", Float ) = 0 
		[Toggle(N_F_DCS_ON)] _N_F_DCS ("Disable Cast Shadow", Float ) = 0
		[Toggle(N_F_NLASOBF_ON)] _N_F_NLASOBF ("No Light and Shadow On BackFace", Float ) = 0


    }

	HLSLINCLUDE

	#pragma target 4.5

	#pragma multi_compile _ LOD_FADE_CROSSFADE

	#pragma shader_feature_local N_F_USETLB_ON
	#pragma shader_feature_local N_F_TRANS_ON

	//Tessellation is still in development
	//#pragma shader_feature_local N_F_TESSE_ON

	#pragma shader_feature_local N_F_O_NM_ON
	#pragma shader_feature_local N_F_O_ON
	#pragma shader_feature_local N_F_MC_ON
	#pragma shader_feature_local N_F_NM_ON
	#pragma shader_feature_local N_F_CO_ON
	#pragma shader_feature_local N_F_SL_ON
	#pragma shader_feature_local N_F_CA_ON
	#pragma shader_feature_local N_F_GLO_ON
	#pragma shader_feature_local N_F_GLOT_ON
	#pragma shader_feature_local N_F_SS_ON
	#pragma shader_feature_local N_F_SCT_ON
	#pragma shader_feature_local N_F_ST_ON
	#pragma shader_feature_local N_F_STIS_ON
	#pragma shader_feature_local N_F_STIAL_ON 
	#pragma shader_feature_local N_F_SON_ON
	#pragma shader_feature_local N_F_PT_ON
	#pragma shader_feature_local N_F_RELGI_ON
	#pragma shader_feature_local N_F_CLD_ON
	#pragma shader_feature_local N_F_R_ON
	#pragma shader_feature_local N_F_FR_ON
	#pragma shader_feature_local N_F_RL_ON
	#pragma shader_feature_local N_F_HDLS_ON
	#pragma shader_feature_local N_F_HPSAS_ON
	#pragma shader_feature_local N_F_PAL_ON
	#pragma shader_feature_local N_F_AL_ON
	#pragma shader_feature_local N_F_ALSL_ON
	#pragma shader_feature_local N_F_NLASOBF_ON
	#pragma shader_feature_local N_F_CS_ON

	ENDHLSL

    SubShader
    {

        Tags{"RenderPipeline"="HDRenderPipeline" "RenderType" = "HDLitShader"}

		Pass {
            Name "Outline"
            Tags { }

			Blend SrcAlpha OneMinusSrcAlpha

            Cull [_DoubleSidedOutline]
			ZTest LEqual

			Stencil {
				WriteMask 0
            	Ref[_RefVal]
            	Comp [_Compa]
            	Pass [_Oper]
            	Fail [_Oper]
            }

            HLSLPROGRAM

			#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragment

			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer

			#define SHADERPASS SHADERPASS_FORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			#define HAS_LIGHTLOOP
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/AtmosphericScattering/AtmosphericScattering.hlsl"

#if N_F_O_ON
			
			CBUFFER_START(UnityPerMaterial)

				uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
				uniform float _TexturePatternStyle;
				uniform float _EnableTextureTransparent;

				uniform float _OutlineWidth;
				uniform sampler2D _OutlineWidthControl; uniform float4 _OutlineWidthControl_ST;
				uniform float3 _OEM;
				uniform int _OutlineExtrudeMethod;
				uniform float3 _OutlineOffset;
				uniform float _OutlineZPostionInCamera;
				uniform float4 _OutlineColor;
				uniform float _MixMainTexToOutline;
				uniform float _NoisyOutlineIntensity;
				uniform float _DynamicNoisyOutline;
				uniform float _LightAffectOutlineColor;
				uniform float _OutlineWidthAffectedByViewDistance;
				uniform float _FarDistanceMaxWidth;
				uniform float _VertexColorBlueAffectOutlineWitdh;

				#if N_F_O_NM_ON
					uniform sampler2D _ONormMap; uniform float4 _ONormMap_ST;
					uniform float _ONormMapInt;
				#endif

			#if N_F_TRANS_ON
				#if N_F_CO_ON
					uniform float _Cutout;
					uniform float _AlphaBasedCutout;
					uniform float _UseSecondaryCutout;
					uniform sampler2D _SecondaryCutout; uniform float4 _SecondaryCutout_ST;
				#else
					uniform float _TOAO;
					uniform sampler2D _MaskTransparency; uniform float4 _MaskTransparency_ST;
					uniform float _Opacity;
					uniform float _TransparentThreshold;
				#endif
			#endif

			CBUFFER_END

			
			float4 EL_AT_SC(PositionInputs posInput, float3 V, float4 inputColor)
			{
				float4 result = inputColor;

				#ifdef N_F_TRANS_ON
		
					float3 volColor, volOpacity;
					EvaluateAtmosphericScattering(posInput, V, volColor, volOpacity);

						result.rgb = result.rgb * (1 - volOpacity) + volColor * result.a;
				
				#endif

					return result;
			}

			float4 ComputeScreenPos(float4 positionCS)
			{
				float4 o = positionCS * 0.5f;
				o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
				o.zw = positionCS.zw;
				return o;
			}

#endif
			struct Attributes
            {

#if N_F_O_ON

                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
				float4 vertexColor	: COLOR;
				float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID

#endif

            };

            struct Varyings
            {

#if N_F_O_ON

                float2 uv                       : TEXCOORD0;
				float4 projPos					: TEXCOORD1;
				float3 positionWS				: TEXCOORD2;
				float3 normalWS					: TEXCOORD3;
				float4 vertexColor				: COLOR;
                float4 positionCS               : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO

#endif

            };

			Varyings LitPassVertex(Attributes input)
            {

				Varyings output;
				ZERO_INITIALIZE(Varyings, output);

#if N_F_O_ON

				UNITY_SETUP_INSTANCE_ID (input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                output.uv = input.uv;
                output.vertexColor = input.vertexColor;

				float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0,0,0,1) );
				float RTD_OB_VP_CAL = distance(objPos.rgb,GetCurrentViewPosition());

				float RTD_OL_VCRAOW_OO = lerp( _OutlineWidth, (_OutlineWidth*(1.0 - output.vertexColor.b)), _VertexColorBlueAffectOutlineWitdh );
				float RTD_OL_OLWABVD_OO = lerp( RTD_OL_VCRAOW_OO, ( clamp(RTD_OL_VCRAOW_OO*RTD_OB_VP_CAL, RTD_OL_VCRAOW_OO, _FarDistanceMaxWidth) ), _OutlineWidthAffectedByViewDistance );
				float4 _OutlineWidthControl_var = tex2Dlod(_OutlineWidthControl,float4(TRANSFORM_TEX(output.uv, _OutlineWidthControl),0.0,0));

                float4 node_3726 = _Time;
                float node_8530_ang = node_3726.g;
                float node_8530_spd = 0.002;
                float node_8530_cos = cos(node_8530_spd*node_8530_ang);
                float node_8530_sin = sin(node_8530_spd*node_8530_ang);
                float2 node_8530_piv = float2(0.5,0.5);
                float2 node_8530 = (mul(output.uv-node_8530_piv,float2x2( node_8530_cos, -node_8530_sin, node_8530_sin, node_8530_cos))+node_8530_piv);

				float2 RTD_OL_DNOL_OO = lerp( output.uv, node_8530, _DynamicNoisyOutline );
				float2 node_8743 = RTD_OL_DNOL_OO;

                float2 node_1283_skew = node_8743 + 0.2127+node_8743.x*0.3713*node_8743.y;
                float2 node_1283_rnd = 4.789*sin(489.123*(node_1283_skew));
                float node_1283 = frac(node_1283_rnd.x*node_1283_rnd.y*(1+node_1283_skew.x));

				output.normalWS = TransformObjectToWorldNormal(input.normalOS);

				#if N_F_O_NM_ON

					float3 _ONormMap_var = UnpackNormal( tex2Dlod( _ONormMap, float4( TRANSFORM_TEX(output.uv, _ONormMap), 0.0, 0 ) ) ); 
					float3 _normloc = normalize(input.normalOS.xyz + ( lerp( float3(0,0,1), _ONormMap_var.rgb, _ONormMapInt ) * float3(1, 1, 0) ) );
				
				#else
					
					float3 _normloc = input.normalOS.xyz;

				#endif

				_OEM = lerp( _normloc , SafeNormalize(input.positionOS.xyz), _OutlineExtrudeMethod);

				float RTD_OL = ( RTD_OL_OLWABVD_OO*0.01 )*_OutlineWidthControl_var.r*lerp(1.0,node_1283,_NoisyOutlineIntensity);
                output.positionCS = TransformWorldToHClip(  TransformObjectToWorld( (input.positionOS.xyz + _OutlineOffset.xyz * 0.01) + _OEM * RTD_OL ) );

				#if defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)
					output.positionCS.z += _OutlineZPostionInCamera * 0.0005;
				#else
					output.positionCS.z -= _OutlineZPostionInCamera * 0.0005;
				#endif

                output.projPos = ComputeScreenPos (output.positionCS);
				output.positionWS = TransformObjectToWorld(input.positionOS.xyz);

#endif

                return output;

            }


				


            float4 LitPassFragment(Varyings input) : SV_Target
            {
#if N_F_O_ON

				UNITY_SETUP_INSTANCE_ID (input);

				PositionInputs posInput = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, input.positionCS.z, input.positionCS.w, input.positionWS);

				float3 VDir = GetWorldSpaceNormalizeViewDir(input.positionWS);

				float3 color = 1;
				float3 lightColor = 0;

				BuiltinData builtinData;
				builtinData.renderingLayers = _EnableLightLayers ? asuint(unity_RenderingLayer.x) : DEFAULT_LIGHT_LAYERS;

				for (uint i = 0; i < _DirectionalLightCount; ++i)
				{
					DirectionalLightData light = _DirectionalLightDatas[i];

					if(IsMatchingLightLayer(light.lightLayers, builtinData.renderingLayers))
					{
						lightColor += light.color * GetCurrentExposureMultiplier();
					}
				}

				float4 objPos = float4( TransformObjectToWorld( float3(0,0,0) ), 1 );
                float2 sceneUVs = (input.projPos.xy / input.projPos.w);

				float RTD_OB_VP_CAL = distance(objPos.rgb, GetCurrentViewPosition());
				float2 RTD_VD_Cal = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTD_OB_VP_CAL);

				float2 RTD_TC_TP_OO = lerp( input.uv, RTD_VD_Cal, _TexturePatternStyle );
				float2 node_2104 = RTD_TC_TP_OO;

				float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2104, _MainTex));

				#if N_F_TRANS_ON

					#if N_F_CO_ON

						float4 _SecondaryCutout_var = tex2D(_SecondaryCutout,TRANSFORM_TEX(input.uv, _SecondaryCutout));
						float RTD_CO_ON = lerp( (lerp((_MainTex_var.r*_SecondaryCutout_var.r),_SecondaryCutout_var.r,_UseSecondaryCutout)+lerp(0.5,(-1.0),_Cutout)), saturate(( (1.0 - _Cutout) > 0.5 ? (1.0-(1.0-2.0*((1.0 - _Cutout)-0.5))*(1.0-lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout))) : (2.0*(1.0 - _Cutout)*lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout)) )), _AlphaBasedCutout );

						clip(RTD_CO_ON - 0.5);

					#else

						float4 _MaskTransparency_var = tex2D(_MaskTransparency,TRANSFORM_TEX(input.uv, _MaskTransparency));

						float RTD_TRAN_MAS = (smoothstep(clamp(-20,1,_TransparentThreshold),1,_MainTex_var.a) *_MaskTransparency_var.r);
						float RTD_TRAN_OPA_Sli = lerp( RTD_TRAN_MAS, smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a)  ,_Opacity);

					#endif

				#endif

#if N_F_PAL_ON

if (LIGHTFEATUREFLAGS_PUNCTUAL)
{

				float4 distances = 0;

				for (uint p = 0; p < _PunctualLightCount; ++p)
				{
					LightData Plight = FetchLight(p);

					if(IsMatchingLightLayer(Plight.lightLayers, builtinData.renderingLayers))
					{
						float3 lightToSample = input.positionWS - Plight.positionRWS;

						distances.w = dot(lightToSample, Plight.forward);

						if (Plight.lightType == GPULIGHTTYPE_PROJECTOR_BOX)
						{
							distances.xyz = 1;
						}
						else
						{
							float3 unL     = -lightToSample;
							float  distSq  = dot(unL, unL);
							float  distRcp = rsqrt(distSq);
							float  dist    = distSq * distRcp;

							float3 PuncLigDir = unL * distRcp;
							distances.xyz = float3(dist, distSq, distRcp);

							ModifyDistancesForFillLighting(distances, Plight.size.x);
						}

						lightColor += (Plight.color * GetCurrentExposureMultiplier()) * PunctualLightAttenuation(distances, Plight.rangeAttenuationScale , Plight.rangeAttenuationBias, Plight.angleScale, Plight.angleOffset);

					}
				}

}
#endif


				//
				#ifdef UNITY_COLORSPACE_GAMMA
					_OutlineColor = float4(LinearToGamma22(_OutlineColor.rgb), _OutlineColor.a);
				#endif

				float node_6587 = 0.0;
				float3 RTD_OL_LAOC_OO = lerp( lerp(_OutlineColor.rgb,_OutlineColor.rgb * _MainTex_var.rgb, _MixMainTexToOutline) , lerp(float3(node_6587,node_6587,node_6587), lerp(_OutlineColor.rgb,_OutlineColor.rgb * _MainTex_var.rgb, _MixMainTexToOutline) ,lightColor.rgb), _LightAffectOutlineColor );
				//


				#if N_F_TRANS_ON

					float Trans_Val = 1;
					
					#ifndef N_F_CO_ON

						if(_TOAO == 1)
						{
							Trans_Val = RTD_TRAN_OPA_Sli;
						}
						else
						{
							clip(RTD_TRAN_OPA_Sli - 0.5);
							Trans_Val = 1;
						}
	
					#endif
					
				#else

					float Trans_Val = 1;

				#endif

				float4 finalRGBA = ApplyBlendMode(RTD_OL_LAOC_OO, Trans_Val);
	
				return EL_AT_SC(posInput, VDir, finalRGBA);

#else

				return 0;

#endif
			}
			ENDHLSL
		}

		Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            Cull [_Culling]

			ZClip On
            ZWrite On
            ZTest LEqual

            ColorMask 0

            HLSLPROGRAM

			#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer

			#define SHADERPASS SHADERPASS_SHADOWS

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
			uniform float _TexturePatternStyle;

			#if N_F_TRANS_ON
				#if N_F_CO_ON
					uniform float _Cutout;
					uniform float _AlphaBasedCutout;
					uniform float _UseSecondaryCutout;
					uniform sampler2D _SecondaryCutout; uniform float4 _SecondaryCutout_ST;
				#else
					uniform sampler2D _MaskTransparency; uniform float4 _MaskTransparency_ST;
					uniform float _Opacity;
					uniform float _TransparentThreshold;
				#endif
			#endif

			uniform float _ReduSha;

			float4 _ShadowBias;
			float3 _LightDirection;

			sampler3D _DitherMaskLOD;
			float dither;

			float4 ComputeScreenPos(float4 positionCS)
			{
				float4 o = positionCS * 0.5f;
				o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
				o.zw = positionCS.zw;
				return o;
			}

			struct Attributes
			{

				float4 positionOS   : POSITION;
				float3 normalOS     : NORMAL;
				float2 texcoord     : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID

			};

			struct Varyings
			{

				float2 uv           : TEXCOORD0;
				float4 projPos		: TEXCOORD1;
				float4 positionCS   : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO

			};

			float4 GetShadowPositionHClip(Attributes input)
			{

				float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
				float3 normalWS = TransformObjectToWorldDir(input.normalOS);

				float invNdotL = 1.0 - saturate(dot(_LightDirection, positionWS));
				float scale = invNdotL * _ShadowBias.y;

				positionWS = _LightDirection * _ShadowBias.xxx + positionWS;
				positionWS = normalWS * scale.xxx + positionWS;
				float4 positionCS = TransformWorldToHClip( positionWS );

			#if UNITY_REVERSED_Z
				positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE) + - _ReduSha * 0.01;
			#else
				positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE) + _ReduSha * 0.01;
			#endif

			return positionCS;

			}

			Varyings ShadowPassVertex(Attributes input)
			{

				Varyings output;
				ZERO_INITIALIZE(Varyings, output);

				UNITY_SETUP_INSTANCE_ID (input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.uv = input.texcoord;

				float4 objPos = mul (GetObjectToWorldMatrix(), float4(0,0,0,1) );

                output.positionCS = TransformWorldToHClip(TransformObjectToWorld(input.positionOS.xyz));
                output.projPos = ComputeScreenPos (output.positionCS);
				output.positionCS = GetShadowPositionHClip(input);

				return output;

			}

			float4 ShadowPassFragment(Varyings input) : SV_Target
			{

				UNITY_SETUP_INSTANCE_ID (input);

				float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0,0,0,1) );
                float2 sceneUVs = (input.projPos.xy / input.projPos.w);

				float2 RTD_OB_VP_CAL = distance(objPos.xyz, GetCurrentViewPosition());
				float2 RTD_VD_Cal = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTD_OB_VP_CAL);

                float2 _TexturePatternStyle_var = lerp( input.uv, RTD_VD_Cal, _TexturePatternStyle );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(_TexturePatternStyle_var, _MainTex));

				#if N_F_TRANS_ON

					#if N_F_CO_ON
						
						float4 _SecondaryCutout_var = tex2D(_SecondaryCutout,TRANSFORM_TEX(input.uv, _SecondaryCutout));
						float RTD_CO_ON = lerp( (lerp((_MainTex_var.r*_SecondaryCutout_var.r),_SecondaryCutout_var.r,_UseSecondaryCutout)+lerp(0.5,(-1.0),_Cutout)), saturate(( (1.0 - _Cutout) > 0.5 ? (1.0-(1.0-2.0*((1.0 - _Cutout)-0.5))*(1.0-lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout))) : (2.0*(1.0 - _Cutout)*lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout)) )), _AlphaBasedCutout );
						float RTD_CO = RTD_CO_ON;

						clip(RTD_CO - 0.5);
            
					#else

						float4 _MaskTransparency_var = tex2D(_MaskTransparency,TRANSFORM_TEX(input.uv, _MaskTransparency));

						float RTD_TRAN_MAS = (smoothstep(clamp(-20,1,_TransparentThreshold),1,_MainTex_var.a) *_MaskTransparency_var.r);
						float RTD_TRAN_OPA_Sli = lerp( RTD_TRAN_MAS, smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a)  ,_Opacity);

						dither = tex3D(_DitherMaskLOD, float3(input.positionCS.xy * 0.25, RTD_TRAN_OPA_Sli * 0.99)).a;
						clip(saturate(( 0.74 > 0.5 ? (1.0-(1.0-2.0*(0.74-0.5))*(1.0-dither)) : (2.0*0.74*dither) )) - 0.5);

					#endif

				#endif

				return 0;
			}

            ENDHLSL
        }

		Pass
        {
            Name "DepthOnly"
            Tags{ "LightMode" = "DepthOnly" }

            Cull [_Culling]

			Stencil
            {
                WriteMask 8
                Ref 0
                Comp Always
                Pass Replace
            }

            ZWrite On

            HLSLPROGRAM

            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer

            #pragma multi_compile _ WRITE_NORMAL_BUFFER
            #pragma multi_compile _ WRITE_MSAA_DEPTH

            #define SHADERPASS SHADERPASS_DEPTH_ONLY
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitProperties.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"

            #ifdef WRITE_NORMAL_BUFFER
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/ShaderPass/LitSharePass.hlsl"
            #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/ShaderPass/LitDepthPass.hlsl"
            #endif

            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"

            #pragma vertex Vert
            #pragma fragment Frag

            ENDHLSL
        }

		Pass
        {
            Name "MotionVectors"
            Tags{ "LightMode" = "MotionVectors" }

            Stencil
            {
                WriteMask 32
                Ref 32
                Comp Always
                Pass Replace
            }

            Cull[_Culling]

            ZWrite On

            HLSLPROGRAM

            #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer

            #pragma multi_compile _ WRITE_NORMAL_BUFFER
            #pragma multi_compile _ WRITE_MSAA_DEPTH

            #define SHADERPASS SHADERPASS_MOTION_VECTORS
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitProperties.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"

            #ifdef WRITE_NORMAL_BUFFER 
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/ShaderPass/LitSharePass.hlsl"
            #else
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/ShaderPass/LitMotionVectorPass.hlsl"
            #endif
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitData.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassMotionVectors.hlsl"

            #pragma vertex Vert
            #pragma fragment Frag

            ENDHLSL
        }

        Pass
        {

            Name "ForwardLit"
            Tags{"LightMode" = "ForwardOnly"}

			Blend SrcAlpha OneMinusSrcAlpha

            Cull [_Culling]
			ZTest [_ZTest]     
			ZWrite [_ZWrite]

			Stencil {
            	Ref[_RefVal]
				WriteMask 0
            	Comp [_Compa]
            	Pass [_Oper]
            	Fail [_Oper]
            }

            HLSLPROGRAM

			#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

			#pragma vertex LitPassVertex
            #pragma fragment LitPassFragment

			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer

			#define SHADERPASS SHADERPASS_FORWARD

			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile _ DEBUG_DISPLAY
            #pragma multi_compile USE_FPTL_LIGHTLIST USE_CLUSTERED_LIGHTLIST

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			#ifdef DEBUG_DISPLAY
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
			#endif

			#define HAS_LIGHTLOOP
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/VolumeRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinGIUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/AreaLighting.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/AtmosphericScattering/AtmosphericScattering.hlsl"
			
			//Tessellation is still in development
			//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"

			CBUFFER_START(UnityPerMaterial)

				uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
				uniform float4 _MainColor;
				uniform float _MVCOL;
				uniform float _MCIALO;
				uniform float _TexturePatternStyle;
				uniform float4 _HighlightColor;
				uniform float _HighlightColorPower;
				uniform float _EnableTextureTransparent;

				#if N_F_MC_ON
					uniform float _MCapIntensity;
					uniform sampler2D _MCap; uniform float4 _MCap_ST;
					uniform float _SPECMODE;
					uniform float _SPECIN;
					uniform sampler2D _MCapMask; uniform float4 _MCapMask_ST;
				#else
					uniform float _SPECMODE;
				#endif

				#if N_F_TRANS_ON
					#if N_F_CO_ON
						uniform float _Cutout;
						uniform float _AlphaBasedCutout;
						uniform float _UseSecondaryCutout;
						uniform sampler2D _SecondaryCutout; uniform float4 _SecondaryCutout_ST;
					#else
						uniform sampler2D _MaskTransparency; uniform float4 _MaskTransparency_ST;
						uniform float _Opacity;
						uniform float _TransparentThreshold;
					#endif
				#endif

				#if N_F_NM_ON
					uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
					uniform float _NormalMapIntensity;
				#endif

				#if N_F_CA_ON
					uniform float _Saturation;
				#endif

				#if N_F_SL_ON
					uniform float _SelfLitIntensity;
					uniform float4 _SelfLitColor;
					uniform float _SelfLitPower;
					uniform float _TEXMCOLINT;
					uniform float _SelfLitHighContrast;
					uniform sampler2D _MaskSelfLit; uniform float4 _MaskSelfLit_ST;
				#endif

				#if N_F_GLO_ON
					uniform float _GlossIntensity;
					uniform float _Glossiness;
					uniform float _GlossSoftness;
					uniform float4 _GlossColor;
					uniform float _GlossColorPower;
					uniform sampler2D _MaskGloss; uniform float4 _MaskGloss_ST;
				#endif

				#if N_F_GLO_ON
					#if N_F_GLOT_ON
						uniform sampler2D _GlossTexture; uniform float4 _GlossTexture_ST;
						uniform float _GlossTextureSoftness;
						uniform float _PSGLOTEX;
						uniform float _GlossTextureRotate;
						uniform float _GlossTextureFollowObjectRotation;
						uniform float _GlossTextureFollowLight;
					#endif
				#endif

				uniform float4 _OverallShadowColor;
				uniform float _OverallShadowColorPower;

				uniform float _SelfShadowShadowTAtViewDirection;

				uniform float _ShadowHardness;
				uniform float _SelfShadowRealtimeShadowIntensity;

				#if N_F_SS_ON
					uniform float _SelfShadowThreshold;
					uniform float VertexColorGreenControlSelfShadowThreshold;
					uniform float _SelfShadowHardness;
					uniform float _SelfShadowAffectedByLightShadowStrength;
				#endif

				uniform float4 _SelfShadowRealTimeShadowColor;
				uniform float _SelfShadowRealTimeShadowColorPower;

				#if N_F_SON_ON
					uniform float _SmoothObjectNormal;
					uniform float _VertexColorRedControlSmoothObjectNormal;
					uniform float4 _XYZPosition;
					uniform float _XYZHardness;
					uniform float _ShowNormal;
				#endif

				#if N_F_SCT_ON
					uniform sampler2D _ShadowColorTexture; uniform float4 _ShadowColorTexture_ST;
					uniform float _ShadowColorTexturePower;
				#endif

				#if N_F_ST_ON
					uniform float _ShadowTIntensity;
					uniform sampler2D _ShadowT; uniform float4 _ShadowT_ST;
					uniform float _ShadowTLightThreshold;
					uniform float _ShadowTShadowThreshold;
					uniform float4 _ShadowTColor;
					uniform float _ShadowTColorPower;
					uniform float _ShadowTHardness;
					uniform float _STIL;
					uniform float _ShowInAmbientLightShadowIntensity;
					uniform float _ShowInAmbientLightShadowThreshold;
					uniform float _LightFalloffAffectShadowT;
				#endif

				#if N_F_PT_ON
					uniform sampler2D _PTexture; uniform float4 _PTexture_ST;
					uniform float4 _PTCol;
					uniform float _PTexturePower;
				#endif

				#if N_F_RELGI_ON
					uniform float _GIFlatShade;
					uniform float _GIShadeThreshold;
					uniform float _EnvironmentalLightingIntensity;
				#endif

				uniform float _LightAffectShadow;
				uniform float _LightIntensity;
				uniform float _DirectionalLightIntensity;
				uniform float _PointSpotlightIntensity;
				uniform float _ALIntensity;
				uniform float _ALTuFo;
				uniform float _LightFalloffSoftness;

				#if N_F_CLD_ON
					uniform float _CustomLightDirectionIntensity;
					uniform float4 _CustomLightDirection;
					uniform float _CustomLightDirectionFollowObjectRotation;
				#endif

				#if N_F_R_ON
					uniform float _ReflectionIntensity;
					uniform float _ReflectionRoughtness;
					uniform float _RefMetallic;
					uniform sampler2D _MaskReflection; uniform float4 _MaskReflection_ST;
				#endif

				#if N_F_R_ON
					#if N_F_FR_ON
						uniform sampler2D _FReflection; uniform float4 _FReflection_ST;
					#endif
				#endif

				#if N_F_RL_ON
					uniform float _RimLigInt;
					uniform float _RimLightUnfill;
					uniform float _RimLightSoftness;	
					uniform float _LightAffectRimLightColor;
					uniform float4 _RimLightColor;
					uniform float _RimLightColorPower;
					uniform float _RimLightInLight;
				#endif

				//Tessellation is still in development
				//uniform float _TessellationSmoothness;
				//uniform half _TessellationTransition;
				//uniform half _TessellationNear;
				//uniform half _TessellationFar;

			CBUFFER_END

			float3 AL_GI( float3 N )
			{
				real4 SHCoefficients[7];
				SHCoefficients[0] = unity_SHAr;
				SHCoefficients[1] = unity_SHAg;
				SHCoefficients[2] = unity_SHAb;
				SHCoefficients[3] = unity_SHBr;
				SHCoefficients[4] = unity_SHBg;
				SHCoefficients[5] = unity_SHBb;
				SHCoefficients[6] = unity_SHC;

				return max(float3(0, 0, 0), SampleSH9(SHCoefficients, N));
            }


			float RTD_LVLC_F( float3 Light_Color_f3 )
			{

					float4 node_3149_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 node_3149_p = lerp(float4(float4(Light_Color_f3.rgb,0.0).zy, node_3149_k.wz), float4(float4(Light_Color_f3.rgb,0.0).yz, node_3149_k.xy), step(float4(Light_Color_f3.rgb,0.0).z, float4(Light_Color_f3.rgb,0.0).y));
					float4 node_3149_q = lerp(float4(node_3149_p.xyw, float4(Light_Color_f3.rgb,0.0).x), float4(float4(Light_Color_f3.rgb,0.0).x, node_3149_p.yzx), step(node_3149_p.x, float4(Light_Color_f3.rgb,0.0).x));
					float node_3149_d = node_3149_q.x - min(node_3149_q.w, node_3149_q.y);
					float node_3149_e = 1.0e-10;
					float3 node_3149 = float3(abs(node_3149_q.z + (node_3149_q.w - node_3149_q.y) / (6.0 * node_3149_d + node_3149_e)), node_3149_d / (node_3149_q.x + node_3149_e), node_3149_q.x);

					return saturate(node_3149.b);

            }

			float4 EL_AT_SC(PositionInputs posInput, float3 V, float4 inputColor)
			{
				float4 result = inputColor;

				#ifdef N_F_TRANS_ON
		
					float3 volColor, volOpacity;
					EvaluateAtmosphericScattering(posInput, V, volColor, volOpacity);

						result.rgb = result.rgb * (1 - volOpacity) + volColor * result.a;
				
				#endif

					return result;
			}

			float4 ComputeScreenPos(float4 positionCS)
			{
				float4 o = positionCS * 0.5f;
				o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
				o.zw = positionCS.zw;
				return o;
			}

			//Temporary Solution
			real Po_Ir(real4x3 L)
			{
				#ifdef APPROXIMATE_POLY_LIGHT_AS_SPHERE_LIGHT

					L[0] = normalize(L[0]);
					L[1] = normalize(L[1]);
					L[2] = normalize(L[2]);
					L[3] = normalize(L[3]);

					real3 F  = ComputeEdgeFactor(L[0], L[1]);
						  F += ComputeEdgeFactor(L[1], L[2]);
						  F += ComputeEdgeFactor(L[2], L[3]);
						  F += ComputeEdgeFactor(L[3], L[0]);

					#if 1
						float l = length(F);
						return max(0.0, (l * l + F.z) / (l + 1  * (0.16 * 0.02) ) );
					#else
					#endif

				#endif
			}
			//==================

            struct Attributes
            {

                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
				float4 vertexColor	: COLOR;
				float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct Varyings
            {

                float2 uv                       : TEXCOORD0;
                float3 normalWS					: TEXCOORD1;
                float4 tangentWS                : TEXCOORD2;
                float3 bitangentWS              : TEXCOORD3;
				float3 positionWS				: TEXCOORD4;
				float4 projPos					: TEXCOORD5;
                float4 positionCS               : SV_POSITION;
				float4 vertexColor				: COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO

            };

            Varyings LitPassVertex(Attributes input)
            {

                Varyings output;
				ZERO_INITIALIZE(Varyings, output);

				UNITY_SETUP_INSTANCE_ID (input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.tangentWS = float4(TransformObjectToWorldDir(input.tangentOS.xyz), input.tangentOS.w);
                output.bitangentWS = cross(output.normalWS, output.tangentWS.xyz) * (input.tangentOS.w * GetOddNegativeScale());
				output.positionWS = TransformObjectToWorld(input.positionOS.xyz);

				output.uv = input.uv;
                output.vertexColor = input.vertexColor;
				
				output.positionCS = TransformWorldToHClip(output.positionWS);
				output.projPos = ComputeScreenPos (output.positionCS);

                return output;
            }

            void LitPassFragment(Varyings input, float facing : VFACE, out float4 outColor : SV_Target0)
            {

				UNITY_SETUP_INSTANCE_ID (input);

				#if N_F_NM_ON

					float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(input.uv, _NormalMap)));
					float3 normalLocal = lerp(float3(0,0,1),_NormalMap_var.rgb,_NormalMapIntensity);

				#else

					float3 normalLocal = float3(0,0,1);

				#endif

				input.positionCS.xy = _OffScreenRendering > 0 ? (input.positionCS.xy * _OffScreenDownsampleFactor) : input.positionCS.xy;
				uint2 tileIndex = uint2(input.positionCS.xy) / GetTileSize();
				PositionInputs posInput = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, input.positionCS.z, input.positionCS.w, input.positionWS.xyz, tileIndex);

				float isFrontFace = ( facing >= 0 ? 1 : 0 );
				float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0,0,0,1) );
				float2 sceneUVs = (input.projPos.xy / input.projPos.w);

				input.normalWS = SafeNormalize(input.normalWS);
                float3x3 tangentTransform = float3x3( input.tangentWS.xyz, input.bitangentWS, input.normalWS);
                float3 viewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
                float3 normalDirection = SafeNormalize(mul( normalLocal, tangentTransform ));
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );

				float2 RTD_OB_VP_CAL = distance(objPos.xyz, GetCurrentViewPosition());
				float2 RTD_VD_Cal = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*RTD_OB_VP_CAL);

				float2 RTD_TC_TP_OO = lerp( input.uv, RTD_VD_Cal, _TexturePatternStyle );
				float2 node_2104 = RTD_TC_TP_OO;

				float3 color = 0;
				float3 A_L_O = 0;
				float3 Ar_L_O = 0;

				//=========//
				//=========//

				LightLoopContext context;
				context.shadowContext  = InitShadowContext();
				context.shadowValue = 1;			
				context.sampleReflection = 0;

				InitContactShadow(posInput, context);

				BuiltinData builtinData;
				ZERO_INITIALIZE(BuiltinData,builtinData);
				
				#ifdef SHADOWS_SHADOWMASK
					float4 shaMask = SampleShadowMask(posInput.positionWS, input.uv.xy);
					builtinData.shadowMask0 = shaMask.x;
					builtinData.shadowMask1 = shaMask.y;
					builtinData.shadowMask2 = shaMask.z;
					builtinData.shadowMask3 = shaMask.w;
				#endif

				builtinData.renderingLayers = _EnableLightLayers ? asuint(unity_RenderingLayer.x) : DEFAULT_LIGHT_LAYERS;

				//=========//
				//=========//

				#if N_F_R_ON

					float3 EnvRef = (SampleSkyTexture(viewReflectDirection, _ReflectionRoughtness ,0) * GetCurrentExposureMultiplier()).rgb;

					if (LIGHTFEATUREFLAGS_ENV)
					{

						#if !defined(N_F_TRANS_ON)

							float4 ssrLighting = LOAD_TEXTURE2D_X(_SsrLightingTexture, posInput.positionSS + float2(_HighlightColorPower,_HighlightColorPower));
							InversePreExposeSsrLighting( ssrLighting );

							ApplyScreenSpaceReflectionWeight(ssrLighting);

							EnvRef += ssrLighting.rgb;

						#endif

						for (uint i = 0; i < _EnvLightCount; ++i)
						{
							EnvLightData ELD = _EnvLightDatas[i];
								
							EnvRef = (SampleEnv(context, ELD.envIndex, viewReflectDirection, _ReflectionRoughtness, ELD.rangeCompressionFactorCompensation) * ELD.multiplier * GetCurrentExposureMultiplier()).rgb;

						}

					}


				#endif

				//=========//
				//=========//

				float4 DirLigCol = 0;
				float3 DirLigDir = 0;
				float DirSha = 1.0;
				float DirLigDim = 0;

				uint i=0;

				if (LIGHTFEATUREFLAGS_DIRECTIONAL)
				{

					i=0;

					for (i = 0; i < _DirectionalLightCount; ++i)
					{

						DirectionalLightData light = _DirectionalLightDatas[i];

										
						if (IsMatchingLightLayer(light.lightLayers, builtinData.renderingLayers))
						{

							DirLigDir = -light.forward;
							DirLigDim = light.shadowDimmer;


						DirLigCol = float4(light.color * GetCurrentExposureMultiplier(), 1.0);

						#ifndef LIGHT_EVALUATION_NO_HEIGHT_FOG
						
							{
							
								float  cosZenithAngle = DirLigDir.y;
								float  fragmentHeight = posInput.positionWS.y;
								float3 oDepth = OpticalDepthHeightFog(_HeightFogBaseExtinction, _HeightFogBaseHeight,
																	  _HeightFogExponents, cosZenithAngle, fragmentHeight);
							
								float3 transm = TransmittanceFromOpticalDepth(oDepth);
								DirLigCol.rgb *= transm;
							}
						#endif

						#if SHADEROPTIONS_PRECOMPUTED_ATMOSPHERIC_ATTENUATION

						#else
						
							bool interactsWithSky = asint(light.distanceFromCamera) >= 0;

							if (interactsWithSky)
							{

								float3 X = GetAbsolutePositionWS(posInput.positionWS);
								float3 C = _PlanetCenterPosition;

								float r        = distance(X, C);
								float cosHoriz = ComputeCosineOfHorizonAngle(r);
								float cosTheta = dot(X - C, DirLigDir) * rcp(r); 

								if (cosTheta >= cosHoriz) 
								{
									float3 oDepth = ComputeAtmosphericOpticalDepth(r, cosTheta, true);
								
									float3 transm  = TransmittanceFromOpticalDepth(oDepth);
									float3 opacity = 1 - transm;
									DirLigCol.rgb *= 1 - (Desaturate(opacity, _AlphaSaturation) * _AlphaMultiplier);
								}
								else
								{
								
								   DirLigCol = 0;
								}
							}

						#endif

						#ifndef LIGHT_EVALUATION_NO_COOKIE
							if (light.cookieMode != COOKIEMODE_NONE)
							{
								float3 lightToSample = posInput.positionWS - light.positionRWS;
								float3 cookie = EvaluateCookie_Directional(context, light, lightToSample);

								DirLigCol.rgb *= cookie;
							}
						#endif

						#ifndef LIGHT_EVALUATION_NO_SHADOWS

							float shadowMask = 1.0;

							//Not Really Needed
							float NdotL = 1;

						#ifdef SHADOWS_SHADOWMASK

							DirSha = shadowMask = (light.shadowMaskSelector.x >= 0.0 && NdotL > 0.0) ? dot( BUILTIN_DATA_SHADOW_MASK , light.shadowMaskSelector) : 1.0;

						#endif

							if ((light.shadowIndex >= 0) && (light.shadowDimmer > 0))
							{
								context.shadowValue = GetDirectionalShadowAttenuation(context.shadowContext, posInput.positionSS, posInput.positionWS, float3(0,0,0) , light.shadowIndex, DirLigDir);
								DirSha = context.shadowValue.r;

							#ifdef SHADOWS_SHADOWMASK

								uint  payloadOffset;
								real  fade;
								int cascadeCount;
								int shadowSplitIndex = 0;

								shadowSplitIndex = EvalShadow_GetSplitIndex(context.shadowContext, light.shadowIndex, posInput.positionWS, fade, cascadeCount);

								fade = ((shadowSplitIndex + 1) == cascadeCount) ? fade : saturate(-shadowSplitIndex);

								DirSha = DirSha - fade + fade * shadowMask;

							
								DirSha = light.nonLightMappedOnly ? min(shadowMask, DirSha) : DirSha;
							#endif

								DirSha = lerp(shadowMask, DirSha, light.shadowDimmer);
							}

						#if N_F_CS_ON

							#if !defined(N_F_TRANS_ON) && !defined(LIGHT_EVALUATION_NO_CONTACT_SHADOWS)
								DirSha = min(DirSha, NdotL > 0.0 ? GetContactShadow(context, light.contactShadowMask, light.isRayTracedContactShadow) : 1.0); //float NdotL = dot(normalDirection, DirLigDir);
							#endif

						#endif

						#else 
							DirSha = 1.0;
						#endif

						}

					}

				}

				//=========//
				//=========//

				#if N_F_MC_ON 
            
					float2 MUV = (mul( UNITY_MATRIX_V, float4(normalDirection,0) ).xyz.rgb.rg*0.5+0.5); 
					float4 _MatCap_var = tex2D(_MCap,TRANSFORM_TEX(MUV, _MCap));
					float4 _MCapMask_var = tex2D(_MCapMask,TRANSFORM_TEX(input.uv, _MCapMask));
					float3 MCapOutP = lerp( lerp(1,0, _SPECMODE), lerp( lerp(1,0, _SPECMODE) ,_MatCap_var.rgb,_MCapIntensity) ,_MCapMask_var.rgb ); 

				#else
            
					float MCapOutP = 1;

				#endif

				float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2104, _MainTex));
				float3 _RTD_MVCOL = lerp(1, input.vertexColor.rgb, _MVCOL);


				//
				#ifdef UNITY_COLORSPACE_GAMMA
					_MainColor = float4(LinearToGamma22(_MainColor.rgb),_MainColor.a);
				#endif

				#if N_F_MC_ON 

					float3 SPECMode_Sel = lerp( lerp( (_MainColor.rgb * MCapOutP), ( _MainColor.rgb + (MCapOutP * _SPECIN) ), _SPECMODE) ,  lerp( ( MCapOutP), ( (MCapOutP * _SPECIN) ), _SPECMODE), _MCIALO );
					float3 RTD_TEX_COL = _MainTex_var.rgb * SPECMode_Sel * _RTD_MVCOL;

					
					float3 RTD_MCIALO_IL = RTD_TEX_COL;

				#else

					float3 RTD_TEX_COL = _MainTex_var.rgb * _MainColor.rgb * MCapOutP * _RTD_MVCOL;
					float3 RTD_MCIALO_IL = lerp( RTD_TEX_COL , _MainTex_var.rgb * MCapOutP * _RTD_MVCOL, _MCIALO);

				#endif
				//

				#if N_F_TRANS_ON

					#if N_F_CO_ON

						float4 _SecondaryCutout_var = tex2D(_SecondaryCutout,TRANSFORM_TEX(input.uv, _SecondaryCutout));
						float RTD_CO_ON = lerp( (lerp((_MainTex_var.r*_SecondaryCutout_var.r),_SecondaryCutout_var.r,_UseSecondaryCutout)+lerp(0.5,(-1.0),_Cutout)), saturate(( (1.0 - _Cutout) > 0.5 ? (1.0-(1.0-2.0*((1.0 - _Cutout)-0.5))*(1.0-lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout))) : (2.0*(1.0 - _Cutout)*lerp((_MainTex_var.a*_SecondaryCutout_var.r),_SecondaryCutout_var.a,_UseSecondaryCutout)) )), _AlphaBasedCutout );
						float RTD_CO = RTD_CO_ON;

						clip(RTD_CO - 0.5);
            
					#else

						float4 _MaskTransparency_var = tex2D(_MaskTransparency,TRANSFORM_TEX(input.uv, _MaskTransparency));

						float RTD_TRAN_MAS = (smoothstep(clamp(-20,1,_TransparentThreshold),1,_MainTex_var.a) *_MaskTransparency_var.r);
						float RTD_TRAN_OPA_Sli = lerp( RTD_TRAN_MAS, smoothstep(clamp(-20,1,_TransparentThreshold) , 1, _MainTex_var.a)  ,_Opacity);

					#endif

				#endif

				float3 lightDirection = DirLigDir;

				#if N_F_NLASOBF_ON
					float3 lightColor = lerp(0,DirLigCol.rgb,isFrontFace);
				#else
					float3 lightColor = DirLigCol.rgb;
				#endif
				
				float RTD_LVLC = RTD_LVLC_F(lightColor.rgb);

                float3 floatDirection = normalize(viewDirection + lightDirection);

				#if N_F_HDLS_ON

					float attenuation = 1; 
				#else
					float dlshmin = lerp(0,0.6,_ShadowHardness);
					float dlshmax = lerp(1,0.6,_ShadowHardness);

					#if N_F_NLASOBF_ON
						float FB_Check = lerp(1,DirSha,isFrontFace);
					#else
						float FB_Check = DirSha;
					#endif

					float attenuation = smoothstep(dlshmin,dlshmax,FB_Check);
				#endif

				#if N_F_SON_ON

					float3 node_76 =  mul(( input.positionWS.xyz - objPos.xyz ), (float3x3)GetObjectToWorldMatrix()); 

					float RTD_SON_VCBCSON_OO = lerp( _SmoothObjectNormal, (_SmoothObjectNormal*(1.0 - input.vertexColor.r)), _VertexColorRedControlSmoothObjectNormal );

					float3 norz = float3(((_XYZPosition.x * 0.1) + node_76.x ),((_XYZPosition.y * 0.1)  + node_76.y),((_XYZPosition.z * 0.1) + node_76.z));

					float3 RTD_SON_ON_OTHERS = lerp(normalDirection, TransformObjectToWorldDir( norz ) ,RTD_SON_VCBCSON_OO);

					float3 RTD_SON = RTD_SON_ON_OTHERS;

					float3 RTD_SNorm_OO = lerp( 1.0, _XYZHardness * RTD_SON_ON_OTHERS , _ShowNormal );
					float3 RTD_SON_CHE_1 = RTD_SNorm_OO;
            
				#else
            
					float3 RTD_SON = normalDirection;
					float3 RTD_SON_CHE_1 = 1;
            
				#endif

				#if N_F_RELGI_ON

					float3 RTD_GI_ST_Sli = (RTD_SON*_GIShadeThreshold);

					float node_2183 = 0;
					float node_8383 = 0.01;
					float3 RTD_GI_FS_OO = lerp( RTD_GI_ST_Sli, float3(smoothstep( float2(node_2183,node_2183), float2(node_8383,node_8383), (RTD_SON.rb*_GIShadeThreshold) ),0.0), _GIFlatShade );

				#else

					float3 RTD_GI_FS_OO = RTD_SON;

				#endif

				#if N_F_SCT_ON
            	
					float4 _ShadowColorTexture_var = tex2D(_ShadowColorTexture,TRANSFORM_TEX(input.uv, _ShadowColorTexture));
					float3 RTD_SCT_ON = lerp(_ShadowColorTexture_var.rgb,(_ShadowColorTexture_var.rgb*_ShadowColorTexture_var.rgb),_ShadowColorTexturePower);

					float3 RTD_SCT = RTD_SCT_ON * _MainColor.rgb;
            
				#else
            
					float3 RTD_SCT = RTD_MCIALO_IL;
            
				#endif

				#if N_F_PT_ON

					float2 node_953 = RTD_VD_Cal;
					float4 _PTexture_var = tex2D(_PTexture,TRANSFORM_TEX(node_953, _PTexture));
					float RTD_PT_ON = lerp((1.0 - _PTexturePower),1.0,_PTexture_var.r);
					float3 RTD_PT_COL = _PTCol.rgb;
            
					float RTD_PT = RTD_PT_ON;
            
				#else
            
					float RTD_PT = 1;
					float3 RTD_PT_COL = 1;
            
				#endif


				//
				#ifdef UNITY_COLORSPACE_GAMMA
					_OverallShadowColor = float4(LinearToGamma22(_OverallShadowColor.rgb), _OverallShadowColor.a);
				#endif

				float3 RTD_OSC = (_OverallShadowColor.rgb*_OverallShadowColorPower);
				//


				//
				#ifdef UNITY_COLORSPACE_GAMMA
					_SelfShadowRealTimeShadowColor = float4(LinearToGamma22(_SelfShadowRealTimeShadowColor.rgb), _SelfShadowRealTimeShadowColor.a);
				#endif

				float3 node_1860 = lerp( RTD_PT_COL, (_SelfShadowRealTimeShadowColor.rgb * _SelfShadowRealTimeShadowColorPower) * RTD_OSC * RTD_SCT, RTD_PT);
				//


                float3 node_6588 = (_LightIntensity * lightColor.rgb);
				float3 RTD_LAS = lerp(node_1860 * RTD_LVLC,( node_1860 * node_6588 ),_LightAffectShadow);


				//
				#ifdef UNITY_COLORSPACE_GAMMA
					_HighlightColor = float4(LinearToGamma22(_HighlightColor.rgb), _HighlightColor.a);
				#endif

				float3 RTD_HL = (_HighlightColor.rgb*_HighlightColorPower+_DirectionalLightIntensity);
				//


				float3 RTD_MCIALO = lerp(RTD_TEX_COL , lerp( lerp( (RTD_TEX_COL * _MainColor.rgb), (RTD_TEX_COL + _MainColor.rgb), _SPECMODE) , _MainTex_var.rgb * MCapOutP * _RTD_MVCOL * 0.7 , clamp((RTD_LVLC*1),0,1) ) , _MCIALO );

				#if N_F_GLO_ON

					#if N_F_GLOT_ON

						//#ifndef SHADER_API_MOBILE
							float node_5992_ang = _GlossTextureRotate;
							float node_5992_spd = 1.0;
							float node_5992_cos = cos(node_5992_spd*node_5992_ang);
							float node_5992_sin = sin(node_5992_spd*node_5992_ang);
							float2 node_5992_piv = float2(0.5,0.5);
						//#endif

							float3 RTD_GT_FL_Sli = lerp(viewDirection,floatDirection,_GlossTextureFollowLight);
							float3 node_2832 = reflect(-RTD_GT_FL_Sli,normalDirection);

							float3 RTD_GT_FOR_OO = lerp( node_2832, mul( GetWorldToObjectMatrix() , float4(node_2832,0) ).xyz, _GlossTextureFollowObjectRotation );
							float2 node_9280 = RTD_GT_FOR_OO.rg;

						//#ifndef SHADER_API_MOBILE
							float2 node_5992 = (mul(float2((-1*node_9280.r),node_9280.g)-node_5992_piv,float2x2( node_5992_cos, -node_5992_sin, node_5992_sin, node_5992_cos))+node_5992_piv);
							float2 node_8759 = (node_5992*0.5+0.5);
						//#endif

						//#ifdef SHADER_API_MOBILE
							//float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp( (float2((-1*node_2832.r),node_2832.g)*0.5+0.5) ,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
						//#else
            				float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp(node_8759,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
						//#endif

						float RTD_GT_ON = _GlossTexture_var.r;

						float RTD_GT = RTD_GT_ON;
            
					#else

						float RTD_GLO_MAIN_Sof_Sli = lerp(0.1,1.0,_GlossSoftness);
						float RTD_NDOTH = saturate(dot(floatDirection, normalDirection));
						float RTD_GLO_MAIN = smoothstep( 0.1, RTD_GLO_MAIN_Sof_Sli, pow(RTD_NDOTH,exp2(lerp(-2,15,_Glossiness))) );

						float RTD_GT = RTD_GLO_MAIN;
            
					#endif

					float node_1533 = 0.0;
					float RTD_GLO_I_Sli = lerp(node_1533, RTD_GT,_GlossIntensity);
					float4 _MaskGloss_var = tex2D(_MaskGloss,TRANSFORM_TEX(input.uv, _MaskGloss));


					//
					#ifdef UNITY_COLORSPACE_GAMMA
						_GlossColor = float4(LinearToGamma22(_GlossColor.rgb), _GlossColor.a);
					#endif

					float3 RTD_GLO_COL = (_GlossColor.rgb*_GlossColorPower); 
					//

					float RTD_GLO_MAS = lerp( 0, RTD_GLO_I_Sli ,_MaskGloss_var.r);


					float RTD_GLO = RTD_GLO_MAS;
            
				#else

					float3 RTD_GLO_COL = 1.0;

					float RTD_GLO = 0;
            
				#endif


				float3 RTD_GLO_OTHERS = RTD_GLO;

				#if N_F_RL_ON

					float node_4353 = 0.0;
					float node_3687 = 0.0;


					//
					#ifdef UNITY_COLORSPACE_GAMMA
						_RimLightColor = float4(LinearToGamma22(_RimLightColor.rgb), _RimLightColor.a);
					#endif

            		float3 RTD_RL_LARL_OO = lerp( _RimLightColor.rgb, lerp(float3(node_3687,node_3687,node_3687),_RimLightColor.rgb,lightColor.rgb), _LightAffectRimLightColor ) * _RimLightColorPower;
					//


					float RTD_RL_S_Sli = lerp(1.70,0.29,_RimLightSoftness);
					float RTD_RL_MAIN = lerp(node_4353, 1 ,smoothstep( 1.71, RTD_RL_S_Sli, pow(abs( 1.0-max(0,dot(normalDirection, viewDirection) ) ), (1.0 - _RimLightUnfill) ) ) );
					
					float RTD_RL_IL_OO = lerp( 0, RTD_RL_MAIN, _RimLigInt);

					float RTD_RL_CHE_1 = RTD_RL_IL_OO;
            
				#else
					
					float RTD_RL_LARL_OO = 1.0;

					float RTD_RL_CHE_1 = 0;
            
				#endif

				#if N_F_CLD_ON

            		float3 RTD_CLD_CLDFOR_OO = lerp( _CustomLightDirection.rgb, TransformObjectToWorldDir( _CustomLightDirection.xyz ), _CustomLightDirectionFollowObjectRotation );
					float3 RTD_CLD_CLDI_Sli = lerp(lightDirection,RTD_CLD_CLDFOR_OO,_CustomLightDirectionIntensity); 
					float3 RTD_CLD = RTD_CLD_CLDI_Sli;
            
				#else
            
					float3 RTD_CLD = lightDirection;
            
				#endif

				float3 RTD_ST_SS_AVD_OO = lerp( RTD_CLD, viewDirection, _SelfShadowShadowTAtViewDirection );
				float RTD_NDOTL = 0.5*dot(RTD_ST_SS_AVD_OO,RTD_SON)+0.5;

				#if N_F_ST_ON
				
					float node_8675 = 1.0;
					float node_949 = 1.0;
					float node_5738 = 1.0;
					float node_3187 = 0.22;

					float4 _ShadowT_var = tex2D(_ShadowT,TRANSFORM_TEX(input.uv, _ShadowT));


					//
					#ifdef UNITY_COLORSPACE_GAMMA
						_ShadowTColor = float4(LinearToGamma22(_ShadowTColor.rgb), _ShadowTColor.a);
					#endif

					float3 RTD_SHAT_COL = lerp( RTD_PT_COL, (_ShadowTColor.rgb*_ShadowTColorPower) * RTD_SCT * RTD_OSC, RTD_PT);
					//

					float3 RTD_ST_LAF = lerp( RTD_SHAT_COL * RTD_LVLC, (RTD_SHAT_COL * node_6588) , _LightAffectShadow );

					float RTD_ST_H_Sli = lerp(0.0,0.22,_ShadowTHardness);

					float RTD_ST_IS_ON = smoothstep( RTD_ST_H_Sli, node_3187, (_ShowInAmbientLightShadowThreshold*_ShadowT_var.rgb) ); 

					#if N_F_STIAL_ON

						float node_2346 = 1.0;
						float RTD_ST_ALI_Sli = lerp(float3(node_8675,node_8675,node_8675),RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            			float RTD_STIAL_ON = lerp(RTD_ST_ALI_Sli,float3(node_2346,node_2346,node_2346),clamp((RTD_LVLC*8.0),0,1));

            			float RTD_STIAL = RTD_STIAL_ON;
            
            		#else
            
            			float RTD_STIAL = 1.0;
            
            		#endif

					#if N_F_STIS_ON
            
            			float RTD_ST_IS = lerp(1,RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            
            		#else
            
            			float RTD_ST_IS = 1;
            
            		#endif

					float RTD_ST_LFAST_OO = lerp(lerp( RTD_NDOTL, (attenuation*RTD_NDOTL), _LightFalloffAffectShadowT ) , 1 , _STIL );
					float RTD_ST_In_Sli = lerp( 1 ,smoothstep( RTD_ST_H_Sli, node_3187, ((_ShadowT_var.r*(1.0 - _ShadowTShadowThreshold))*(RTD_ST_LFAST_OO *_ShadowTLightThreshold*0.01)) ),_ShadowTIntensity);
					float RTD_ST_ON = RTD_ST_In_Sli;

					float RTD_ST = RTD_ST_ON;
            
				#else
            
					float RTD_ST = 1;
					float RTD_SHAT_COL = 1;
					float RTD_ST_LAF = 1;
					float RTD_STIAL = 1;
					float RTD_ST_IS = 1;
            
				#endif

				float node_5573 = 1.0;

				#if N_F_SS_ON
 
					float RTD_SS_SSH_Sil = lerp(0.3,1.0,_SelfShadowHardness);
					float RTD_SS_SSTH_Sli = lerp(-1, 1, _SelfShadowThreshold);
					float RTD_SS_VCGCSSS_OO = lerp( RTD_SS_SSTH_Sli, (RTD_SS_SSTH_Sli*(1.0 - input.vertexColor.g)), VertexColorGreenControlSelfShadowThreshold );
					float RTD_SS_SST = smoothstep( RTD_SS_SSH_Sil, 1.0, (RTD_NDOTL * lerp(7, RTD_SS_VCGCSSS_OO ,RTD_SS_SSTH_Sli)) );
					float RTD_SS_SSABLSS_OO = lerp( RTD_SS_SST, lerp(RTD_SS_SST,node_5573, (1.0 - DirLigDim)  ), _SelfShadowAffectedByLightShadowStrength );
					float RTD_SS_ON = lerp(node_5573,(RTD_SS_SSABLSS_OO*attenuation),_SelfShadowRealtimeShadowIntensity);

					float RTD_SS = RTD_SS_ON;
            
				#else
            
					float RTD_SS_OFF = lerp(node_5573,attenuation,_SelfShadowRealtimeShadowIntensity);

					float RTD_SS = RTD_SS_OFF;
            
				#endif
				
				float3 RTD_R_OFF_OTHERS = lerp( lerp( RTD_ST_LAF, RTD_LAS, RTD_ST_IS) , lerp( RTD_ST_LAF, lerp( lerp( RTD_MCIALO_IL * RTD_HL , RTD_GLO_COL , RTD_GLO_OTHERS) , RTD_RL_LARL_OO , RTD_RL_CHE_1 ) * lightColor.rgb, RTD_ST) , RTD_SS ) ; //All mixed by lerp

				#if N_F_R_ON

					float3 RTD_FR_OFF_OTHERS = EnvRef;

					#if N_F_FR_ON
            
						float2 node_8431 = reflect(viewDirection,normalDirection).rg;
						float2 node_4207 = (float2(node_8431.r,(-1*node_8431.g))*0.5+0.5);
						float4 _FReflection_var = tex2Dlod(_FReflection,float4(TRANSFORM_TEX(node_4207, _FReflection),0.0,_ReflectionRoughtness));
						float3 RTD_FR_ON = _FReflection_var.rgb;

						float3 RTD_FR = RTD_FR_ON;
            
					#else
            
						float3 RTD_FR = RTD_FR_OFF_OTHERS;

					#endif

					float4 _MaskReflection_var = tex2D(_MaskReflection,TRANSFORM_TEX(input.uv, _MaskReflection));
					float3 RTD_R_MET_Sli = lerp(1,(9 * (RTD_TEX_COL - (9 * 0.005) ) ) , _RefMetallic);
					float3 RTD_R_MAS = lerp(RTD_R_OFF_OTHERS, (RTD_FR * RTD_R_MET_Sli) ,_MaskReflection_var.r);
					float3 RTD_R_ON = lerp(RTD_R_OFF_OTHERS, RTD_R_MAS ,_ReflectionIntensity);

					float3 RTD_R = RTD_R_ON;
            
				#else
            
					float3 RTD_R = RTD_R_OFF_OTHERS;
            
				#endif

				#if N_F_RELGI_ON

					float node_3622 = 0.0;
					float node_1766 = 1.0;

					#if N_F_R_ON

						float ref_int_val = _ReflectionIntensity;

					#else

						float ref_int_val = 1;

					#endif

					float3 RTD_SL_OFF_OTHERS = lerp( RTD_SHAT_COL , RTD_MCIALO , RTD_STIAL) * (AL_GI( lerp(float3(node_3622,node_3622,node_3622),float3(node_1766,node_1766,node_1766),RTD_GI_FS_OO) ) * ((_EnvironmentalLightingIntensity) * GetCurrentExposureMultiplier()))  ;

				#else

					float3 RTD_SL_OFF_OTHERS = 0;

				#endif

				#if N_F_SL_ON

            		float3 RTD_SL_HC_OO = lerp( 1.0, RTD_TEX_COL, _SelfLitHighContrast );
					float4 _MaskSelfLit_var = tex2D(_MaskSelfLit,TRANSFORM_TEX(input.uv, _MaskSelfLit));


					//
					#ifdef UNITY_COLORSPACE_GAMMA
						_SelfLitColor = float4(LinearToGamma22(_SelfLitColor.rgb), _SelfLitColor.a);
					#endif

					float3 RTD_SL_MAS = lerp(RTD_SL_OFF_OTHERS,((_SelfLitColor.rgb * RTD_TEX_COL * RTD_SL_HC_OO)*_SelfLitPower),_MaskSelfLit_var.r);
					//
					
					
					float3 RTD_SL_ON = lerp(RTD_SL_OFF_OTHERS,RTD_SL_MAS,_SelfLitIntensity);

					float3 RTD_SL = RTD_SL_ON;

					float3 RTD_R_SEL = lerp(RTD_R,lerp(RTD_R,RTD_TEX_COL*_TEXMCOLINT,_MaskSelfLit_var.r),_SelfLitIntensity); 
					float3 RTD_SL_CHE_1 = RTD_R_SEL;
            
				#else
            
					float3 RTD_SL = RTD_SL_OFF_OTHERS;
					float3 RTD_SL_CHE_1 = RTD_R;
            
				#endif

				#if N_F_RL_ON

            		float3 RTD_RL_ON = lerp( (lerp(RTD_SL_CHE_1, RTD_RL_LARL_OO, RTD_RL_MAIN) ), RTD_SL_CHE_1, _RimLightInLight);
					float3 RTD_RL = RTD_RL_ON;
            
				#else
            
					float3 RTD_RL = RTD_SL_CHE_1;
            
				#endif

				float3 RTD_CA_OFF_OTHERS = (RTD_RL + RTD_SL);

				float3 main_light_output = RTD_CA_OFF_OTHERS * RTD_SON_CHE_1;
        

				//=========//
				//=========//

#if N_F_PAL_ON

if (LIGHTFEATUREFLAGS_PUNCTUAL)
{

				float4 PunLigCol = 1;
				float3 PuncLigDir = 0;
				float PuncSha = 1;
				float PuncLF = 1;

				i=0;

                for (i = 0; i < _PunctualLightCount; ++i)
                {
                    LightData Plight = FetchLight(i);

					if (IsMatchingLightLayer(Plight.lightLayers, builtinData.renderingLayers))
					{

						float4 distances;

							float3 lightToSample = input.positionWS - Plight.positionRWS;

							distances.w = dot(lightToSample, Plight.forward);

							if (Plight.lightType == GPULIGHTTYPE_PROJECTOR_BOX)
							{
								PuncLigDir = -Plight.forward;
								distances.xyz = 1; 
							}
							else
							{
								float3 unL     = -lightToSample;
								float  distSq  = dot(unL, unL);
								float  distRcp = rsqrt(distSq);
								float  dist    = distSq * distRcp;

								PuncLigDir = unL * distRcp;
								distances.xyz = float3(dist, distSq, distRcp);

								ModifyDistancesForFillLighting(distances, Plight.size.x);
							}

						//=========================

						PunLigCol = float4(Plight.color * GetCurrentExposureMultiplier(), 1.0) ;

						PuncLF = PunctualLightAttenuation(distances, Plight.rangeAttenuationScale , Plight.rangeAttenuationBias, Plight.angleScale, Plight.angleOffset);

						#ifndef LIGHT_EVALUATION_NO_HEIGHT_FOG

							{
								float cosZenithAngle = PuncLigDir.y;
								float distToLight = (Plight.lightType == GPULIGHTTYPE_PROJECTOR_BOX) ? distances.w : distances.x;
								float fragmentHeight = posInput.positionWS.y;
								PunLigCol.a *= TransmittanceHeightFog(_HeightFogBaseExtinction, _HeightFogBaseHeight,
																	_HeightFogExponents, cosZenithAngle,
																fragmentHeight, distToLight);
							}
						#endif

							if (Plight.cookieIndex >= 0)
							{
								float3 lightToSample = posInput.positionWS - Plight.positionRWS;
								float4 cookie = EvaluateCookie_Punctual(context, Plight, lightToSample);
                    
								PunLigCol *= cookie;
							}
						//=============================

						#ifndef LIGHT_EVALUATION_NO_SHADOWS

						float shadowMask = 1.0;

						//Not Really Needed
						float NdotL = 1;
						
						#ifdef SHADOWS_SHADOWMASK

							PuncSha = shadowMask = (Plight.shadowMaskSelector.x >= 0.0 && NdotL > 0.0) ? dot(BUILTIN_DATA_SHADOW_MASK, Plight.shadowMaskSelector) : 1.0;

						#endif

						#if defined(SCREEN_SPACE_SHADOWS) && !defined(N_F_TRANS_ON) && (SHADERPASS != SHADERPASS_VOLUMETRIC_LIGHTING)

							if(Plight.screenSpaceShadowIndex >= 0)
							{
								PuncSha = GetScreenSpaceShadow(posInput, Plight.screenSpaceShadowIndex);
							}
							else

						#endif

							if ((Plight.shadowDimmer > 0)) //(Plight.shadowIndex >= 0) && 
							{
								PuncSha = GetPunctualShadowAttenuation(context.shadowContext, posInput.positionSS, posInput.positionWS, float3(0,0,0), Plight.shadowIndex, PuncLigDir, distances.x, Plight.lightType == GPULIGHTTYPE_POINT, Plight.lightType != GPULIGHTTYPE_PROJECTOR_BOX);
					
							#ifdef SHADOWS_SHADOWMASK
	
								PuncSha = Plight.nonLightMappedOnly ? min(shadowMask, PuncSha) : PuncSha;

							#endif

								PuncSha = lerp(shadowMask, PuncSha, Plight.shadowDimmer);
							}

						#if N_F_CS_ON
							#if !defined(N_F_TRANS_ON) && !defined(LIGHT_EVALUATION_NO_CONTACT_SHADOWS)
								PuncSha = min(PuncSha, 1 > 0.0 ? GetContactShadow(context, Plight.contactShadowMask, Plight.isRayTracedContactShadow) : 1.0); //float NdotL = dot(normalDirection, PuncLigDir);
							#endif
						#endif

							PuncSha;

						#else 

							PuncSha;

						#endif

						//=============================

					float3 lightDirection = PuncLigDir;


					#if N_F_NLASOBF_ON
						float3 lightColor = lerp(0,PunLigCol.rgb,isFrontFace);
					#else
						float3 lightColor = PunLigCol.rgb;
					#endif

					float RTD_LVLC = RTD_LVLC_F(lightColor.rgb);

					float3 floatDirection = normalize(viewDirection+lightDirection);

					#if N_F_HPSAS_ON
						float attenuation = 1; 
					#else
						float dlshmin = lerp(0,0.6,_ShadowHardness);
						float dlshmax = lerp(1,0.6,_ShadowHardness);

						#if N_F_NLASOBF_ON
							float FB_Check = lerp(1,PuncSha,isFrontFace);
						#else
							float FB_Check = PuncSha;
						#endif

						float attenuation = smoothstep(dlshmin, dlshmax ,FB_Check);
					#endif

					float lightfos = smoothstep(0, _LightFalloffSoftness ,PuncLF);


					float3 node_6588 = (_LightIntensity * lightColor.rgb);

					float3 RTD_LAS = lerp (node_1860 * RTD_LVLC, ( node_1860 * node_6588 ), _LightAffectShadow);
					float3 RTD_HL = (_HighlightColor.rgb*_HighlightColorPower+_PointSpotlightIntensity);

					float3 RTD_MCIALO = lerp(RTD_TEX_COL , lerp( lerp( (RTD_TEX_COL * _MainColor.rgb), (RTD_TEX_COL + _MainColor.rgb), _SPECMODE) , _MainTex_var.rgb * MCapOutP * _RTD_MVCOL * 0.7 , clamp((RTD_LVLC*1),0,1) ) , _MCIALO );

					#if N_F_GLO_ON

						#if N_F_GLOT_ON

							//#ifndef SHADER_API_MOBILE
								float node_5992_ang = _GlossTextureRotate;
								float node_5992_spd = 1.0;
								float node_5992_cos = cos(node_5992_spd*node_5992_ang);
								float node_5992_sin = sin(node_5992_spd*node_5992_ang);
								float2 node_5992_piv = float2(0.5,0.5);
							//#endif

								float3 RTD_GT_FL_Sli = lerp(viewDirection,floatDirection,_GlossTextureFollowLight);
								float3 node_2832 = reflect(-RTD_GT_FL_Sli,normalDirection);

								float3 RTD_GT_FOR_OO = lerp( node_2832, mul( GetWorldToObjectMatrix() , float4(node_2832,0) ).xyz, _GlossTextureFollowObjectRotation );
								float2 node_9280 = RTD_GT_FOR_OO.rg;

							//#ifndef SHADER_API_MOBILE
								float2 node_5992 = (mul(float2((-1*node_9280.r),node_9280.g)-node_5992_piv,float2x2( node_5992_cos, -node_5992_sin, node_5992_sin, node_5992_cos))+node_5992_piv);
								float2 node_8759 = (node_5992*0.5+0.5);
							//#endif

							//#ifdef SHADER_API_MOBILE
								//float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp( (float2((-1*node_2832.r),node_2832.g)*0.5+0.5) ,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
							//#else
            					float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp(node_8759,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
							//#endif

							float RTD_GT_ON = _GlossTexture_var.r;

							float RTD_GT = RTD_GT_ON;
            
						#else

							float RTD_GLO_MAIN_Sof_Sli = lerp(0.1,1.0,_GlossSoftness);
							float RTD_NDOTH = saturate(dot(floatDirection, normalDirection));
							float RTD_GLO_MAIN = smoothstep( 0.1, RTD_GLO_MAIN_Sof_Sli, pow(RTD_NDOTH,exp2(lerp(-2,15,_Glossiness))) );

							float RTD_GT = RTD_GLO_MAIN;
            
						#endif

						float node_1533 = 0.0;
						float RTD_GLO_I_Sli = lerp(node_1533, RTD_GT,_GlossIntensity);
						float4 _MaskGloss_var = tex2D(_MaskGloss,TRANSFORM_TEX(input.uv, _MaskGloss));
						float3 RTD_GLO_COL = (_GlossColor.rgb*_GlossColorPower); 
						float RTD_GLO_MAS = lerp( 0, RTD_GLO_I_Sli ,_MaskGloss_var.r);


						float RTD_GLO = RTD_GLO_MAS;
            
					#else

						float3 RTD_GLO_COL = 1.0;
						float RTD_GLO = 0;
            
					#endif	


					float3 RTD_GLO_OTHERS = RTD_GLO;

					#if N_F_RL_ON

						float node_4353 = 0.0;
						float node_3687 = 0.0;

            			float3 RTD_RL_LARL_OO = lerp( _RimLightColor.rgb, lerp(float3(node_3687,node_3687,node_3687),_RimLightColor.rgb,lightColor.rgb), _LightAffectRimLightColor ) * _RimLightColorPower;
						float RTD_RL_S_Sli = lerp(1.70,0.29,_RimLightSoftness);
						float RTD_RL_MAIN = lerp(node_4353, 1 ,smoothstep( 1.71, RTD_RL_S_Sli, pow(abs( 1.0-max(0,dot(normalDirection, viewDirection) ) ), (1.0 - _RimLightUnfill) ) ) );
						float RTD_RL_IL_OO = lerp( 0, RTD_RL_MAIN, _RimLigInt);

						float RTD_RL_CHE_1 = RTD_RL_IL_OO;
            
					#else
					
						float RTD_RL_LARL_OO = 1.0;

						float RTD_RL_CHE_1 = 0;
            
					#endif

					#if N_F_CLD_ON

            			float3 RTD_CLD_CLDFOR_OO = lerp( _CustomLightDirection.rgb, TransformObjectToWorldDir( _CustomLightDirection.xyz ), _CustomLightDirectionFollowObjectRotation );
						float3 RTD_CLD_CLDI_Sli = lerp(lightDirection,RTD_CLD_CLDFOR_OO,_CustomLightDirectionIntensity); 
						float3 RTD_CLD = RTD_CLD_CLDI_Sli;
            
					#else
            
						float3 RTD_CLD = lightDirection;
            
					#endif

					float3 RTD_ST_SS_AVD_OO = lerp( RTD_CLD, viewDirection, _SelfShadowShadowTAtViewDirection );
					float RTD_NDOTL = 0.5*dot(RTD_ST_SS_AVD_OO,RTD_SON)+0.5;

					#if N_F_ST_ON
				
						float node_8675 = 1.0;
						float node_949 = 1.0;
						float node_5738 = 1.0;
						float node_3187 = 0.22;

						float4 _ShadowT_var = tex2D(_ShadowT,TRANSFORM_TEX(input.uv, _ShadowT));

						float3 RTD_SHAT_COL = lerp( RTD_PT_COL, (_ShadowTColor.rgb*_ShadowTColorPower) * RTD_SCT * RTD_OSC, RTD_PT);
	
						float3 RTD_ST_LAF = lerp( RTD_SHAT_COL * RTD_LVLC, (RTD_SHAT_COL * node_6588) , _LightAffectShadow );

						float RTD_ST_H_Sli = lerp(0.0,0.22,_ShadowTHardness);

						float RTD_ST_IS_ON = smoothstep( RTD_ST_H_Sli, node_3187, (_ShowInAmbientLightShadowThreshold*_ShadowT_var.rgb) ); 

						#if N_F_STIAL_ON

							float node_2346 = 1.0;
							float RTD_ST_ALI_Sli = lerp(float3(node_8675,node_8675,node_8675),RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            				float RTD_STIAL_ON = lerp(RTD_ST_ALI_Sli,float3(node_2346,node_2346,node_2346),clamp((RTD_LVLC*8.0),0,1));

            				float RTD_STIAL = RTD_STIAL_ON;
            
            			#else
            
            				float RTD_STIAL = 1.0;
            
            			#endif

						#if N_F_STIS_ON
            
            				float RTD_ST_IS = lerp(1,RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            
            			#else
            
            				float RTD_ST_IS = 1;
            
            			#endif

						float RTD_ST_LFAST_OO = lerp(lerp( RTD_NDOTL, (lightfos*RTD_NDOTL), _LightFalloffAffectShadowT ) , 1 , _STIL );
						float RTD_ST_In_Sli = lerp( node_949 ,smoothstep( RTD_ST_H_Sli, node_3187, ((_ShadowT_var.r*(1.0 - _ShadowTShadowThreshold))*(RTD_ST_LFAST_OO *_ShadowTLightThreshold*0.01)) ),_ShadowTIntensity);
						float RTD_ST_ON = RTD_ST_In_Sli;

						float RTD_ST = RTD_ST_ON;
            
					#else
            
						float RTD_ST = 1;
						float RTD_SHAT_COL = 1;
						float RTD_ST_LAF = 1;
						float RTD_STIAL = 1;
						float RTD_ST_IS = 1;
            
					#endif

					float node_5573 = 1.0;

					#if N_F_SS_ON
 
						float RTD_SS_SSH_Sil = lerp(0.3,1.0,_SelfShadowHardness);
						float RTD_SS_SSTH_Sli = lerp(-1, 1, _SelfShadowThreshold);
						float RTD_SS_VCGCSSS_OO = lerp( RTD_SS_SSTH_Sli, (RTD_SS_SSTH_Sli*(1.0 - input.vertexColor.g)), VertexColorGreenControlSelfShadowThreshold );
						float RTD_SS_SST = smoothstep( RTD_SS_SSH_Sil, 1.0, (RTD_NDOTL * lerp(7, RTD_SS_VCGCSSS_OO ,RTD_SS_SSTH_Sli)) );
						float RTD_SS_SSABLSS_OO = lerp( RTD_SS_SST, lerp(RTD_SS_SST,node_5573, (1.0 - Plight.shadowDimmer) ), _SelfShadowAffectedByLightShadowStrength );
						float RTD_SS_ON = lerp(node_5573,(RTD_SS_SSABLSS_OO*attenuation),_SelfShadowRealtimeShadowIntensity);

						float RTD_SS = RTD_SS_ON;
            
					#else
            
						float RTD_SS_OFF = lerp(node_5573,attenuation,_SelfShadowRealtimeShadowIntensity);

						float RTD_SS = RTD_SS_OFF;
            
					#endif

					float3 RTD_R_OFF_OTHERS = lerp( lerp( RTD_ST_LAF, RTD_LAS, RTD_ST_IS) , lerp( RTD_ST_LAF, lerp( lerp( RTD_MCIALO_IL * RTD_HL , RTD_GLO_COL , RTD_GLO_OTHERS) , RTD_RL_LARL_OO , RTD_RL_CHE_1 ) * lightColor.rgb, RTD_ST) , RTD_SS );

					#if N_F_R_ON

						float3 RTD_FR_OFF_OTHERS = EnvRef;

						#if N_F_FR_ON
            
							float2 node_8431 = reflect(viewDirection,normalDirection).rg;
							float2 node_4207 = (float2(node_8431.r,(-1*node_8431.g))*0.5+0.5);
							float4 _FReflection_var = tex2Dlod(_FReflection,float4(TRANSFORM_TEX(node_4207, _FReflection),0.0,_ReflectionRoughtness));
							float3 RTD_FR_ON = _FReflection_var.rgb;

							float3 RTD_FR = RTD_FR_ON;
            
						#else
            
							float3 RTD_FR = RTD_FR_OFF_OTHERS;

						#endif

						float4 _MaskReflection_var = tex2D(_MaskReflection,TRANSFORM_TEX(input.uv, _MaskReflection));
						float3 RTD_R_MET_Sli = lerp(1,(9 * (RTD_TEX_COL - (9 * 0.005) ) ) , _RefMetallic);
						float3 RTD_R_MAS = lerp(RTD_R_OFF_OTHERS, (RTD_FR * RTD_R_MET_Sli) ,_MaskReflection_var.r);
						float3 RTD_R_ON = lerp(RTD_R_OFF_OTHERS, RTD_R_MAS ,_ReflectionIntensity);

						float3 RTD_R = RTD_R_ON;
            
					#else
            
						float3 RTD_R = RTD_R_OFF_OTHERS;
            
					#endif

					
					#if N_F_SL_ON

            			float3 RTD_SL_HC_OO = lerp( 1.0, RTD_TEX_COL, _SelfLitHighContrast );
						float4 _MaskSelfLit_var = tex2D(_MaskSelfLit,TRANSFORM_TEX(input.uv, _MaskSelfLit));					
						float3 RTD_SL_ON = lerp(RTD_SL_OFF_OTHERS,RTD_SL_MAS,_SelfLitIntensity);
						float3 RTD_SL = RTD_SL_ON;
						float3 RTD_R_SEL = lerp(RTD_R,lerp(RTD_R,RTD_TEX_COL*_TEXMCOLINT,_MaskSelfLit_var.r),_SelfLitIntensity); 

						float3 RTD_SL_CHE_1 = RTD_R_SEL;
            
					#else
            
						float3 RTD_SL = RTD_SL_OFF_OTHERS;
						float3 RTD_SL_CHE_1 = RTD_R;
            
					#endif


					#if N_F_RL_ON

            			float3 RTD_RL_ON = lerp( (lerp(RTD_SL_CHE_1, RTD_RL_LARL_OO, RTD_RL_MAIN) ), RTD_SL_CHE_1, _RimLightInLight);
						float3 RTD_RL = RTD_RL_ON;
            
					#else
            
						float3 RTD_RL = RTD_SL_CHE_1;
            
					#endif

					float3 RTD_CA_OFF_OTHERS = (RTD_RL + RTD_SL);

					float3 punctual_light_output = RTD_CA_OFF_OTHERS * lightfos;


					#if N_F_USETLB_ON
						A_L_O += punctual_light_output;
					#else
						A_L_O = max (punctual_light_output,A_L_O);
					#endif

					}

				}
			
	}


#endif

				//=========//
				//=========//

#if SHADEROPTIONS_AREA_LIGHTS

#if N_F_AL_ON

				if (LIGHTFEATUREFLAGS_AREA)
				{

					float4 ALigCol = 1;
					float3 ALigDir = 0;
					float ASha = 1;
					float ALF = 1;
					float3 ALF2 = 0;
					float3 ALCk = 1;

					uint NPLC = _PunctualLightCount;
					uint NALC = _AreaLightCount;

					if (NALC > 0)
					{

						i=0;

						uint last = NALC - 1;
						LightData Alight = _LightDatas[NPLC+i];

						while (i <= last )
						{

							if(IsMatchingLightLayer(Alight.lightLayers, builtinData.renderingLayers))
							{
								if (Alight.lightType == GPULIGHTTYPE_TUBE)
								{

									float  len = Alight.size.x;
									float3 T   = Alight.right;

									float3 unL = Alight.positionRWS - posInput.positionWS;
									float3 axis = Alight.right;
									ALigDir = unL + axis;

									float range          = Alight.range;
									float invAspectRatio = saturate(range / (range + (0.5 * len)));

									ALF = EllipsoidalDistanceAttenuation(unL, axis, invAspectRatio, Alight.rangeAttenuationScale, Alight.rangeAttenuationBias);

									if (ALF != 0.0)
									{
										Alight.positionRWS -= posInput.positionWS;
										
										#if N_F_ALSL_ON
											float3 P1 = Alight.positionRWS - T * (0.5 * len);
											float3 P2 = Alight.positionRWS + T * (0.5 * len);

											P1 = mul(P1, transpose( GetOrthoBasisViewNormal(viewDirection, normalDirection, dot(normalDirection,viewDirection) ) ) );
											P2 = mul(P2, transpose( GetOrthoBasisViewNormal(viewDirection, normalDirection, dot(normalDirection,viewDirection) ) ) );

											float3 B = normalize(cross(P1, P2));

											ALF2 = 1;

										#else
											//Temporary Solution
											float dis_obj_lig = distance( ( input.positionWS.xyz - objPos.xyz ) , unL ) * 0.08 * (_ALTuFo * 0.01);
											ALF2 = max( (1.0 - dis_obj_lig) , 0.0) * clamp( (0.5 * len) ,0,1);
											//==================
										#endif

										#if N_F_ALSL_ON
											ALigCol = float4(Alight.color * LTCEvaluate(P1, P2, B, k_identity3x3) *  GetCurrentExposureMultiplier() ,1.0);
										#else
											ALigCol = float4(Alight.color * ALF2 * GetCurrentExposureMultiplier() ,1.0);
										#endif
									}
						
								}
								else if (Alight.lightType == GPULIGHTTYPE_RECTANGLE)
								{

									#if SHADEROPTIONS_BARN_DOOR
										RectangularLightApplyBarnDoor(Alight, posInput.positionWS);
									#endif

									float3 unL = Alight.positionRWS - posInput.positionWS;
									ALigDir = unL;

									if (dot(Alight.forward, unL) < FLT_EPS)
									{
										float3x3 lightToWorld = float3x3(Alight.right, Alight.up, -Alight.forward);
										unL = mul(unL, transpose(lightToWorld));

										float halfWidth  = Alight.size.x * 0.5;
										float halfHeight = Alight.size.y * 0.5;

										float  range      = Alight.range;
										float3 invHalfDim = rcp(float3(range + halfWidth, range + halfHeight, range));

									#ifdef ELLIPSOIDAL_ATTENUATION

										ALF = EllipsoidalDistanceAttenuation(unL, invHalfDim, Alight.rangeAttenuationScale, Alight.rangeAttenuationBias);

									#else

										ALF = BoxDistanceAttenuation(unL, invHalfDim, Alight.rangeAttenuationScale, Alight.rangeAttenuationBias);

									#endif

										if (ALF != 0.0)
										{

											Alight.positionRWS -= posInput.positionWS;

											float4x3 lightVerts;

											lightVerts[0] = Alight.positionRWS + Alight.right * -halfWidth + Alight.up * -halfHeight;
											lightVerts[1] = Alight.positionRWS + Alight.right * -halfWidth + Alight.up *  halfHeight;
											lightVerts[2] = Alight.positionRWS + Alight.right *  halfWidth + Alight.up *  halfHeight;
											lightVerts[3] = Alight.positionRWS + Alight.right *  halfWidth + Alight.up * -halfHeight;

											#if N_F_ALSL_ON
												lightVerts = mul(lightVerts, transpose( GetOrthoBasisViewNormal(viewDirection, normalDirection, dot(normalDirection,viewDirection) ) ) );
											#else
												lightVerts = mul(lightVerts, transpose( (float3x3) lightVerts ) );
											#endif
																							
											float4x3 LD = mul(lightVerts, 1.0);

											#if N_F_ALSL_ON
												ALF2 = 1;

												float3 ALRTLF = PolygonIrradiance(LD);

												if ( Alight.cookieMode != COOKIEMODE_NONE )
												{
													float3 formFactorD =  PolygonFormFactor(LD);
													ALRTLF *= SampleAreaLightCookie(Alight.cookieScaleOffset, LD, formFactorD);
												}

												ALigCol = float4(Alight.color * ALRTLF * GetCurrentExposureMultiplier() ,1.0);
											#else
												ALF2  = Po_Ir(LD);

												if ( Alight.cookieMode != COOKIEMODE_NONE )
												{
													float3 formFactorD =  PolygonFormFactor(LD);
													ALCk = SampleAreaLightCookie(Alight.cookieScaleOffset, LD, formFactorD);
												}

												ALigCol = float4(Alight.color * ALCk * GetCurrentExposureMultiplier() ,1.0);
											#endif

										}

									}

										float shadowMask = 1.0;

										#ifdef SHADOWS_SHADOWMASK

											ASha = shadowMask = (Alight.shadowMaskSelector.x >= 0.0) ? dot(BUILTIN_DATA_SHADOW_MASK, Alight.shadowMaskSelector) : 1.0;

										#endif

										#if defined(SCREEN_SPACE_SHADOWS) && !defined(N_F_TRANS_ON)

											if (Alight.screenSpaceShadowIndex >= 0)
											{
												ASha = GetScreenSpaceShadow(posInput, Alight.screenSpaceShadowIndex);
											}
											else

										#endif 

											if (Alight.shadowIndex != -1)
											{

													ASha = GetAreaLightAttenuation(context.shadowContext, posInput.positionSS, posInput.positionWS, normalDirection, Alight.shadowIndex, normalize(Alight.positionRWS), length(Alight.positionRWS));

										#ifdef SHADOWS_SHADOWMASK
													ASha = Alight.nonLightMappedOnly ? min(shadowMask, ASha) : ASha;
										#endif
													ASha = lerp(shadowMask, ASha, Alight.shadowDimmer);
											}

								}

								float3 lightDirection = ALigDir;

								#if N_F_NLASOBF_ON
									float3 lightColor = lerp(0,ALigCol.rgb ,isFrontFace);
								#else
									float3 lightColor = ALigCol.rgb;
								#endif

								float RTD_LVLC = RTD_LVLC_F(lightColor.rgb);

								float3 floatDirection = normalize(viewDirection+lightDirection);

								#if N_F_HPSAS_ON
									float attenuation = 1; 
								#else
									float dlshmin = lerp(0,0.6,_ShadowHardness);
									float dlshmax = lerp(1,0.6,_ShadowHardness);

									#if N_F_NLASOBF_ON
										float FB_Check = lerp(1,ASha,isFrontFace);
									#else
										float FB_Check = ASha;
									#endif

									float attenuation = smoothstep(dlshmin, dlshmax ,FB_Check);
								#endif

								float3 lightfos = smoothstep(0, _LightFalloffSoftness ,ALF * ALF2); 


								float3 node_6588 = (_LightIntensity * lightColor.rgb);

								float3 RTD_LAS = lerp (node_1860 * RTD_LVLC, ( node_1860 * node_6588 ), _LightAffectShadow);
								float3 RTD_HL = (_HighlightColor.rgb*_HighlightColorPower+_ALIntensity);

								float3 RTD_MCIALO = lerp(RTD_TEX_COL , lerp( lerp( (RTD_TEX_COL * _MainColor.rgb), (RTD_TEX_COL + _MainColor.rgb), _SPECMODE) , _MainTex_var.rgb * MCapOutP * _RTD_MVCOL * 0.7 , clamp((RTD_LVLC*1),0,1) ) , _MCIALO );

								#if N_F_GLO_ON

									#if N_F_GLOT_ON

										//#ifndef SHADER_API_MOBILE
											float node_5992_ang = _GlossTextureRotate;
											float node_5992_spd = 1.0;
											float node_5992_cos = cos(node_5992_spd*node_5992_ang);
											float node_5992_sin = sin(node_5992_spd*node_5992_ang);
											float2 node_5992_piv = float2(0.5,0.5);
										//#endif

											float3 RTD_GT_FL_Sli = lerp(viewDirection,floatDirection,_GlossTextureFollowLight);
											float3 node_2832 = reflect(-RTD_GT_FL_Sli,normalDirection);

											float3 RTD_GT_FOR_OO = lerp( node_2832, mul( GetWorldToObjectMatrix() , float4(node_2832,0) ).xyz, _GlossTextureFollowObjectRotation );
											float2 node_9280 = RTD_GT_FOR_OO.rg;

										//#ifndef SHADER_API_MOBILE
											float2 node_5992 = (mul(float2((-1*node_9280.r),node_9280.g)-node_5992_piv,float2x2( node_5992_cos, -node_5992_sin, node_5992_sin, node_5992_cos))+node_5992_piv);
											float2 node_8759 = (node_5992*0.5+0.5);
										//#endif

										//#ifdef SHADER_API_MOBILE
											//float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp( (float2((-1*node_2832.r),node_2832.g)*0.5+0.5) ,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
										//#else
            								float4 _GlossTexture_var = tex2Dlod(_GlossTexture,float4(TRANSFORM_TEX( lerp(node_8759,RTD_VD_Cal,_PSGLOTEX) , _GlossTexture),0.0,_GlossTextureSoftness));
										//#endif

										float RTD_GT_ON = _GlossTexture_var.r;

										float RTD_GT = RTD_GT_ON;
            
									#else

										float RTD_GLO_MAIN_Sof_Sli = lerp(0.1,1.0,_GlossSoftness);
										float RTD_NDOTH = saturate(dot(floatDirection, normalDirection));
										float RTD_GLO_MAIN = smoothstep( 0.1, RTD_GLO_MAIN_Sof_Sli, pow(RTD_NDOTH,exp2(lerp(-2,15,_Glossiness))) );

										float RTD_GT = RTD_GLO_MAIN;
            
									#endif

									float node_1533 = 0.0;
									float RTD_GLO_I_Sli = lerp(node_1533, RTD_GT,_GlossIntensity);
									float4 _MaskGloss_var = tex2D(_MaskGloss,TRANSFORM_TEX(input.uv, _MaskGloss));
									float3 RTD_GLO_COL = (_GlossColor.rgb*_GlossColorPower); 
									float RTD_GLO_MAS = lerp( 0, RTD_GLO_I_Sli ,_MaskGloss_var.r);


									float RTD_GLO = RTD_GLO_MAS;
            
								#else

									float3 RTD_GLO_COL = 1.0;
									float RTD_GLO = 0;
            
								#endif	


								float3 RTD_GLO_OTHERS = RTD_GLO;

								#if N_F_RL_ON

									float node_4353 = 0.0;
									float node_3687 = 0.0;

            						float3 RTD_RL_LARL_OO = lerp( _RimLightColor.rgb, lerp(float3(node_3687,node_3687,node_3687),_RimLightColor.rgb,lightColor.rgb), _LightAffectRimLightColor ) * _RimLightColorPower;
									float RTD_RL_S_Sli = lerp(1.70,0.29,_RimLightSoftness);
									float RTD_RL_MAIN = lerp(node_4353, 1 ,smoothstep( 1.71, RTD_RL_S_Sli, pow(abs( 1.0-max(0,dot(normalDirection, viewDirection) ) ), (1.0 - _RimLightUnfill) ) ) );
									float RTD_RL_IL_OO = lerp( 0, RTD_RL_MAIN, _RimLigInt);

									float RTD_RL_CHE_1 = RTD_RL_IL_OO;
            
								#else
					
									float RTD_RL_LARL_OO = 1.0;

									float RTD_RL_CHE_1 = 0;
            
								#endif

								#if N_F_CLD_ON

            						float3 RTD_CLD_CLDFOR_OO = lerp( _CustomLightDirection.rgb, TransformObjectToWorldDir( _CustomLightDirection.xyz ), _CustomLightDirectionFollowObjectRotation );
									float3 RTD_CLD_CLDI_Sli = lerp(lightDirection,RTD_CLD_CLDFOR_OO,_CustomLightDirectionIntensity); 
									float3 RTD_CLD = RTD_CLD_CLDI_Sli;
            
								#else
            
									float3 RTD_CLD = lightDirection;
            
								#endif

								float3 RTD_ST_SS_AVD_OO = lerp( RTD_CLD, viewDirection, _SelfShadowShadowTAtViewDirection );
								float RTD_NDOTL = 0.5*dot(RTD_ST_SS_AVD_OO,RTD_SON)+0.5;

								#if N_F_ST_ON
				
									float node_8675 = 1.0;
									float node_949 = 1.0;
									float node_5738 = 1.0;
									float node_3187 = 0.22;

									float4 _ShadowT_var = tex2D(_ShadowT,TRANSFORM_TEX(input.uv, _ShadowT));

									float3 RTD_SHAT_COL = lerp( RTD_PT_COL, (_ShadowTColor.rgb*_ShadowTColorPower) * RTD_SCT * RTD_OSC, RTD_PT);
	
									float3 RTD_ST_LAF = lerp( RTD_SHAT_COL * RTD_LVLC, (RTD_SHAT_COL * node_6588) , _LightAffectShadow );

									float RTD_ST_H_Sli = lerp(0.0,0.22,_ShadowTHardness);

									float RTD_ST_IS_ON = smoothstep( RTD_ST_H_Sli, node_3187, (_ShowInAmbientLightShadowThreshold*_ShadowT_var.rgb) ); 

									#if N_F_STIAL_ON

										float node_2346 = 1.0;
										float RTD_ST_ALI_Sli = lerp(float3(node_8675,node_8675,node_8675),RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            							float RTD_STIAL_ON = lerp(RTD_ST_ALI_Sli,float3(node_2346,node_2346,node_2346),clamp((RTD_LVLC*8.0),0,1));

            							float RTD_STIAL = RTD_STIAL_ON;
            
            						#else
            
            							float RTD_STIAL = 1.0;
            
            						#endif

									#if N_F_STIS_ON
            
            							float RTD_ST_IS = lerp(1,RTD_ST_IS_ON,_ShowInAmbientLightShadowIntensity);
            
            						#else
            
            							float RTD_ST_IS = 1;
            
            						#endif

									float RTD_ST_LFAST_OO = lerp(lerp( RTD_NDOTL, (lightfos*RTD_NDOTL), _LightFalloffAffectShadowT ) , 1 , _STIL );
									float RTD_ST_In_Sli = lerp( node_949 ,smoothstep( RTD_ST_H_Sli, node_3187, ((_ShadowT_var.r*(1.0 - _ShadowTShadowThreshold))*(RTD_ST_LFAST_OO *_ShadowTLightThreshold*0.01)) ),_ShadowTIntensity);
									float RTD_ST_ON = RTD_ST_In_Sli;

									float RTD_ST = RTD_ST_ON;
            
								#else
            
									float RTD_ST = 1;
									float RTD_SHAT_COL = 1;
									float RTD_ST_LAF = 1;
									float RTD_STIAL = 1;
									float RTD_ST_IS = 1;
            
								#endif

								float node_5573 = 1.0;

								#if N_F_SS_ON
 
									float RTD_SS_SSH_Sil = lerp(0.3,1.0,_SelfShadowHardness);
									float RTD_SS_SSTH_Sli = lerp(-1, 1, _SelfShadowThreshold);
									float RTD_SS_VCGCSSS_OO = lerp( RTD_SS_SSTH_Sli, (RTD_SS_SSTH_Sli*(1.0 - input.vertexColor.g)), VertexColorGreenControlSelfShadowThreshold );
									float RTD_SS_SST = smoothstep( RTD_SS_SSH_Sil, 1.0, (RTD_NDOTL * lerp(7, RTD_SS_VCGCSSS_OO ,RTD_SS_SSTH_Sli)) );
									float RTD_SS_SSABLSS_OO = lerp( RTD_SS_SST, lerp(RTD_SS_SST,node_5573, (1.0 - Alight.shadowDimmer) ), _SelfShadowAffectedByLightShadowStrength );
									float RTD_SS_ON = lerp(node_5573,(RTD_SS_SSABLSS_OO*attenuation),_SelfShadowRealtimeShadowIntensity);

									float RTD_SS = RTD_SS_ON;
            
								#else
            
									float RTD_SS_OFF = lerp(node_5573,attenuation,_SelfShadowRealtimeShadowIntensity);

									float RTD_SS = RTD_SS_OFF;
            
								#endif

								float3 RTD_R_OFF_OTHERS = lerp( lerp( RTD_ST_LAF, RTD_LAS, RTD_ST_IS) , lerp( RTD_ST_LAF, lerp( lerp( RTD_MCIALO_IL * RTD_HL , RTD_GLO_COL , RTD_GLO_OTHERS) , RTD_RL_LARL_OO , RTD_RL_CHE_1 ) * lightColor.rgb, RTD_ST) , RTD_SS );

								#if N_F_R_ON

									float3 RTD_FR_OFF_OTHERS = EnvRef;

									#if N_F_FR_ON
            
										float2 node_8431 = reflect(viewDirection,normalDirection).rg;
										float2 node_4207 = (float2(node_8431.r,(-1*node_8431.g))*0.5+0.5);
										float4 _FReflection_var = tex2Dlod(_FReflection,float4(TRANSFORM_TEX(node_4207, _FReflection),0.0,_ReflectionRoughtness));
										float3 RTD_FR_ON = _FReflection_var.rgb;

										float3 RTD_FR = RTD_FR_ON;
            
									#else
            
										float3 RTD_FR = RTD_FR_OFF_OTHERS;

									#endif

									float4 _MaskReflection_var = tex2D(_MaskReflection,TRANSFORM_TEX(input.uv, _MaskReflection));
									float3 RTD_R_MET_Sli = lerp(1,(9 * (RTD_TEX_COL - (9 * 0.005) ) ) , _RefMetallic);
									float3 RTD_R_MAS = lerp(RTD_R_OFF_OTHERS, (RTD_FR * RTD_R_MET_Sli) ,_MaskReflection_var.r);
									float3 RTD_R_ON = lerp(RTD_R_OFF_OTHERS, RTD_R_MAS ,_ReflectionIntensity);

									float3 RTD_R = RTD_R_ON;
            
								#else
            
									float3 RTD_R = RTD_R_OFF_OTHERS;
            
								#endif

					
								#if N_F_SL_ON

            						float3 RTD_SL_HC_OO = lerp( 1.0, RTD_TEX_COL, _SelfLitHighContrast );
									float4 _MaskSelfLit_var = tex2D(_MaskSelfLit,TRANSFORM_TEX(input.uv, _MaskSelfLit));					
									float3 RTD_SL_ON = lerp(RTD_SL_OFF_OTHERS,RTD_SL_MAS,_SelfLitIntensity);
									float3 RTD_SL = RTD_SL_ON;
									float3 RTD_R_SEL = lerp(RTD_R,lerp(RTD_R,RTD_TEX_COL*_TEXMCOLINT,_MaskSelfLit_var.r),_SelfLitIntensity); 

									float3 RTD_SL_CHE_1 = RTD_R_SEL;
            
								#else
            
									float3 RTD_SL = RTD_SL_OFF_OTHERS;
									float3 RTD_SL_CHE_1 = RTD_R;
            
								#endif


								#if N_F_RL_ON

            						float3 RTD_RL_ON = lerp( (lerp(RTD_SL_CHE_1, RTD_RL_LARL_OO, RTD_RL_MAIN) ), RTD_SL_CHE_1, _RimLightInLight);
									float3 RTD_RL = RTD_RL_ON;
            
								#else
            
									float3 RTD_RL = RTD_SL_CHE_1;
            
								#endif

								float3 RTD_CA_OFF_OTHERS = (RTD_RL + RTD_SL);

								float3 area_light_output = RTD_CA_OFF_OTHERS * lightfos;


								#if N_F_USETLB_ON
									Ar_L_O += area_light_output;
								#else
									Ar_L_O = max (area_light_output,Ar_L_O);
								#endif

							}

							Alight = _LightDatas[NPLC+(min(++i, last))];

						}
					}
				}

#endif

#endif

				//=========//
				//=========//

				#if N_F_USETLB_ON
					color = main_light_output + A_L_O + Ar_L_O;
				#else
					color = max(max(main_light_output ,A_L_O), Ar_L_O);
				#endif


				#if N_F_TRANS_ON

					float Trans_Val = 1;
					
					#ifndef N_F_CO_ON

						Trans_Val = RTD_TRAN_OPA_Sli;

					#endif
					
				#else

					float Trans_Val = 1;

				#endif

				#ifdef DEBUG_DISPLAY
					color = RTD_TEX_COL * exp2(_DebugExposure);
				#endif

				#if N_F_CA_ON
            
					float3 RTD_CA_ON = lerp(color,dot(color,float3(0.3,0.59,0.11)),(1.0 - _Saturation));
					float3 RTD_CA = RTD_CA_ON;
            
				#else

					float3 RTD_CA = color;
            
				#endif

				float4 finalRGBA = ApplyBlendMode(RTD_CA, Trans_Val);

                outColor = EL_AT_SC(posInput, viewDirection, finalRGBA);

            }

            ENDHLSL
			 
        }

    }

    FallBack "Hidden/InternalErrorShader"

  CustomEditor "RealToonShaderGUI_HDRP_SRP"
}