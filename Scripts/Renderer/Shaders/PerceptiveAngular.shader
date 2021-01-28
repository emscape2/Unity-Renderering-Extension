Shader "GUIllaume/PerceptiveAngularShader"
{
    Properties
    {
        [MainTexture] _MainTex("Sprite", 2D) = "white" { }
        _PearlStrength("PearlStrength", Range(0.000000,3.00000)) = 0.05
        _OcclusionStrength("OcclusionStrength", Range(0.000000,3.00000)) = 0.05
        _EmissionColor("Emission", Color) = (0.000000,0.000000,0.000000,1.000000)
        [MainColor] _Color("Main(BG) Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        
        //_BumpScale("Scale", Float) = 1.000000
        //[Normal]  _BumpMap("Normal Map", 2D) = "bump" { }
    }
    SubShader
    {
        //Tags { "RenderType" = "Opague"}
        Tags { "RenderType" = "Transparent"}
        LOD 200
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend 
       // #pragma surface surf Standard fullforwardshadows 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 5.0

        sampler2D _MainTex;
        float _OcclusionStrength;
        float _PearlStrength;

        struct Input
        {
            float2 uv_MainTex;
            float3 WorldNormal; 
            float3 worldRefl;
            float4 _WorldSpaceCameraPos;
        };

        fixed4 _EmissionColor;
        fixed4 _Color;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            half3 color = normalize(ObjSpaceViewDir(IN._WorldSpaceCameraPos));
                // tex2D(_MainTex, IN.uv_MainTex);
            half3 colour =  _PearlStrength * cross(-normalize(IN.worldRefl),IN.WorldNormal) * color.rgb+ (_Color.rgb * _Color.a);// _EmissionColor;
            o.Albedo = _Color.a * cross(colour, IN._WorldSpaceCameraPos.xyz); //*/*
            half4 texcol = tex2D(_MainTex, IN.uv_MainTex);
            o.Emission = texcol * (1- _EmissionColor.a) + _EmissionColor * _EmissionColor.a *normalize((   reflect(_WorldSpaceLightPos0, color.rgb)));
            // Metallic and smoothness come from slider variables
            o.Metallic = colour* _OcclusionStrength;
            o.Smoothness = color + _OcclusionStrength*0.2;
            o.Alpha = texcol.a;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/Diffuse"
}
