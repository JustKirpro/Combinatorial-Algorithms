using System.IO;

namespace Archiver.Archive
{
    public class ArchiveWriter
    {
        private const byte BufferSize = 10;
        private readonly byte[] _buf = new byte[BufferSize];
        private byte _oneByte;
        private byte _bitsCount;
        private byte _bytesCount;
        private readonly FileStream _fileStream;
        
        /// <summary>
        /// Creates an object to write bitwise to the file. To finish writing, use the Finish method
        /// </summary>
        public ArchiveWriter(string fileName)
        {
            _fileStream = new FileStream(fileName, FileMode.Create);
        }
        
        /// <summary>
        /// Writes one bit to the file
        /// </summary>
        /// <param name="bit">Written bit value</param>
        private void WriteBit(byte bit)
        {
            _oneByte = (byte)((_oneByte << 1) + bit);
            _bitsCount++;
            
            if (_bitsCount == 8)
                WriteByte();
        }
        
        /// <summary>
        /// Writing to a bit string file
        /// </summary>
        /// <param name="w">Bit string (should only contain characters 0 and 1)</param>
        public void WriteWord(string w)
        {
            foreach (var c in w)
            {
                WriteBit(c == '0' ? (byte) 0 : (byte) 1);
            }
        }
        
        /// <summary>
        /// Ends writing to the file
        /// </summary>
        public void Finish()
        {
            while (_bitsCount > 0)
            {
                WriteBit(0);
            }

            if (_bytesCount > 0)
            {
                _fileStream.Write(_buf, 0, _bytesCount);
            }

            _fileStream.Close();
        }
        
        private void WriteByte()
        {
            _buf[_bytesCount++] = _oneByte;
            _oneByte = 0;
            _bitsCount = 0;
            
            if (_bytesCount == BufferSize)
            {
                _fileStream.Write(_buf, 0, BufferSize);
                _bytesCount = 0;
            }
        }
    }
}