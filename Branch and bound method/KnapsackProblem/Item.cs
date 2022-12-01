namespace KnapsackProblem;

public class Item
{
    public int Weight { get; }
    public int Value { get; }
    public double UnitValue { get; }
    public int Number { get; }

    public Item(int weight, int value, int number)
    {
        Weight = weight;
        Value = value;
        UnitValue = (double)Value / Weight;
        Number = number;
    }
}