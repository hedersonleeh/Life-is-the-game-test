Shader "Unlit/OutLineShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        

        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness("Outline Thickness", Range(0,.1)) = 0.03
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
            pass
            {
                cull front
                CGPROGRAM
                #include "UnityCG.cginc"

                #pragma vertex vert
                #pragma fragment frag


                struct appdata
                {
                    float4 vertex  : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 position : SV_POSITION;
                    float3 normal : TEXCOORD0;
                };

                //color of the outline
                fixed4 _OutlineColor;
                //thickness of the outline
                float _OutlineThickness;

                v2f vert(appdata v)
                {

                    v2f o;
                    float3 normal = normalize(v.normal);
                    //normal.z = 0;
                    float3 outlineOffset = normal * _OutlineThickness * abs(sin(_Time.y *5));

                    float3 position = v.vertex + outlineOffset;
                    o.normal = v.normal;
                    o.position = UnityObjectToClipPos(position);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {

                    return _OutlineColor;
                }
                ENDCG
            }
           


        }
            //fallback which adds stuff we didn't implement like shadows and meta passes
                    FallBack "Standard"
}
