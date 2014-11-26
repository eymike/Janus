using System.Threading.Tasks;

using JanusEngine.Graphics;

namespace JanusEngine
{
    public class Application
    {
        object m_resetLock = new object();
        public GraphicsDevice GraphicsDevice { get; private set; }

        private IOSMessageLoop m_messageLoop;

        Task m_renderLoop = null;
        Task m_updateLoop = null;

        bool m_renderloopExiting = false;
        bool m_exiting = false;

        public Application(ISwapchainFactory swapChainFactory, IOSMessageLoop messageLoop)
        {
            GraphicsDevice = new GraphicsDevice(swapChainFactory);
            m_messageLoop = messageLoop;

            swapChainFactory.Resizing += OnResizing;
            swapChainFactory.Resized += OnResized;
        }

        private void OnResizing()
        {
            m_renderloopExiting = true;
            lock (m_resetLock)
            {
                GraphicsDevice.Reset();
            }
        }

        private void OnResized()
        {
            m_renderLoop = Task.Run(() => { RenderLoop(); });
        }

        public void Run()
        {
            m_renderLoop = Task.Run(() => { RenderLoop(); });
            m_updateLoop = Task.Run(() => { UpdateLoop(); });

            while (!m_exiting)
            {
                m_messageLoop.PumpMessages();
            }

            m_renderLoop.Wait();
            m_updateLoop.Wait();
        }

        public void Exit()
        {
            m_renderloopExiting = true;
            m_exiting = true;
        }

        private void RenderLoop()
        {
            lock (m_resetLock)
            {
                while (!m_renderloopExiting)
                {
                    GraphicsDevice.Clear();

                    GraphicsDevice.Present();
                }
            }
        }

        private void UpdateLoop()
        {
            while (!m_exiting)
            {
                
            }
        }
    }
}
