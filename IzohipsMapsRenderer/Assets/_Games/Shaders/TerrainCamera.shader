Shader "Wonnasimth_Camera/TerrainCamera"
{
    Properties
    {
        _RampTex ("Texture", 2D) = "white" {}
        _MinY ("MinY", float) = 0
        _MaxY ("MaxY", float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

        	    // World position
        	    float3 wPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _RampTex;
            float _MinY;
            float _MaxY;
            
            fixed4 frag (v2f i) : SV_Target
            {
            	// Not needed
            	// sample the texture
            	//fixed4 col = tex2D(_MainTex, i.uv);
            	// apply fog
            	//UNITY_APPLY_FOG(i.fogCoord, col);

            	// i.wPos.y: [_MinY, _MaxY]
            	// u:        [0,     1]
            	fixed u = (i.wPos.y - _MinY) / (_MaxY - _MinY);
            	u = saturate(u);

            	// Posterize
            	fixed4 col = tex2D(_RampTex, fixed2(u, 0.5));

            	return col;
            }
            ENDCG
        }
    }
}
