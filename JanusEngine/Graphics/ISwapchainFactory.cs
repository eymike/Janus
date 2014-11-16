using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.DXGI;

namespace JanusEngine.Graphics
{
    public interface ISwapchainFactory
    {
        SwapChain1 CreateSwapChain(Device1 device, SwapChainDescription1 description);

        event Action Resizing;
        event Action Resized;
    }
}
