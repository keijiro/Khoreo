Shader "Hidden/Khoreo/Webcam/Screen"
{
    Properties
    {
        _MainTex("", 2D) = "black" {}
    }

    HLSLINCLUDE

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/jp.keijiro.noiseshader/Shader/SimplexNoise2D.hlsl"

    float min_xy(float2 v) { return min(v.x, v.y); }
    float max_xy(float2 v) { return max(v.x, v.y); }

    sampler2D _MainTex;
    float2 _FilterParams;
    float4 _Time;

    void Vertex(
        float4 position : POSITION,
        float2 texcoord : TEXCOORD0,
        out float4 outPosition : SV_Position,
        out float2 outTexcoord : TEXCOORD0
    )
    {
        outPosition.x =  position.x * 2 - 1;
        outPosition.y = -position.y * 2 + 1;
        outPosition.zw = float2(1, 1);
        outTexcoord = texcoord;
    }

    void Fragment(
        float4 position : SV_Position,
        float2 texcoord : TEXCOORD0,
        out float4 outColor : SV_Target
    )
    {
        float contours, grid_lines, edge_lines, edge_mask;

        float2 fw = fwidth(texcoord);

        // Contour lines (edge detection effect)
        {
            float2 uv = texcoord;

            float2 uv0 = uv;
            float2 uv1 = uv + fw;
            float2 uv2 = uv + float2(fw.x, 0);
            float2 uv3 = uv + float2(0, fw.y);

            float3 c0 = tex2D(_MainTex, uv0).rgb;
            float3 c1 = tex2D(_MainTex, uv1).rgb;
            float3 c2 = tex2D(_MainTex, uv2).rgb;
            float3 c3 = tex2D(_MainTex, uv3).rgb;

            // Roberts cross operator
            float3 g1 = c1 - c0.rgb;
            float3 g2 = c3 - c2;
            float g = sqrt(dot(g1, g1) + dot(g2, g2));

            // Threshold parameters
            float th1 = _FilterParams.x;
            float th2 = th1 + 1 - _FilterParams.y;
            contours = smoothstep(th1, th2, g);
        }

        // Grid lines
        {
            float2 repeat = float2(8, 4);
            float2 uv = frac(texcoord * repeat + 0.5);

            float2 p = min(uv, 1 - uv);
            float2 p_div_fw = p / (fw * repeat);

            float cut = all(p < 0.2);

            grid_lines = max_xy(saturate(1 - p_div_fw)) * cut;
        }

        // Outer edge
        {
            float2 p = min(texcoord, 1 - texcoord);
            float2 p_div_fw = p / fw;

            float cut = all(abs(p - 0.5) > 0.3);

            edge_mask  = min_xy(saturate(    p_div_fw));
            edge_lines = max_xy(saturate(2 - p_div_fw)) * cut;
        }

        // Noise
        {
            uint seed = _Time.y * 60;

            float r1 = GenerateHashedRandomFloat(seed);
            float r2 = GenerateHashedRandomFloat(seed + 0xcafebabe);
            float r3 = GenerateHashedRandomFloat(seed + 0xdeadbeef);

            contours *= lerp(0.5, 1, r1);
            grid_lines *= r2;
            edge_lines *= r3;
        }

        // Composition
        outColor.rgb = (contours + grid_lines + edge_lines) * edge_mask;
        outColor.a = 1;
    }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDHLSL
        }
    }
}
