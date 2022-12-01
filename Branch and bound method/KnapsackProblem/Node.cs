using System.Collections.Generic;

namespace KnapsackProblem;

public class Node
{
    public double Estimation { get; init; }
    public int Level { get; init; }
    public List<int> ItemNumbers { get; init; } = new();
    public int Weight { get; init; }
    public int Value { get; init; }
}