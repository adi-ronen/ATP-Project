using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Compression
{
    class MyCompressorStream : Stream
    {
        private static readonly int m_compress = 1;
        private static readonly int m_decompress = 2;

        private const int m_BufferSize = 100;
        private byte[] m_bytesReadFromStream;
        private Queue<byte> m_queue;
        private MyMaze3DCompressor mazeCompressor;

        private Stream m_io;
        private int m_mode;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="io">stream</param>
        /// <param name="mode">stream mode (compressed or decompressed) </param>
        public MyCompressorStream(Stream io, int mode)
        {
            this.m_io = io;
            this.m_mode = mode;
            m_bytesReadFromStream = new byte[m_BufferSize];
            m_queue = new Queue<byte>();
            mazeCompressor = new MyMaze3DCompressor();
        }
        /// <summary>
        /// returns true if the current stream support reading
        /// </summary>
        public override bool CanRead
        {
            get { return m_io.CanRead; }
        }

        /// <summary>
        /// returns true if the current stream support writing
        /// </summary>
        public override bool CanWrite
        {
            get { return m_io.CanWrite; }
        }

        /// <summary>
        /// read an array of bytes and
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns>bytesCountreturns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (m_mode != m_compress && m_mode != m_decompress)
                return 0;
            int r = 0;
            while (m_queue.Count < count && (r = m_io.Read(m_bytesReadFromStream, 0, m_BufferSize)) != 0)
            {
                byte[] data = new byte[r];
                for (int i = 0; i < r; data[i] = m_bytesReadFromStream[i], i++) ;
                if (m_mode == MyCompressorStream.m_decompress)
                {
                    byte[] decompressed = mazeCompressor.decompress(data);
                    foreach (byte b in decompressed) { m_queue.Enqueue(b); }
                }
                else if (m_mode == MyCompressorStream.m_compress)
                {
                    byte[] compressed = mazeCompressor.compress(data);
                    foreach (byte b in compressed) { m_queue.Enqueue(b); }
                }
            }
            int bytesCount = Math.Min(m_queue.Count, count);
            for (int i = 0; i < bytesCount; i++)
            {
                buffer[i + offset] = m_queue.Dequeue();
            }
            return bytesCount;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (m_mode == MyCompressorStream.m_compress || m_mode == MyCompressorStream.m_decompress)
            {
                byte[] data = new byte[count];
                for (int i = 0; i < count; data[i] = buffer[i + offset], i++) ;
                if (m_mode == MyCompressorStream.m_compress)
                {
                    byte[] compressed = mazeCompressor.compress(data);
                    m_io.Write(compressed, 0, compressed.Length);
                }
                else
                {
                    byte[] decompressed = mazeCompressor.decompress(data);
                    m_io.Write(decompressed, 0, decompressed.Length);
                }
            }
        }
        /// <summary>
        /// get compress mode
        /// </summary>
        public static int Compress
        {
            get
            {
                return m_compress;
            }
        }
        /// <summary>
        /// get decompress mode
        /// </summary>
        public static int Decompress
        {
            get
            {
                return m_decompress;
            }
        }
        public override void Flush()
        {
            m_io.Flush();
        }

        public override bool CanSeek
        {
            get { return m_io.CanSeek; }
        }

        #region nonrelevant
        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
