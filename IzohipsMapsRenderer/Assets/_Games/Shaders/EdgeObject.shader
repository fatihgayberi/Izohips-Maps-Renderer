Shader "Wonnasimth_Object/EdgeObject"
{
    Properties
    {        
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MapThreshold ("MapThreshold", float) = 0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        float _MapThreshold;
        float4 _MainTex_TexelSize; // float4(1 / width, 1 / height, width, height)

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float LUM (float3 c)
        {
            return c.r*.3 + c.g*.59 + c.b*.11;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 C = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float3 N = tex2D(_MainTex, IN.uv_MainTex + fixed2(0, _MainTex_TexelSize.y)).rgb;
            float3 S = tex2D(_MainTex, IN.uv_MainTex - fixed2(0, _MainTex_TexelSize.y)).rgb;
            float3 W = tex2D(_MainTex, IN.uv_MainTex + fixed2(_MainTex_TexelSize.x, 0)).rgb;
            float3 E = tex2D(_MainTex, IN.uv_MainTex - fixed2(_MainTex_TexelSize.x, 0)).rgb;
            
            float C_lum = LUM(C);
            float N_lum = LUM(N);
            float S_lum = LUM(S);
            float W_lum = LUM(W);
            float E_lum = LUM(E);

            float L_lum = saturate(N_lum + S_lum + W_lum + E_lum - 4 * C_lum);

            L_lum = step(_MapThreshold, L_lum);

            o.Albedo = C.rgb + float4(L_lum, L_lum, L_lum, 1);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = C.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
