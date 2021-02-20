Shader "Unlit/Depth"
{
    Properties
    {
        _Range("Range", Float) = 1.0
        _Root("Root", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float depth : DEPTH;
            };

            float _Range;
            float _Root;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.depth = (-mul(UNITY_MATRIX_MV, v.vertex).z + _Root) * _Range;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.depth;
            }
            ENDCG
        }
    }
}
