Shader "GUIllaume/PlainColourShader"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" { }
        _OcclusionMap("Occlusion", 2D) = "white" { }
        _OcclusionStrength("Strength", Range(0.000000,1.000000)) = 1.000000
        _PearlStrength("PearlStrength", Range(0.000000,1.00000)) = 0.05
        _EmissionColor("Emission", Color) = (0.000000,0.000000,0.000000,1.000000)
        _Color("Main(BG) Color", Color) = (0.000000,0.000000,0.000000,1.000000)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque"}
        LOD 200
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 6.0

        sampler2D _OcclusionMap;
        sampler2D _MainTex;
        float _OcclusionStrength;
        float _PearlStrength;

        struct Input
        {
            float2 uv_MainTex;
            float3 WorldNormal; 
            float3 worldRefl;
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
            fixed4 color = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = /*_EmissionColor * _EmissionColor.a **/ _PearlStrength * cross(-normalize(IN.worldRefl),IN.WorldNormal) * color.rgb+ (_Color.rgb * _Color.a);// _EmissionColor;
            
            o.Emission = _EmissionColor * _EmissionColor.a;
            // Metallic and smoothness come from slider variables
            o.Metallic = 1.0 - (tex2D(_OcclusionMap, IN.uv_MainTex) * _OcclusionStrength* 0.4);
            o.Smoothness = _OcclusionStrength*tex2D(_OcclusionMap, IN.uv_MainTex) * 0.28;
            o.Alpha = sign(color.a-0.94);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
