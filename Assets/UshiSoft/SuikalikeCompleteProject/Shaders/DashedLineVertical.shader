Shader "UshiSoft/DashedLineVertical"
{
    Properties
    {
        // �J��Ԃ��p�x
        _Repeat ("Repeat", Float) = 2
        // �ړ����x
        _Speed ("Speed", Float) = 30
    }
    SubShader
    {
        Tags
        {
            // �`�ʏ�
            "Queue"="Transparent"
            // �킩���
            "IgnoreProjector"="True"
            // ���߂���
            "RenderType"="Transparent"
            // �C���X�y�N�^�[�̃v���r���[���ǂ̂悤�ɕ\�����邩
            "PreviewType"="Plane"
            // �X�v���C�g���g���ꍇ�ɕK�v?
            "CanUseSpriteAtlas"="True"
        }

        // ���ʂ��`��
        Cull Off
        // ���C�g�ɉe������Ȃ��Ȃ�(���_�J���[�����̂܂܏o��)
        Lighting Off
        // �[�x�o�b�t�@�ւ̏������݃I�t
        // �[�x�o�b�t�@���ĂȂ�ł���
        ZWrite Off
        // ���ߕ��@
        // �悭�킩���
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

            // �v���p�e�B�̒l�͂����Œ�`���Ȃ��Ǝg���Ȃ�
            float _Repeat;
            float _Speed;
            float _Vertical;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                // ���_�����[���h���W�ɕϊ�
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
