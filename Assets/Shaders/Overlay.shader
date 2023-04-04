Shader "Custom/Overlay"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LeftFluidTex ("Left Fluid Texture", 2D) = "white" {}
        _RightFluidTex ("Right Fluid Texture", 2D) = "white" {}
        _LeftOrRight ("Left or Right", Float) = 0.0
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
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _LeftFluidTex;
            sampler2D _RightFluidTex;
            sampler2D _CameraDepthTexture;
            float _LeftOrRight;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float4 left_fluid_col = tex2D(_LeftFluidTex, i.uv);
                float4 right_fluid_col = tex2D(_RightFluidTex, i.uv);
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
                float4 fluid_col = _LeftOrRight ? left_fluid_col : right_fluid_col;
                if (depth < fluid_col.a)
                {
                    col.rgb = col.rgb * fluid_col.rgb;
                }
                return col;
            }
            ENDCG
        }
    }
}
