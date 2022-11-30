/// <summary>
/// Author: AkilarLiao
/// Date: 2022/11/17
/// Desc: For test dynamic add pass architecture purpose
/// architecture
/// </summary>
Shader "TestOutline"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque" 
            "IgnoreProjector" = "True"
        }
        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}
            HLSLPROGRAM
            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram
            #include "TestOutlineImpl.hlsl"
            ENDHLSL
        }
        Pass
        {
            Name "Outline"
            Tags{"LightMode" = "OutLine"}
            Cull front
            HLSLPROGRAM
            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram
            #define PROCESS_OUTLINE
            #include "TestOutlineImpl.hlsl"
            ENDHLSL
        }
    }
}
