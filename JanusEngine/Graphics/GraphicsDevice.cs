using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using JanusEngine.Trace;

namespace JanusEngine.Graphics
{
    public class GraphicsDevice
    {
        public SharpDX.Direct3D11.Device1 Device { get; private set; }
        public DeviceContext1 ImmediateContext { get; private set; }

        private ISwapchainFactory m_swapChainFactory;


        private SwapChain1 m_swapChain;
        private RenderTargetView m_renderTargetView;
        private DepthStencilView m_depthStencilView;
        private Texture2D m_depthStencilBuffer;

        public GraphicsDevice(ISwapchainFactory swapchainFactory)
        {
            m_swapChainFactory = swapchainFactory;

            DeviceCreationFlags flags = DeviceCreationFlags.BgraSupport;

            flags |= DeviceCreationFlags.Debug;

            FeatureLevel[] featureLevels =
            {
                FeatureLevel.Level_11_1,
                FeatureLevel.Level_11_0,
                FeatureLevel.Level_10_1,
                FeatureLevel.Level_10_0
            };

            var device = new SharpDX.Direct3D11.Device(DriverType.Hardware, flags, featureLevels);

            Device = ComObject.As<SharpDX.Direct3D11.Device1>(device);
            Logger.Log.Info(string.Format("Created device with feature level: {0}", device.FeatureLevel));

            ImmediateContext = Device.ImmediateContext1;

            m_swapChainFactory.Resized += SwapchainFactory_Resized;
            m_swapChainFactory.Resizing += SwapChainFactory_Resizing;

            Reset();
        }

        public void Reset()
        {
            DestroySwapChainDependentResources();
            CreateOrResizeSwapChainDependentResources();
        }

        private void CreateOrResizeSwapChainDependentResources()
        {
            if (m_swapChain == null)
            {
                var desc = new SwapChainDescription1
                {
                    AlphaMode = AlphaMode.Premultiplied,
                    BufferCount = 2,
                    Flags = SwapChainFlags.AllowModeSwitch,
                    Format = Format.B8G8R8A8_UNorm,
                    SampleDescription = new SampleDescription(1, 0),
                    Stereo = false,
                    SwapEffect = SwapEffect.FlipSequential,
                    Usage = Usage.BackBuffer,
                };

                var dxgiDevice = ComObject.As<SharpDX.DXGI.Device1>(Device);
                m_swapChain = m_swapChainFactory.CreateSwapChain(dxgiDevice, desc);
            }

            var swapChainDescription = m_swapChain.Description1;

            using (var backbuffer = m_swapChain.GetBackBuffer<Texture2D>(0))
            {
                m_renderTargetView = new RenderTargetView(Device, backbuffer);
            }

            var depthStencilDesc = new Texture2DDescription
            {
                Format = Format.D24_UNorm_S8_UInt,
                ArraySize = 1,
                Width = swapChainDescription.Width,
                Height = swapChainDescription.Height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                BindFlags = BindFlags.DepthStencil
            };

            m_depthStencilBuffer = new Texture2D(Device, depthStencilDesc);
            m_depthStencilView = new DepthStencilView(Device, m_depthStencilBuffer);

            ImmediateContext.OutputMerger.SetRenderTargets(m_renderTargetView);
        }
            

        private void DestroySwapChainDependentResources()
        {
            m_renderTargetView = null;
            m_depthStencilView = null;
            m_depthStencilBuffer = null;
        }

        private void SwapChainFactory_Resizing()
        {
            DestroySwapChainDependentResources();
        }

        private void SwapchainFactory_Resized()
        {
            CreateOrResizeSwapChainDependentResources();
        }

        public void Clear()
        {
            ImmediateContext.OutputMerger.SetRenderTargets(m_renderTargetView);
            ImmediateContext.ClearRenderTargetView(m_renderTargetView, new Color4(0.0f, 0.5f, 0.5f, 1.0f));
            ImmediateContext.ClearDepthStencilView(m_depthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1, 0);
        }


        public void Present()
        {
            m_swapChain.Present(1, PresentFlags.None);
        }
    }
}
