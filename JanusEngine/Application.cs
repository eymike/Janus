using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JanusEngine.Graphics;

namespace JanusEngine
{
    public class Application
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public Application(ISwapchainFactory swapChainFactory)
        {
            GraphicsDevice = new GraphicsDevice(swapChainFactory);
        }

        public void Run()
        {
            
        }

    }
}
