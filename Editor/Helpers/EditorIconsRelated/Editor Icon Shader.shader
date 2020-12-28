Shader "Custom/EditorIcon"
{
	Properties
	{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
	}
    SubShader
	{
        Blend SrcAlpha Zero
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
				};

                sampler2D _MainTex;
                float4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = _Color;
					col.a *= tex2D(_MainTex, i.uv).a;
					return col;
				}
			ENDCG
		}
	}
}