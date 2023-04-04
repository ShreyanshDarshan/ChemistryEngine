Shader "Custom/CompositeFluid"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DepthTex ("Main Depth Texture", 2D) = "white" {}
        _BackDepthTex ("Back Depth Texture", 2D) = "white" {}
        _SurfaceTex ("Surface Texture", 2D) = "white" {}
        _SurfaceDepthTex ("Surface Depth Texture", 2D) = "white" {}
        _CubeDepthTex ("Cube Depth Texture", 2D) = "white" {}
        _CubeBackDepthTex ("Cube Back Depth Texture", 2D) = "white" {}
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
            sampler2D _DepthTex;
            sampler2D _SurfaceTex;
            sampler2D _BackDepthTex;
            sampler2D _SurfaceDepthTex;
            sampler2D _CubeDepthTex;
            sampler2D _CubeBackDepthTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 body_depth = tex2D(_DepthTex, i.uv);
                float4 back_depth = tex2D(_BackDepthTex, i.uv);
                float4 surface_depth = tex2D(_SurfaceDepthTex, i.uv);
                float4 cube_depth = tex2D(_CubeDepthTex, i.uv);
                float4 cube_back_depth = tex2D(_CubeBackDepthTex, i.uv);
                bool in_front = back_depth.r <= surface_depth.r; 
                bool behind = body_depth.r >= surface_depth.r;
                bool not_sky_surface = surface_depth.r > 0.0;
                bool not_sky_body = body_depth > 0.0;
                bool not_sky_back = body_depth > 0.0;
                bool not_sky_cube = cube_depth > 0.0;

                float4 blank = float4(1, 1, 1, 0);
                fixed4 body_col = float4(tex2D(_MainTex, i.uv).rgb, body_depth.r);

                if (!not_sky_surface && !not_sky_body) {
                    return blank;
                }
                
                if (!not_sky_back && !not_sky_body && not_sky_surface) {
                    return blank;
                }

                if (!not_sky_surface) {
                    if (not_sky_cube && cube_depth.r > body_depth.r && cube_back_depth.r < back_depth.r) {
                        return blank;
                    }
                    return body_col;
                }


                if (!in_front && behind) {
                    return blank;
                }

                if (in_front && behind) {
                    return float4(tex2D(_SurfaceTex, i.uv).rgb, surface_depth.r);
                }
                
                return body_col;
            }
            ENDCG
        }
    }
}
