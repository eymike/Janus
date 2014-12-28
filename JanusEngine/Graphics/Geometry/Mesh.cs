using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace JanusEngine.Graphics.Geometry
{
    public class Mesh
    {
        public SharpDX.Direct3D11.Buffer VertexBuffer { get; protected set; }
        public SharpDX.Direct3D11.Buffer IndexBuffer { get; protected set; }

        protected VertexBufferBinding m_vertexBufferBinding;
        protected PrimitiveTopology m_topology;
        protected Format m_indexFormat;
        protected int m_indexCount;

        void Draw(DeviceContext context)
        {
            context.InputAssembler.PrimitiveTopology = m_topology;
            context.InputAssembler.SetVertexBuffers(0, m_vertexBufferBinding);
            context.InputAssembler.SetIndexBuffer(IndexBuffer, m_indexFormat, 0);
            context.DrawIndexed(m_indexCount, 0, 0);
        }
    }

    public class Mesh<VertexType, IndexType> : Mesh
        where VertexType : struct 
        where IndexType : struct
    {
        public Mesh(GraphicsDevice graphics, VertexType[] vertexes, IndexType[] indexes, PrimitiveTopology topology)
        {
            var sizeOfIndex = Marshal.SizeOf(typeof(IndexType));
            if (sizeOfIndex != 4 && sizeOfIndex != 2)
            {
                throw new ArgumentException("Must use index type that is size of int or short.");
            }

            m_topology = topology;
            m_indexFormat = sizeOfIndex == 4 ? Format.R32_UInt : Format.R16_UInt;
            m_indexCount = indexes.Length;

            var sizeOfVertex = Marshal.SizeOf(typeof(VertexType));
            var vertexBufferSizeInBytes = sizeOfVertex * vertexes.Length;

            BufferDescription vertexDesc = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = vertexBufferSizeInBytes,
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            using (var dataStream = new DataStream(vertexBufferSizeInBytes, true, true))
            {
                dataStream.WriteRange<VertexType>(vertexes);
                VertexBuffer = new SharpDX.Direct3D11.Buffer(graphics.Device, dataStream, vertexDesc);
            }

            var indexBufferSizeInBytes = Marshal.SizeOf(typeof(IndexType)) * indexes.Length;

            var indexBufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = indexBufferSizeInBytes,
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            using (var datastream = new DataStream(vertexBufferSizeInBytes, true, true))
            {
                datastream.WriteRange<IndexType>(indexes);
                IndexBuffer = new SharpDX.Direct3D11.Buffer(graphics.Device, datastream, indexBufferDescription);
            }

            m_vertexBufferBinding = new VertexBufferBinding(VertexBuffer, sizeOfVertex, 0);
        }
    }
}
