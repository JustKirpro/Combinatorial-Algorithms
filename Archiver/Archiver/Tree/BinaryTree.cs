using System.Collections.Generic;
using System.IO;

namespace Archiver.Tree
{
    public class BinaryTree
    {
        public Node? Root { get; }

        private readonly PriorityQueue<Node, double> _priorityQueue = new();
        
        /// <summary>
        /// <exception cref="IOException">
        ///     Thrown when an error occurs while reading from the frequency file
        /// </exception>
        /// </summary>
        public BinaryTree(string frequenciesPath)
        {
            ReadFrequencies(frequenciesPath);

            while (_priorityQueue.Count > 1)
            {
                ConnectNodes();
            }

            Root = _priorityQueue.Dequeue();
        }
        
        private void ReadFrequencies(string frequenciesPath)
        {
            _priorityQueue.Enqueue(new Node {Value = "$", Frequency = 0}, 0);
            var lines = File.ReadAllLines(frequenciesPath);

            foreach (var line in lines)
            {
                var content = line.Split();
                var character = " ";
                
                if (content.Length == 2)
                {
                    character = content[0] == string.Empty ? "\n" : content[0];
                }
                
                var frequency = double.Parse(content.Length == 2 ? content[1] : content[2]);
                _priorityQueue.Enqueue(new Node {Value = character, Frequency = frequency}, frequency);
            }
        }
        
        private void ConnectNodes()
        {
            var firstNode = _priorityQueue.Dequeue();
            var secondNode = _priorityQueue.Dequeue();

            var newNode = new Node
            {
                Value = firstNode.Value + secondNode.Value,
                Frequency = firstNode.Frequency + secondNode.Frequency,
                LeftChild = firstNode,
                RightChild = secondNode
            };
            
            _priorityQueue.Enqueue(newNode, newNode.Frequency);
        }
    }   
}