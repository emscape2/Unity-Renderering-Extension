Shader "GUIllaume/PlainColourShader,Transparent"
{
    Properties
    {
        [MainTexture] _MainTex("Sprite", 2D) = "white" { }
        _OcclusionMap("Occlusion", 2D) = "white" { }
        _Cutoff("Strength", Range(0.000000,1.000000)) = 1.000000
        _Alpha("Alpha", Range(0.000000,1.000000)) = 1.000000
         [PowerSlider(5.0)]  _Shininess("PearlStrength", Range(0.000000,1.00000)) = 0.05
        _Emission("Emission", Color) = (0.000000,0.000000,0.000000,1.000000)
        [MainColor] _Color("Main Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        
        //_BumpScale("Scale", Float) = 1.000000
        //[Normal]  _BumpMap("Normal Map", 2D) = "bump" { }
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"}
        LOD 200
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade 
        //#pragma surface surf Standard fullforwardshadows 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 5.0

        sampler2D _OcclusionMap;
        sampler2D _MainTex;
        float _Shininess;
        float _Cutoff;
        float _Alpha;

        struct Input
        {
            float2 uv_MainTex;
            float3 WorldNormal; 
            float3 worldRefl;
        };

        fixed4 _Emission;
        fixed4 _Color;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
      //  UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
      //  UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 color = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = /*_EmissionColor */ _Color.a * _Cutoff * cross(-normalize(IN.worldRefl), IN.WorldNormal) * color.rgb + (_Color.rgb);// _EmissionColor;
            o.Emission = color.rgb *_Emission * _Emission.a;
            // Metallic and smoothness come from slider variables
            o.Metallic = 1.0 - (tex2D(_OcclusionMap, IN.uv_MainTex) * _Shininess * 0.4);
            o.Smoothness = _Shininess * tex2D(_OcclusionMap, IN.uv_MainTex) * 0.28;
            o.Alpha = color.a * _Alpha;
}
        ENDCG
    }
                   FallBack "Unlit/Transparent Cutout"
}
