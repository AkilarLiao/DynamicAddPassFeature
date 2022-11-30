using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

[ExecuteAlways]
public class DynamicOutlinePassTest : MonoBehaviour, IAddPassInterface
{
    private void OnEnable()
    {
        DynamicAddPassFeature.AppendAddPassInterfaces(this);
        m_drawOutLinePass = new DrawOutLinePass();
    }
    private void OnDisable()
    {
        DynamicAddPassFeature.RemoveAddPassInterfaces(this);
    }
    void IAddPassInterface.OnAddPass(ScriptableRenderer renderer,
        ref RenderingData renderingData)
    {
        if (!enabled)
            return;
        renderer.EnqueuePass(m_drawOutLinePass);
    }

    private DrawOutLinePass m_drawOutLinePass = null;

    class DrawOutLinePass : ScriptableRenderPass
    {
        public DrawOutLinePass()
        {
            renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
            m_filteringSettings = new FilteringSettings(RenderQueueRange.opaque,
                ~0);
            m_profilingSampler = new ProfilingSampler(m_profilerTag);
            m_shaderTagId = new ShaderTagId("OutLine");
        }
        
        public override void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            CommandBuffer command = CommandBufferPool.Get(m_profilerTag);
            using (new ProfilingScope(command, m_profilingSampler))
            {
                context.ExecuteCommandBuffer(command);
                command.Clear();
                var sortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
                var drawSettings = CreateDrawingSettings(m_shaderTagId,
                    ref renderingData, sortFlags);
                context.DrawRenderers(renderingData.cullResults, ref drawSettings,
                    ref m_filteringSettings);
            }
            context.ExecuteCommandBuffer(command);
            CommandBufferPool.Release(command);
        }
        private FilteringSettings m_filteringSettings;
        private const string m_profilerTag = "Render Opaque outLines";
        private ShaderTagId m_shaderTagId = ShaderTagId.none;
        private ProfilingSampler m_profilingSampler = null;
    }
}
