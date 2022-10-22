namespace Archiver.Tree
{
    public class Node
    {
        public string Value { get; init; } = string.Empty;
        public double Frequency { get; init; }
        public Node? LeftChild { get; init; }
        public Node? RightChild { get; init; }
    }
}