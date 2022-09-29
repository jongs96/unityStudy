
Shader"Custom/Cutout" 
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
		_TintColor("TintColor", Color) = (0, 0, 0, 1)
		_Cutoff("Cutoff", Range(0, 360)) = 0.0
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{ 

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform fixed4 _TintColor;
			uniform float _Cutoff;

			fixed4 frag(v2f_img i) : COLOR
			{
				fixed4 color = tex2D(_MainTex, i.uv);

				float rads = _Cutoff * 0.0174532925;

				float2 position = float2 (sin(rads), cos(rads));
				float2 uvPosition = float2 (i.uv.x + (i.uv.x - 1), i.uv.y + (i.uv.y - 1));
				position *= sqrt(uvPosition.x*uvPosition.x + uvPosition.y*uvPosition.y);

				if (_Cutoff >= 180 && i.uv.x > 0.5) 
				{
					color.a = 0;
				} 
				else if(i.uv.x > 0.5 && uvPosition.y > position.y) 
				{
					color.a = 0;
				} 
				else if(_Cutoff > 180 && i.uv.x <= 0.5 && uvPosition.y < position.y) 
				{
					color.a = 0;
				}
				return color * _TintColor;
			}
			ENDCG
		} //endpass
	}
	FallBack "Diffuse"
}