using System;
using System.IO;

using JanusEngine.Content;

namespace JanusDesktop.Content
{
    public class DesktopAssetMonitor : IAssetMonitor
    {
        public event Action<Stream> AssetChanged;

        private FileSystemWatcher m_watcher;
        public string URI { get; private set; }

        public DesktopAssetMonitor(string uri)
        {
            var directory = Path.GetDirectoryName(uri);
            URI = uri;

            m_watcher = new FileSystemWatcher(directory);
            m_watcher.Changed += OnWatchedDirectoryChanged;
        }

        private void Invoke()
        {
            var changed = AssetChanged;
            if (changed != null)
            {
                var fs = new FileStream(URI, FileMode.Open);
                changed(fs);
            }
        }

        private void OnWatchedDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            if(e.FullPath == URI)
            {
                Invoke();
            }
        }
    }
}
