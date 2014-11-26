using System;
using SharpDX.DXGI;
using System.Windows.Forms;

using JanusEngine;
using JanusEngine.Graphics;

namespace JanusDesktop.Graphics
{
    public class DesktopSwapChainFactory : Form, ISwapchainFactory, IOSMessageLoop
    {
        public event Action Resizing;
        public event Action Resized;

        public DesktopSwapChainFactory()
        {
            ClientSize = new System.Drawing.Size(800, 600);
            //MinimumSize = new System.Drawing.Size(200, 200);

            ResizeRedraw = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

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

        public SwapChain1 CreateSwapChain(Device device, SwapChainDescription1 description)
        {
            var desc = description;
            desc.Width = ClientSize.Width;
            desc.Height = ClientSize.Height;

            Factory2 dxgiFactory = new Factory().QueryInterface<Factory2>();
            return new SwapChain1(dxgiFactory, device, Handle, ref desc, null, null);
        }

        public void PumpMessages()
        {
            System.Windows.Forms.Application.DoEvents();
        }
    }
}
