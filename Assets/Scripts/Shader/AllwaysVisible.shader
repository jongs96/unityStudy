Shader "Study/AllwaysVisible"
{
    Properties
    {
        _SilhouetteColor("Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent+100"}
        LOD 100

        Pass
        {               
            ZWrite off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;                
            };

            struct v2f
            {                
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);                
                return o;
            }

            float4 _SilhouetteColor;

            fixed4 frag (v2f i) : SV_Target
            {                
                return _SilhouetteColor;
            }
            ENDCG
        }
    }
}
