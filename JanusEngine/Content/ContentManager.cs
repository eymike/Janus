using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusEngine.Content
{
    public class AssetRef
    {
        protected AssetTypeLoader m_loader;
        protected IAssetMonitor m_assetMonitor;

        public AssetRef(AssetTypeLoader loader, IAssetMonitor monitor)
        {
            m_loader = loader;
            m_assetMonitor = monitor;
        }


    }

    public class AssetRef<AssetType> : AssetRef
    {
        public AssetType Asset { get; private set; }

        public AssetRef(AssetType asset, AssetTypeLoader loader, IAssetMonitor monitor) :
            base(loader, monitor)
        {
            Asset = asset;
            m_assetMonitor.AssetChanged += OnAssetChanged;
        }

        private void OnAssetChanged(Stream obj)
        {
            Asset = (AssetType)m_loader.ProcessStream(obj);
        }
    }

    public abstract class AssetTypeLoader
    {
        public Type AssetType { get; protected set; }
        protected IFileSystem m_filesystem;
        protected ServiceProvider m_services;

        public void Initialize(IFileSystem filesystem, ServiceProvider services)
        {
            m_filesystem = filesystem;
            m_services = services;
        }

        public AssetRef LoadAsset(string uri)
        {
            return PackageAsset(ProcessStream(m_filesystem.OpenAsset(uri)), m_filesystem.CreateAssetMonitor(uri));
        }

        public abstract object ProcessStream(Stream assetStream);
        public abstract AssetRef PackageAsset(object asset, IAssetMonitor monitor);
    }


    public class ContentManager
    {
        IFileSystem m_filesystem;
        ServiceProvider m_services;

        private Dictionary<string, AssetRef> m_loadedAssets = new Dictionary<string, AssetRef>();
        private Dictionary<Type, AssetTypeLoader> m_assetLoaders = new Dictionary<Type, AssetTypeLoader>();

        public ContentManager(IFileSystem filesystem, ServiceProvider services)
        {
            m_filesystem = filesystem;
            m_services = services;

            AddAssetLoader<VertexShaderAssetLoader>();
            AddAssetLoader<PixelShaderAssetLoader>();
            AddAssetLoader<GeometryShaderAssetLoader>();
            AddAssetLoader<HullShaderAssetLoader>();
            AddAssetLoader<DomainShaderAssetLoader>();
        }

        public void AddAssetLoader<AssetLoaderType>() where AssetLoaderType : AssetTypeLoader, new()
        {
            var loader = new AssetLoaderType();
            loader.Initialize(m_filesystem, m_services);
            m_assetLoaders.Add(loader.AssetType, loader);
        }

        public AssetRef<AssetType> Load<AssetType>(string uri)
        {
            AssetRef<AssetType> retval = null;
            if (m_loadedAssets.ContainsKey(uri))
            {
                retval = (AssetRef<AssetType>)m_loadedAssets[uri];
            }
            else
            {
                var loader = m_assetLoaders[typeof(AssetType)];
                var assetRef = loader.LoadAsset(uri);

                m_loadedAssets.Add(uri, assetRef);
                retval = (AssetRef<AssetType>)assetRef;
            }

            return retval;
        }

    }
}
