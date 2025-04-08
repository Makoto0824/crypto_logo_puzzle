Shader "UshiSoft/Disolve"
{
    Properties
    {
        // メインテクスチャ
        // _MainTexという名前じゃないとだめ
        // NoScaleOffsetでUV Scale/Offsetが表示されなくなる
        [NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        // ディゾルブの元となるノイズテクスチャ
        [NoScaleOffset]
        _NoiseTexture ("NoiseTexture", 2D) = "white" {}
        // しきい値
        // 1で完全に透過
        _Threshold ("Threshold", Range(0, 1)) = 0
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
            };

            // プロパティの値はここで定義しないと使えない
            sampler2D _MainTex;
            sampler2D _NoiseTexture;
            float _Threshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ノイズテクスチャの色
                fixed4 c = tex2D(_NoiseTexture, i.uv);
                // しきい値を超えたら非表示にする
                // ノイズテクスチャはグレースケールなのでいずれかのカラーチャンネルを見ればおｋ
                if (c.r < _Threshold)
                {
                    discard;
                }
                // それ以外はテクスチャの色を返す
                return tex2D(_MainTex, i.uv) * i.color;
            }
            ENDCG
        }
    }
}
