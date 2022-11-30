using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public interface IAddPassInterface
{
    void OnAddPass(ScriptableRenderer renderer,
        ref RenderingData renderingData);
};

public class DynamicAddPassFeature : ScriptableRendererFeature
{
    public static bool AppendAddPassInterfaces(
            IAddPassInterface theInterface)
    {
        if ((theInterface == null) ||
            (ms_addPassInterfaces.Find(theInterface) != null))
            return false;
        ms_addPassInterfaces.AddLast(theInterface);
        return true;
    }
    public static bool RemoveAddPassInterfaces(
        IAddPassInterface theInterface)
    {
        return ms_addPassInterfaces.Remove(theInterface);
    }
    public override void Create()
    {   
    }
    public override void AddRenderPasses(ScriptableRenderer renderer,
            ref RenderingData renderingData)
    {
        var element = ms_addPassInterfaces.GetEnumerator();
        while (element.MoveNext())
            element.Current.OnAddPass(renderer, ref renderingData);
        element.Dispose();
    }
    private static LinkedList<IAddPassInterface> ms_addPassInterfaces =
        new LinkedList<IAddPassInterface>();
}
