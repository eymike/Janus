using System;

using SharpDX.DXGI;

namespace JanusEngine.Graphics
{
    public interface ISwapchainFactory
    {
        SwapChain1 CreateSwapChain(Device device, SwapChainDescription1 description);

        event Action Resizing;
        event Action Resized;
    }
}
