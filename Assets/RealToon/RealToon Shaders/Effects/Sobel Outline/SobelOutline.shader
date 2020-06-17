//RealToon - Sobel Outline Effect (HDRP - Post Processing)
//MJQStudioWorks
//2020

Shader  "Hidden/HDRP/RealToon/Effects/SobelOutline"
{

    HLSLINCLUDE
		
		#pragma target 4.5
		#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
		#include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/FXAA.hlsl"
		#include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/RTUpscale.hlsl"

		struct Attributes {
            uint vertexID : SV_VertexID;
			UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct Varyings {
            float4 positionCS : SV_POSITION;
            float2 texcoord    : TEXCOORD1;
        };

        Varyings Vert (Attributes input) {

            Varyings output;
			ZERO_INITIALIZE(Varyings, output);

			UNITY_SETUP_INSTANCE_ID(input);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

            output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
			output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
			

            return output;
        }

		TEXTURE2D_X(_InputTexture);

		float _OutlineWidth;
        float3 _OutlineColor;
		float _OutlineColorIntensity;
        float _OutlineThreshold;
		float _ColOutMiSel;
		float _OutOnSel;

        float SamDep(float2 uv)
        {
			return LOAD_TEXTURE2D_X(_CameraDepthTexture, uv).r;
        }

		float sob_fil (float CX, float2 uv) 
        {
            float2 d = float2(CX, CX);
                
            float hr = 0;
            float vt = 0;
                
            hr += SamDep(uv + float2(-1.0, -1.0) * d) *  1.0;
            hr += SamDep(uv + float2( 1.0, -1.0) * d) * -1.0;
            hr += SamDep(uv + float2(-1.0,  0.0) * d) *  2.0;
            hr += SamDep(uv + float2( 1.0,  0.0) * d) * -2.0;
            hr += SamDep(uv + float2(-1.0,  1.0) * d) *  1.0;
            hr += SamDep(uv + float2( 1.0,  1.0) * d) * -1.0;
                
            vt += SamDep(uv + float2(-1.0, -1.0) * d) *  1.0;
            vt += SamDep(uv + float2( 0.0, -1.0) * d) *  2.0;
            vt += SamDep(uv + float2( 1.0, -1.0) * d) *  1.0;
            vt += SamDep(uv + float2(-1.0,  1.0) * d) * -1.0;
            vt += SamDep(uv + float2( 0.0,  1.0) * d) * -2.0;
            vt += SamDep(uv + float2( 1.0,  1.0) * d) * -1.0;
                
            return sqrt( pow(hr * hr,2) + pow(vt * vt,2) ) * 100;
        }

        float4 CustomPostProcess(Varyings input) : SV_Target
        {
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				uint2 positionSS = input.texcoord * _ScreenSize.xy;

                float3 ful_scr_so = LOAD_TEXTURE2D_X(_InputTexture, positionSS).xyz;
                float sob_fil_o = sob_fil(_OutlineWidth, positionSS) > ( (_OutlineThreshold * 0.01) * SamDep(positionSS) ) ? 1 : 0;
				float3 coloutmix = lerp(_OutlineColor, ful_scr_so * _OutlineColor, _ColOutMiSel);
                return float4( lerp( coloutmix * _OutlineColorIntensity, lerp( ful_scr_so , 1, _OutOnSel ) , (1.0 - sob_fil_o) ) ,1);
        }

    ENDHLSL

    SubShader
    {
		Tags{ "RenderPipeline" = "HDRenderPipeline" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex Vert
                #pragma fragment CustomPostProcess

            ENDHLSL
        }
    }

	Fallback Off
}
