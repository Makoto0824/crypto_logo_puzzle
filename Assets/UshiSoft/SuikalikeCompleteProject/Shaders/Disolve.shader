Shader "UshiSoft/Disolve"
{
    Properties
    {
        // ���C���e�N�X�`��
        // _MainTex�Ƃ������O����Ȃ��Ƃ���
        // NoScaleOffset��UV Scale/Offset���\������Ȃ��Ȃ�
        [NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        // �f�B�]���u�̌��ƂȂ�m�C�Y�e�N�X�`��
        [NoScaleOffset]
        _NoiseTexture ("NoiseTexture", 2D) = "white" {}
        // �������l
        // 1�Ŋ��S�ɓ���
        _Threshold ("Threshold", Range(0, 1)) = 0
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
            };

            // �v���p�e�B�̒l�͂����Œ�`���Ȃ��Ǝg���Ȃ�
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
                // �m�C�Y�e�N�X�`���̐F
                fixed4 c = tex2D(_NoiseTexture, i.uv);
                // �������l�𒴂������\���ɂ���
                // �m�C�Y�e�N�X�`���̓O���[�X�P�[���Ȃ̂ł����ꂩ�̃J���[�`�����l��������΂���
                if (c.r < _Threshold)
                {
                    discard;
                }
                // ����ȊO�̓e�N�X�`���̐F��Ԃ�
                return tex2D(_MainTex, i.uv) * i.color;
            }
            ENDCG
        }
    }
}
