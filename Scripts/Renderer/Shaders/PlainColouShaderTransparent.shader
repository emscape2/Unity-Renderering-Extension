Shader"GUIllaume/SinusoidRenderTexTransaparent"
{
    Properties
    {
        [MainTexture] _MainTex("Sprite", 2D) = "white" { }
        _Alpha("Alpha", Range(0.000000,1.000000)) = 1.000000
        _Emission("Emission", Color) = (0.000000,0.000000,0.000000,1.000000)
        [MainColor] _Color("Main Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        
        //_BumpScale("Scale", Float) = 1.000000
        //[Normal]  _BumpMap("Normal Map", 2D) = "bump" { }
    }
    SubShader
    {
        Tags {"RenderType" = "Overlay" 
            "Queue" = "Overlay" 
            "ForceNoShadowCasting" = "True"
            "IgnoreProjector" = "True" }
         
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade 
        //#pragma surface surf Standard fullforwardshadows 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

sampler2D _MainTex;
float _Alpha;

struct Input
{
    float2 uv_MainTex;
};

fixed4 _Emission;
fixed4 _Color;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
      //  UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
      //  UNITY_INSTANCING_BUFFER_END(Props)

                void surf(Input IN, inout SurfaceOutputStandard o)
                {
                            // Albedo comes from a texture tinted by color
                    fixed4 color = tex2D(_MainTex, IN.uv_MainTex);
                    o.Albedo = color.rgb;
                    o.Emission = color.rgb * _Emission * _Emission.a * _Alpha + ((1.0 - _Alpha) * _Color.rgb);
           
                    o.Alpha = color.a * _Alpha;
                }
        ENDCG
    }
FallBack"Unlit/Transparent Cutout"
}
