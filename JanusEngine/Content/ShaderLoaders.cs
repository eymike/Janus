using System;
using System.Collections.Generic;
using System.IO;

using SharpDX.Direct3D11;
using JanusEngine.Graphics;

namespace JanusEngine.Content
{
    public class VertexShaderAssetLoader : AssetTypeLoader
    {
        public VertexShaderAssetLoader() 
        {
            AssetType = typeof(VertexShader);
        }

        public override object ProcessStream(Stream assetStream)
        {
            var graphics = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            var device = graphics.Device;

            assetStream.Position = 0;
            var buffer = new byte[assetStream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < assetStream.Length;)
            {
                totalBytesCopied += assetStream.Read(buffer, totalBytesCopied, Convert.ToInt32(assetStream.Length) - totalBytesCopied);
            }

            var vShader = new VertexShader(device, buffer);
            vShader.Tag = buffer;

            return vShader;
        }

        public override AssetRef PackageAsset(object asset, IAssetMonitor monitor)
        {
            return new AssetRef<VertexShader>((VertexShader)asset, this, monitor);
        }
    }

    public class PixelShaderAssetLoader : AssetTypeLoader
    {
        public PixelShaderAssetLoader()
        {
            AssetType = typeof(PixelShader);
        }

        public override object ProcessStream(Stream assetStream)
        {
            var graphics = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            var device = graphics.Device;

            assetStream.Position = 0;
            var buffer = new byte[assetStream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < assetStream.Length;)
            {
                totalBytesCopied += assetStream.Read(buffer, totalBytesCopied, Convert.ToInt32(assetStream.Length) - totalBytesCopied);
            }

            var vShader = new PixelShader(device, buffer);
            vShader.Tag = buffer;

            return vShader;
        }

        public override AssetRef PackageAsset(object asset, IAssetMonitor monitor)
        {
            return new AssetRef<PixelShader>((PixelShader)asset, this, monitor);
        }
    }

    public class GeometryShaderAssetLoader : AssetTypeLoader
    {
        public GeometryShaderAssetLoader()
        {
            AssetType = typeof(GeometryShader);
        }

        public override object ProcessStream(Stream assetStream)
        {
            var graphics = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            var device = graphics.Device;

            assetStream.Position = 0;
            var buffer = new byte[assetStream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < assetStream.Length;)
            {
                totalBytesCopied += assetStream.Read(buffer, totalBytesCopied, Convert.ToInt32(assetStream.Length) - totalBytesCopied);
            }

            var vShader = new GeometryShader(device, buffer);
            vShader.Tag = buffer;

            return vShader;
        }

        public override AssetRef PackageAsset(object asset, IAssetMonitor monitor)
        {
            return new AssetRef<GeometryShader>((GeometryShader)asset, this, monitor);
        }
    }

    public class HullShaderAssetLoader : AssetTypeLoader
    {
        public HullShaderAssetLoader()
        {
            AssetType = typeof(HullShader);
        }

        public override object ProcessStream(Stream assetStream)
        {
            var graphics = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            var device = graphics.Device;

            assetStream.Position = 0;
            var buffer = new byte[assetStream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < assetStream.Length;)
            {
                totalBytesCopied += assetStream.Read(buffer, totalBytesCopied, Convert.ToInt32(assetStream.Length) - totalBytesCopied);
            }

            var vShader = new HullShader(device, buffer);
            vShader.Tag = buffer;

            return vShader;
        }

        public override AssetRef PackageAsset(object asset, IAssetMonitor monitor)
        {
            return new AssetRef<HullShader>((HullShader)asset, this, monitor);
        }
    }

    public class DomainShaderAssetLoader : AssetTypeLoader
    {
        public DomainShaderAssetLoader()
        {
            AssetType = typeof(DomainShader);
        }

        public override object ProcessStream(Stream assetStream)
        {
            var graphics = (GraphicsDevice)m_services.GetService(typeof(GraphicsDevice));
            var device = graphics.Device;

            assetStream.Position = 0;
            var buffer = new byte[assetStream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < assetStream.Length;)
            {
                totalBytesCopied += assetStream.Read(buffer, totalBytesCopied, Convert.ToInt32(assetStream.Length) - totalBytesCopied);
            }

            var vShader = new DomainShader(device, buffer);
            vShader.Tag = buffer;

            return vShader;
        }

        public override AssetRef PackageAsset(object asset, IAssetMonitor monitor)
        {
            return new AssetRef<DomainShader>((DomainShader)asset, this, monitor);
        }
    }
}