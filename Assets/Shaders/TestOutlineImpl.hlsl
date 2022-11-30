/// <summary>
/// Author: AkilarLiao
/// Date: 2022/11/17
/// Desc: For test dynamic add pass architecture purpose
/// architecture
/// </summary>
#ifndef TEST_OUTLINE_IMPL_INCLUDED
#define TEST_OUTLINE_IMPL_INCLUDED
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct VertexInput
{
    float4 positionOS	: POSITION;
#ifdef PROCESS_OUTLINE
    float3 normalOS : NORMAL;
#endif //PROCESS_OUTLINE
};

struct VertexOutput
{
    float4 positionCS	: SV_POSITION;
};

VertexOutput VertexProgram(VertexInput input)
{
    VertexOutput output = (VertexOutput)0;
#ifdef PROCESS_OUTLINE
    real3 normal = normalize(input.normalOS);
    float3 positionOS = input.positionOS.xyz;
    positionOS += normal * 0.03;
    output.positionCS = TransformObjectToHClip(positionOS);
#else
    output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
#endif //PROCESS_OUTLINE
    return output;
}

half4 FragmentProgram(VertexOutput input) : SV_Target
{
#ifdef PROCESS_OUTLINE
    return half4(0.0, 0.0, 0.0, 1.0);
#else
    return half4(1.0, 0.0, 0.0, 1.0);
#endif //PROCESS_OUTLINE
}

#endif //TEST_OUTLINE_IMPL_INCLUDED