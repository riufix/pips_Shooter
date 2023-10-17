Shader "Unlit/Water"
{
    Properties
    {
        _VoronoiTex ("Voronoi Texture", 2D) = "white" {}
        _BaseColor ("Color", Color) = (0,0,0,1)
        _Intensity ("Intensity", Float) = .5
        _Size ("Size", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _VoronoiTex;
            float4 _BaseColor;
            float _Intensity, _Size;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 Waveform(float2 coords, float3 Wave){
                return coords + float2(sin((_Time.x + Wave.x * coords.y) * Wave.y + coords.y)*Wave.z,0);
            }

            float Layer(float2 UV){
                fixed4 Voronoi = tex2D(_VoronoiTex, UV);
                return Voronoi.x-0.02;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float size = 1;
                fixed4 col = _BaseColor;
                float Caustics =  0;
                Caustics +=            Layer(Waveform(i.uv * 1.75 * _Size + float2(_Time.x, 0), float3(0.5, 5, 0.2)));
                Caustics +=       .4 * Layer(Waveform(i.uv * 1.2  * _Size + float2(_Time.x*.5, _Time.x*.5), float3(0.1, 5, 0.4)));
                Caustics +=       .2 * Layer(Waveform(i.uv        * _Size + float2(0,_Time.x), float3(0.05, 5, 0.2)));
                Caustics +=       .1 * Layer(Waveform(i.uv * .2   * _Size + float2(-_Time.x, -_Time.x*.5), float3(0.15, 2, 0.1)));

                return Caustics*_Intensity + col;
            }
            ENDCG
        }
    }
}
