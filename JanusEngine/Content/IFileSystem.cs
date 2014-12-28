using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusEngine.Content
{
    public interface IFileSystem
    {
        Stream OpenAsset(string uri);

        IAssetMonitor CreateAssetMonitor(string uri);
    }
}
