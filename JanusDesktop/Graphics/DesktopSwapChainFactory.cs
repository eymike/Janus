using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using System.Windows.Forms;


namespace JanusDesktop.Graphics
{
    public class DesktopSwapChainFactory : Form, JanusEngine.Graphics.ISwapchainFactory
    {
        public event Action Resizing;
        public event Action Resized;

        public DesktopSwapChainFactory()
        {
            ResizeBegin += OnWindowResizeBegin;
            ResizeEnd += OnWindowSizeResizeEnd;
        }

        private void OnWindowResizeBegin(object sender, EventArgs e)
        {
            var ev = Resizing;
            if (ev != null)
            {
                ev();
            }
        }

        private void OnWindowSizeResizeEnd(object sender, EventArgs e)
        {
            var ev = Resized;
            if (ev != null)
            {
                ev();
            }
        }

        public SwapChain1 CreateSwapChain(Device1 device, SwapChainDescription1 description)
        {
            var desc = description;
            desc.Width = ClientSize.Width;
            desc.Height = ClientSize.Height;

            var dxgiFactory = ComObject.As<Factory2>(new Factory());
            return dxgiFactory.CreateSwapChainForHwnd(device, Handle, ref desc, null, null);
        }
    }
}
