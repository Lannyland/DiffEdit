Shader "Custom/LannyShader"
{
	Properties
	{
		_MainTex ("Base (RGB) Mask (A)", 2D) = "white" {}
		_Color ("Tint Color", Color) = (1,1,1,1)
		_TerrainTex ("Terrain (RGB)", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		
		LOD 200
		Cull Off
		Lighting Off
		ZWrite On
		Fog { Mode Off }
		ColorMask RGB
		Blend Off
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _TerrainTex;
			fixed4 _Color;
			
			struct appdata_t
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
					
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				
				half4 col = ( 0.5 * tex2D(_MainTex, i.texcoord) + 0.5 * tex2D(_TerrainTex, i.texcoord) ) * i.color ;
				
				return half4( lerp(col.rgb, col.rgb * _Color.rgb, col.a), col.a );
			}
			ENDCG
		}
	}
	
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		
		LOD 100
		Cull Off
		Lighting Off
		ZWrite On
		Fog { Mode Off }
		ColorMask RGB
		AlphaTest Greater .01
		Blend Off
		
		Pass
		{
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
			
			SetTexture [_MainTex]
			{
				ConstantColor [_Color]
				Combine Previous * Constant
			}
		}
	}
}