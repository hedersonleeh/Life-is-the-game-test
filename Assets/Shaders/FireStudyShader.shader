Shader "Unlit/FireStudyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowMap ("FlowMap", 2D) = "white" {}
        _Firecolor ("Color", Color) = (1,1,1,1)
        [HDR]_Firecolor2 ("Color2", Color) = (1,1,1,1)
        _Firecolor3  ("Color3", Color) = (1,1,1,1)
        [MaterialToggle]  _FlipUV  ("FlipUV", Float) = 1
        _Offset  ("Offset", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {        
            Blend One OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex,_FlowMap;
            float4 _MainTex_ST;
            float4 _Firecolor;
            float4 _Firecolor2,_Firecolor3,_Offset;
            float _FTime,_FlipUV;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            float sdUnevenCapsule( float2 p, float r1, float r2, float h )
            {
                p.x = abs(p.x);
                float b = (r1-r2)/h;
                float a = sqrt(1.0-b*b);
                float k = dot(p,float2(-b,a));
                if( k < 0.0 ) return length(p) - r1;
                if( k > a*h ) return length(p-float2(0.0,h)) - r2;
                return dot(p, float2(a,b) ) - r1;
            }
            float4 scrollFlowMap(in float2 uv)
            {
                
                uv.y -=_Time.y;
                
                fixed4 flowMap = tex2D(_FlowMap,uv);
                return flowMap;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                if(_FlipUV)
                    i.uv.xy = i.uv.yx;

                i.uv+=_Offset.xy;
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 flow =scrollFlowMap(i.uv);
                float l = 1-length((i.uv -.5) *2);
                float q=pow(i.uv.y,2);
                float2 dUv =i.uv-float2(.5,.35);
                dUv.y += sin(flow.y)*10 * pow(i.uv.y,2);
                float sdf=1-sdUnevenCapsule((dUv),.2,.03,1);
                float drople = smoothstep(.98,1.1 ,sdf);
                float sd =floor(drople*3)/3 ;
                clip(sd -.01);
                float3 sdColor =sd;
                if(sd <=0.33)
                {
                    return _Firecolor ;
                }
                if(sd <= 0.66 )
                {
                    if(i.uv.x <0.5)
                    return _Firecolor2 ;
                    

                }
                // if(sd <= .99)
                // {
                    return lerp(_Firecolor3,_Firecolor,smoothstep(0.1,.35,i.uv.y));

                // }
                // if(sd <= 1.5)
                // {
                //     return 1;lerp(_Firecolor,_Firecolor3,smoothstep(0.2,.35,i.uv.y));

                // }
                //  return 1;
                return l-0.5;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
