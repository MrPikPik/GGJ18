Shader "Custom/CurvedWorld" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		uniform sampler2D _MainTex;
		float curv = 0.005f;
		float curv2 = 0.0025f;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void vert(inout appdata_full v){
			float4 vv = mul(unity_ObjectToWorld, v.vertex);



			vv.xyz -= _WorldSpaceCameraPos.xyz;
			vv = float4(0.0f, (vv.z * vv.z) * -curv2 + (vv.x * vv.x) * -curv, 0.0f, 0.0f);

			v.vertex += mul(unity_WorldToObject, vv);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}