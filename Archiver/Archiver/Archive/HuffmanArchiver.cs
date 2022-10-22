using System.Collections.Generic;
using System.IO;
using Archiver.Tree;

namespace Archiver.Archive
{
    public class HuffmanArchiver
    {
        private readonly BinaryTree _binaryTree;
        private readonly Dictionary<char, string> _codes = new();

        /// <summary>
        /// <exception cref="IOException">
        ///     Thrown when an error occurs while reading from the frequency file
        /// </exception>
        /// </summary>
        public HuffmanArchiver(string frequenciesPath)
        {
            _binaryTree = new BinaryTree(frequenciesPath);
            CreateCodes(_binaryTree.Root, string.Empty);
        }

        /// <summary>
        /// <exception cref="IOException">
        ///     Thrown when an error occurs while reading from the source file or writing to the compressed file
        /// </exception>
        /// </summary>
        public void CompressFile(string sourceFilePath, string compressedFilePath)
        {
            ArchiveWriter archiveWriter = new(compressedFilePath);

            var lines = File.ReadAllLines(sourceFilePath);

            for (var i = 0; i < lines.Length; i++)
            {
                foreach (var character in lines[i])
                {
                    archiveWriter.WriteWord(_codes[character]);
                }

                if (i < lines.Length - 1)
                    archiveWriter.WriteWord(_codes['\n']);
            }

            archiveWriter.WriteWord(_codes['$']);
            archiveWriter.Finish();
        }

        /// <summary>
        /// <exception cref="IOException">
        ///     Thrown when an error occurs while reading from the compressed file or writing to the decompressed file
        /// </exception>
        /// </summary>
        public void DecompressFile(string compressedFilePath, string decompressedFilePath)
        {
            ArchiveReader archiveReader = new(compressedFilePath);
            using StreamWriter streamWriter = new(decompressedFilePath);

            while (true)
            {
                var currentNode = _binaryTree.Root;

                while (currentNode.Value.Length != 1)
                {
                    archiveReader.ReadBit(out var bit);
                    currentNode = bit == 0 ? currentNode.LeftChild : currentNode.RightChild;
                }

                var currentCharacter = currentNode.Value;

                if (currentCharacter == "$")
                {
                    break;
                }

                streamWriter.Write(currentCharacter == "\n" ? "\n\r" : currentCharacter);
            }
            
            archiveReader.Finish();
        }

        private void CreateCodes(Node? node, string code)
        {
            if (node is null)
                return;

            if (node.Value.Length == 1)
            {
                _codes.Add(char.Parse(node.Value), code);
                return;
            }

            CreateCodes(node.LeftChild, code + '0');
            CreateCodes(node.RightChild, code + '1');
        }
    }
}