namespace KnapsackProblem;

public struct Item
{
    public int Weight { get; init; }
    public int Value { get; init; }
    public bool IsTaken { get; set; }
}