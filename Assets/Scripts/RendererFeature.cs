using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class RendererFeature : ScriptableRendererFeature
{
    [SerializeField] RendererFeatureSettings settings;
    RendererFeaturePass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new RendererFeaturePass(settings);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

        // You can request URP color texture and depth buffer as inputs by uncommenting the line below,
        // URP will ensure copies of these resources are available for sampling before executing the render pass.
        // Only uncomment it if necessary, it will have a performance impact, especially on mobiles and other TBDR GPUs where it will break render passes.
        //m_ScriptablePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);

        // You can request URP to render to an intermediate texture by uncommenting the line below.
        // Use this option for passes that do not support rendering directly to the backbuffer.
        // Only uncomment it if necessary, it will have a performance impact, especially on mobiles and other TBDR GPUs where it will break render passes.
        //m_ScriptablePass.requiresIntermediateTexture = true;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptablePass);
    }

    // Use this class to pass around settings from the feature to the pass
    [Serializable]
    public class RendererFeatureSettings
    {
        public Material material;
    }
    //rendererFeatureSetting not important for now
    class RendererFeaturePass : ScriptableRenderPass
    {
        readonly RendererFeatureSettings settings;

        public RendererFeaturePass(RendererFeatureSettings settings)
        {
            this.settings = settings;
        }
        //this sets the setting from settings above to each pass

        
        // This class stores the data needed by the RenderGraph pass.
        // It is passed as a parameter to the delegate function that executes the RenderGraph pass.
        private class PassData
        {
            public TextureHandle inputColor;
            public Material material;
            public TextureHandle outputTexture;
        }

        // This static method is passed as the RenderFunc delegate to the RenderGraph render pass.
        // It is used to execute draw commands.
        static void ExecutePass(PassData data, RasterGraphContext context)
        {
            Debug.Log("Render pass executed");

            if (data.material == null)
            {
                Debug.LogWarning("No material assigned to the pass.");
                return;
            }

            // Blitter.BlitTexture will bind the source texture to the built-in "_BlitTexture" property.
            
            // Your shader should sample from "_BlitTexture" when using this helper.
            Blitter.BlitTexture(context.cmd, data.inputColor, new Vector4(1, 1, 0, 0), data.material, 0);
        }

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            const string passName = "Render Custom Pass";

            // This adds a raster render pass to the graph, specifying the name and the data type that will be passed to the ExecutePass function.
            using (var builder = renderGraph.AddRasterRenderPass<PassData>(passName, out var passData)) //out var passData nghĩa là 1 instance của PassData
            {
                // Use this scope to set the required inputs and outputs of the pass and to
                // setup the passData with the required properties needed at pass execution time.

                // Make use of frameData to access resources and camera data through the dedicated containers.
                // Eg:
                UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
                UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();

                // Setup pass inputs and outputs through the builder interface.
                passData.inputColor = resourceData.activeColorTexture;
                builder.UseTexture(passData.inputColor, 0);

                passData.material = settings.material;
                TextureHandle destination = UniversalRenderer.CreateRenderGraphTexture( //set output là những gì hiểm thị trên màn hình
                    renderGraph,
                    cameraData.cameraTargetDescriptor,
                    "Destination Texture",
                    false);

                passData.outputTexture = destination;

                // This sets the render target of the pass to a newly created texture.
                builder.SetRenderAttachment(passData.outputTexture, 0);

                // Assigns the ExecutePass function to the render pass delegate. This will be called by the render graph when executing the pass.
                builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context));
                
            }
        }
    }
}  
