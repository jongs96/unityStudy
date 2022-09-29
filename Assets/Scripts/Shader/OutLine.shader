Shader "Study/OutLine"
{
    Properties
    {        
        _OutlineColor("Outline Color", Color) = (1,0,0,1)
        _OutlineWidth("Outline Width", Range(0.0, 0.05)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" "IgnoreProjector" = "True"}
        Pass
        {
            Zwrite off
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * _OutlineWidth;
                return o;
            }

            fixed4 frag (v2f i) : COLOR
            {                
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
