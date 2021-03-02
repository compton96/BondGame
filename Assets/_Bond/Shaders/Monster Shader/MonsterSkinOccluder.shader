Shader "Unlit/MonsterSkinOccluder"
{
    Properties
    {

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 150

        Pass
        {
            Color (0, 0, 0, 0)
            Cull Front
        }
    }
}
