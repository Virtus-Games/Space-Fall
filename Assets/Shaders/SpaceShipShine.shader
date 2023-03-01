Shader "Custom/SpaceShip"
{
    Properties
    {
        _MainTexture("Manin Texture",2D) = "Green"
        _ShineTexture ("Shine Texture", 2D) = "white" {}
        _ShineColor("ShineColor",Color) = (1,1,1,1)
        _ShineSpeed("ShineSpeed",float)=1

        [Toggle] _LinearShine("Linear Shine",Float) =0
        [Toggle] _MixShine("Mix Shine",Float) = 0
        [Toggle] _HideShine("Hide Shine",Float) = 0
 
         [Enum(UnityEngine.Rendering.BlendMode)]
         _SrcFactor("Src Factor",Float) = 0

          [Enum(UnityEngine.Rendering.BlendMode)]
         _DstFactor("DstFactor",Float) = 0
          
           [Enum(UnityEngine.Rendering.BlendOp)]
           _Opp("Opp",Float) =0

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend[_SrcFactor][_DstFactor]
        BlendOp[_Opp]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _LINEARSHINE_ON _MIXSHINE_ON _HIDESHINE_ON
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uvs : TEXCOORD0;                 
                float4 vertex : SV_POSITION;
                float4 screenSpaceCoord : TEXCOORD1;
            };

            sampler2D _ShineTexture;
            float4 _ShineTexture_ST;

             sampler2D _MainTexture;
            float4 _MainTexture_ST;
            float _ShineSpeed;
            float4 _ShineColor;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvs.xy = TRANSFORM_TEX(v.uv, _MainTexture);
                o.uvs.zw = TRANSFORM_TEX(v.uv, _ShineTexture);
                o.screenSpaceCoord = ComputeScreenPos(o.vertex);
              
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                #if _LINEARSHINE_ON     

                float2 screenSpaceUV = i.screenSpaceCoord.xy/i.screenSpaceCoord.w;
                fixed4 maintex = tex2D(_MainTexture, i.uvs.xy);
                fixed4 shineTex = tex2D(_ShineTexture,screenSpaceUV + float2(_Time.x*_ShineSpeed,0));              
                fixed3 color = maintex.rgb +shineTex.rgb*shineTex.a*_ShineColor.rgb;
                         
                return fixed4(color,1);                 

               #elif _MIXSHINE_ON
               fixed4 maintex = tex2D(_MainTexture, i.uvs.xy);
                fixed4 shineTex = tex2D(_ShineTexture,i.uvs.zw + float2(_Time.x*_ShineSpeed,0));              
                fixed3 color = maintex.rgb +shineTex.rgb*shineTex.a*_ShineColor.rgb;
                return fixed4(color,1);

                #elif _HIDESHINE_ON
                 fixed4 maintex = tex2D(_MainTexture, i.uvs.xy);
                            
                fixed3 color = maintex.rgb;
                return fixed4(color,1);
             
                #endif

                
                
            }
            ENDCG
        }
    }
}
