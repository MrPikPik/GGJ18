Shader "Custom/WorldAlignedCurved" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Scale("Texture Scale", Float) = 1.0
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		sampler2D _MainTex;
		fixed4 _Color;
		float _Scale;
		float curv = 0.005f;
		float curv2 = 0.0025f;

		struct Input {
			float3 worldNormal;
			float3 worldPos;
		};

		void vert(inout appdata_full v) {
			float4 vv = mul(unity_ObjectToWorld, v.vertex);

			

			vv.xyz -= _WorldSpaceCameraPos.xyz;
			vv = float4(0.0f, (vv.z * vv.z) * -curv2 + (vv.x * vv.x) * -curv, 0.0f, 0.0f);

			v.vertex += mul(unity_WorldToObject, vv);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			float2 UV;
			fixed4 c;

			if (abs(IN.worldNormal.x)>0.5) {
				UV = IN.worldPos.yz; // side
				c = tex2D(_MainTex, UV* _Scale); // use WALLSIDE texture
			} else if (abs(IN.worldNormal.z)>0.5) {
				UV = IN.worldPos.xy; // front
				c = tex2D(_MainTex, UV* _Scale); // use WALL texture
			} else {
				UV = IN.worldPos.xz; // top
				c = tex2D(_MainTex, UV* _Scale); // use FLR texture
			}

			o.Albedo = c.rgb * _Color;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "VertexLit"
}