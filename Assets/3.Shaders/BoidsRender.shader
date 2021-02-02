Shader "Eugene/BoidsRender"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline"}

        Pass
        {
            Cull Back //use default culling because this shader is billboard 
            ZTest Less
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // -------------------------------------
            // Universal Render Pipeline keywords
            // When doing custom shaders you most often want to copy and paste these #pragmas
            // These multi_compile variants are stripped from the build depending on:
            // 1) Settings in the URP Asset assigned in the GraphicsSettings at build time
            // e.g If you disabled AdditionalLights in the asset then all _ADDITIONA_LIGHTS variants
            // will be stripped from build
            // 2) Invalid combinations are stripped. e.g variants with _MAIN_LIGHT_SHADOWS_CASCADE
            // but not _MAIN_LIGHT_SHADOWS are invalid and therefore stripped.
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile_fog
            // -------------------------------------

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                half3  color  : COLOR;
                float3 normal : NORMAL;
                float2 uv     : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 vertex      : SV_POSITION;
                half3  color       : COLOR;
                float3 normal      : NORMAL;
                float2 uv          : TEXCOORD0;
                float  fogCoord    : TEXCOORD1;
                float4 shadowCoord : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // Boid의 구조체
            struct BoidData
            {
                float3 velocity; // 속도
                float3 position; // 위치
            };

            //머터리얼마다 변경되는 값
            CBUFFER_START(UnityPerMaterial)
                // Boid 데이터의 구조체 버퍼
                StructuredBuffer<BoidData> _BoidDataBuffer;

                sampler2D _MainTex; // 텍스처
                float4 _MainTex_ST;

                half   _Glossiness; // 광택
                half   _Metallic;   // 금속특성
                float4  _Color;      // 컬러

                float3 _ObjectScale; // Boid 객체의 크기
            CBUFFER_END


            // 오일러각(라디안)을 회전 행렬로 변환
            float4x4 eulerAnglesToRotationMatrix(float3 angles)
            {
                float ch = cos(angles.y); float sh = sin(angles.y); // heading
                float ca = cos(angles.z); float sa = sin(angles.z); // attitude
                float cb = cos(angles.x); float sb = sin(angles.x); // bank

                // Ry-Rx-Rz (Yaw Pitch Roll)
                return float4x4(
                    ch * ca + sh * sb * sa, -ch * sa + sh * sb * ca, sh * cb, 0,
                    cb * sa, cb * ca, -sb, 0,
                    -sh * ca + ch * sb * sa, sh * sa + ch * sb * ca, ch * cb, 0,
                    0, 0, 0, 1
                    );
            }

            Varyings vert(Attributes IN, uint instanceID : SV_InstanceID)
            {
                Varyings OUT;

                // 인스턴스 ID로 BOid의 데이터를 가져오기
                BoidData boidData = _BoidDataBuffer[instanceID];

                float3 pos = boidData.position.xyz; // Boid의 위치를 취득
                float3 scl = _ObjectScale;          // Boid의 스케일을 취득

                // 객체의 좌표에서 월드 좌표를 변환하는 행렬을 정의
                float4x4 object2world = (float4x4)0;
                // 스케일 값 대입
                object2world._11_22_33_44 = float4(scl.xyz, 1.0);
                // 속도에서 Y축에 대한 회전을 산출
                float rotY =
                    atan2(boidData.velocity.x, boidData.velocity.z);
                // 속도에서 X축에 대한 회전을 산출
                float rotX =
                    -asin(boidData.velocity.y / (length(boidData.velocity.xyz) + 1e-8));
                // 오일러각(라디안)에서 회전 행렬을 구한다
                float4x4 rotMatrix = eulerAnglesToRotationMatrix(float3(rotX, rotY, 0));
                // 행렬에 회전을 적용
                object2world = mul(rotMatrix, object2world);
                // 행렬에 평행이동을 적용
                object2world._14_24_34 += pos.xyz;

                // 정점을 좌표 변환
                OUT.vertex = mul(object2world, IN.vertex);
                OUT.vertex = mul(UNITY_MATRIX_MVP, OUT.vertex);

                // 법선을 좌표 변환
                OUT.normal = normalize(mul(object2world, IN.normal));
                OUT.color = IN.color;

                //
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.fogCoord = ComputeFogFactor(OUT.vertex.z);

                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.vertex.xyz);
                OUT.shadowCoord = GetShadowCoord(vertexInput);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);

                //Lighting Calculate(Lambert)
                Light mainLight = GetMainLight(IN.shadowCoord);
                float NdotL = saturate(dot(normalize(_MainLightPosition.xyz), IN.normal));
                float3 ambient = SampleSH(IN.normal);

                float4 col = tex2D(_MainTex, IN.uv) * _Color;
                //below texture sampling code does not use in material inspector
                //float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_Maintex, IN.uv);dd
                
                col.rgb *= NdotL * _MainLightColor.rgb * mainLight.shadowAttenuation * mainLight.distanceAttenuation + ambient;
                col.rgb = MixFog(col.rgb, IN.fogCoord);
                return col;
            }
            ENDHLSL
        }
    }
}
