// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GUIllaume/PlainColourShaderVertex"
{
    Properties
    {
       [PowerSlider(5.0)] _Shininess("Strength", Range(0.000000,1.000000)) = 1.000000
        _PearlStrength("PearlStrength", Range(0.000000,1.00000)) = 0.05
        _Emission("Emission", Color) = (0.000000,0.000000,0.000000,1.000000)
        [MainColor] _Color("Main(BG) Color", Color) = (0.000000,0.000000,0.000000,1.000000)

           //_BumpScale("Scale", Float) = 1.000000
           //[Normal]  _BumpMap("Normal Map", 2D) = "bump" { }
    }
        SubShader{
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 3.0

                #include "UnityCG.cginc"

                struct vertexInput {
                    float4 vertex : POSITION;
                };
    uniform fixed4 _Color;
    uniform fixed4 _Emission;
    uniform float _PearlStrength;
    uniform float _Shininess;
                struct fragmentInput {
                    float4 position : SV_POSITION;
                };

                fragmentInput vert(vertexInput i) {
                    fragmentInput o;
                    o.position = UnityObjectToClipPos(i.vertex);
                    return o;
                }
                fixed4 frag(fragmentInput i) : SV_Target{
                    fixed4 relf = reflect(_Color , normalize(fixed4(cross(_Shininess,i.position).xyz,1.0)));
                    return relf *_PearlStrength *0.1+ _Emission;
                }
                ENDCG
            }
    }
           FallBack "Unlit/Color"
}
