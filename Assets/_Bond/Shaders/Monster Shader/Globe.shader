Shader "Unlit/Globe"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Highlight("Highlight", Color) = (1,1,1,1)
        _Threshold("Highlight Threshold", Float) = 0.8
        _InteriorColor("Interior Color", Color) = (1, 1, 1, 1)
        _InteriorColorStrength("Interior Color Strength", Float) = 1.0
        _BoilStrength("Boil Strength", Float) = 1
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _NoiseScrollSpeed("Noise Scroll Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="GlobeBuffer" }
        LOD 50

        Pass
        {
            CGPROGRAM
            #pragma target 3.5
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float noise : PSIZE;
            };

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float4 _Color;
            float4 _Highlight;
            float _Threshold;
            float4 _InteriorColor;
            float _InteriorColorStrength;
            float _BoilStrength;
            float _GlobeBoilScroll;
            float _NoiseScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                float scroll = _NoiseScrollSpeed * _GlobeBoilScroll;
                float4 uv = TRANSFORM_TEX(v.uv + float2(scroll, scroll), _NoiseTex).xyxy;
                uv.w = 0;
                float noise = tex2Dlod(_NoiseTex, uv).r;
                float boil = noise * _BoilStrength;

                o.vertex = UnityObjectToClipPos(v.vertex + v.normal * boil);
                //o.vertex = UnityObjectToClipPos(v.vertex + v.normal);
                o.noise = noise;

                o.normal = UnityObjectToWorldNormal(v.normal);
                    
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = _Color;
                if (max(0, dot(normalize(_WorldSpaceLightPos0.xyz), i.normal)) > _Threshold)// UNITY_MATRIX_IT_MV[2].xyz
                {
                    col = _Highlight;
                }
                float3 forward = mul((float3x3)unity_CameraToWorld, float3(0, 0, -1));
                return lerp(col, _InteriorColor, dot(normalize(forward), i.normal) * _InteriorColorStrength);
            }
            ENDCG
        }
    }
}
