Shader "UshiSoft/DashedLineVertical"
{
    Properties
    {
        // 繰り返し頻度
        _Repeat ("Repeat", Float) = 2
        // 移動速度
        _Speed ("Speed", Float) = 30
    }
    SubShader
    {
        Tags
        {
            // 描写順
            "Queue"="Transparent"
            // わからん
            "IgnoreProjector"="True"
            // 透過する
            "RenderType"="Transparent"
            // インスペクターのプレビューをどのように表示するか
            "PreviewType"="Plane"
            // スプライトを使う場合に必要?
            "CanUseSpriteAtlas"="True"
        }

        // 裏面も描写
        Cull Off
        // ライトに影響されなくなる(頂点カラーがそのまま出る)
        Lighting Off
        // 深度バッファへの書き込みオフ
        // 深度バッファってなんですか
        ZWrite Off
        // 透過方法
        // よくわからん
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color: COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color: COLOR;
                float2 uv : TEXCOORD0;
                float3 worldPos : WORLD_POS;
            };

            // プロパティの値はここで定義しないと使えない
            float _Repeat;
            float _Speed;
            float _Vertical;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                // 頂点をワールド座標に変換
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return step(0.5, frac(_Time.x * _Speed + i.worldPos.y * _Repeat)) * i.color;
            }
            ENDCG
        }
    }
}
