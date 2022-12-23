Shader "Wonnasimth_Object/Grays"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Light("Light", Color) = (0.3, 0.59, 0.11, 1)
    }
    SubShader
    {
        // Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200
        CGPROGRAM
        #pragma surface surf Lambert alpha
        
        sampler2D _MainTex;
        fixed3 _Light;
        
        struct Input 
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = dot(c.rgb, float3(_Light.r, _Light.g, _Light.b));
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
