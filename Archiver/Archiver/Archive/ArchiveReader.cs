using System.IO;

namespace Archiver.Archive
{
    public class ArchiveReader
    {
        private const byte BufferSize = 10;
        private readonly byte[] _buffer = new byte[BufferSize];
        private readonly byte[] _oneByte = new byte[8];
        private byte _bitsCount;
        private byte _bytesCount;
        private byte _byteIndex;
        private readonly FileStream _fileStream;
        
        /// <summary>
        /// Creates an object to read bitwise from the file. To finish reading, use the Finish method
        /// </summary>
        public ArchiveReader(string filePath)
        {
            _fileStream = new FileStream(filePath, FileMode.Open);
        }

        /// <summary>
        /// Reads one bit from the file
        /// </summary>
        /// <param name="bit">Read bit value</param>
        /// <returns>True if the read was successful, false if the end of the file has been reached.</returns>
        public bool ReadBit(out byte bit)
        {
            var result = true;

            if (_bitsCount == 0)
            {
                result = ReadByte();
            }

            bit = result ? _oneByte[--_bitsCount] : (byte) 0;

            return result;
        }
        
        /// <summary>
        /// Closes the file
        /// </summary>
        public void Finish() => _fileStream.Close();
        
        private bool ReadByte()
        {
            if (_bytesCount == _byteIndex)
            {
                _bytesCount = (byte)_fileStream.Read(_buffer, 0, BufferSize);
                _byteIndex = 0;
            }

            if (_bytesCount == 0)
            {
                return false;
            }

            var oneByte = _buffer[_byteIndex++];
            
            for (var i = 0; i < 8; i++)
            {
                _oneByte[i] = (byte)(oneByte & 1);
                oneByte = (byte)(oneByte >> 1);
            }
            
            _bitsCount = 8;
            
            return true;
        }
    }
}