Shader "Hidden/BlackHole"
{
    Properties
    {
        [HideInInspector]   _MainTex ("Texture", 2D) = "white" {}
        _Rad("Radius",Float) =1
        _CircleSize("Circle size",Range(0,1)) =.1
        _Power("Power",Range(0,2)) =.1
        _Ratio("Ratio",Float) =1
    }
    SubShader
    {      
        Tags { "Queue" = "Transparent" }
         ZTest Off
       //  `   ZWrite Off
        GrabPass{}
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
                float4 grapPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.grapPos = ComputeGrabScreenPos(o.vertex);    
                return o;
            }

            sampler2D _MainTex,_GrabTexture;
            float _Rad;
            float _Ratio;
            float _Distance,_CircleSize,_Power;
            fixed4 frag (v2f i) : SV_Target
            {                            
                float2 offset =i.grapPos.xy; // We shift our pixel to the desired position
                _Distance = 1000;
                  float2 ratio = {_Ratio,1}; // determines the aspect ratio
                float rad = length(((i.uv-.5))); // the distance from the conventional "center" of the screen.

                float blink =  (sin(_Time.y*20 ) * .01 ) *_Power;                
             //   rad+= blink;
                float deformation = 1/pow(rad*pow(_Distance,0.5),2)*_Rad*2;
                deformation*= _Power *(1- rad*2);
                offset =offset*(1-deformation);
                
                
                half4 res = tex2Dproj(_GrabTexture,float4(offset.xy,i.grapPos.zw));
                //if (rad*_Distance<pow(2*_Rad/_Distance,0.5)*_Distance) {res.g+=0.2;} // verification of compliance with the Einstein radius
                float circle =1-step(rad,_CircleSize);
                
                float4 blackHole = res * circle;
                return blackHole;//; res;// float4(offset.xy,i.grapPos.z,i.grapPos.w*(1-circle));

            }
            ENDCG
        }
    }
}
