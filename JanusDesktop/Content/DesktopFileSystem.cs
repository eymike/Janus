using System.IO;
using System.Windows.Forms;

using JanusEngine.Content;

namespace JanusDesktop.Content
{
    public class DesktopFileSystem : IFileSystem
    {
        public string m_basePath;

        public DesktopFileSystem()
        {
            m_basePath = Path.GetDirectoryName(Application.ExecutablePath);
        }

        public Stream OpenAsset(string uri)
        {
            return new FileStream(GetAbsoluteUri(uri), FileMode.Open);
        }

        public IAssetMonitor CreateAssetMonitor(string uri)
        {
            return new DesktopAssetMonitor(GetAbsoluteUri(uri));
        }

        private string GetAbsoluteUri(string uri)
        {
            return Path.Combine(m_basePath, uri);
        }
    }
}
