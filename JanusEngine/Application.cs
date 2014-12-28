using System.Threading.Tasks;

using JanusEngine.Content;
using JanusEngine.Graphics;

namespace JanusEngine
{
    public class Application
    {
        object m_resetLock = new object();
        public ServiceProvider m_services = new ServiceProvider();

        private IOSMessageLoop m_messageLoop;

        Task m_renderLoop = null;
        Task m_updateLoop = null;

        bool m_renderloopExiting = false;
        bool m_exiting = false;

        public Application(
            ISwapchainFactory swapChainFactory,
            IOSMessageLoop messageLoop,
            IFileSystem fileSystem)
        {
            m_services.Add(new GraphicsDevice(swapChainFactory));
            m_services.Add(new ContentManager(fileSystem, m_services));

            m_messageLoop = messageLoop;

            swapChainFactory.Resizing += OnResizing;
            swapChainFactory.Resized += OnResized;
        }

        private void OnResizing()
        {
            m_renderloopExiting = true;
            lock (m_resetLock)
            {
                var device = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
                device.Reset();
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
            var device = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            lock (m_resetLock)
            {
                while (!m_renderloopExiting)
                {
                    device.Clear();

                    device.Present();
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
