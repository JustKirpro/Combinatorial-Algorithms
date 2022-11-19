namespace KnapsackProblem;

public static class KnapsackProblemSolver
{
    public static void Solve(Item[] items, int maxWeight)
    {
        Console.WriteLine("Test data items:");
        PrintItems(items);
        Console.WriteLine($"Test data max weight = {maxWeight}\n");
        var n = items.Length;
        
        var s = new int[n];
        var b = new int[n];
    
        for (var i = 0; i < n; i++)
        {
            b[i] = i + 1;
        }

        var currentValue = 0;
        var maxValue = 0;
        var currentWeight = 0;
        var top = 0;
        var changedIndex = -1;
        
        while(true)
        {
            if (changedIndex > -1)
            {
                if (s[changedIndex] == 0)
                {
                    currentValue -= items[changedIndex].Value;
                    currentWeight -= items[changedIndex].Weight;
                }
                else
                {
                    currentValue += items[changedIndex].Value;
                    currentWeight += items[changedIndex].Weight;
                }
    
                if (currentWeight <= maxWeight && currentValue > maxValue)
                {
                    maxValue = currentValue;

                    for (var i = 0; i < n; i++)
                    {
                        items[i].IsTaken = s[i] == 1;
                    }
                }
            }
    
            PrintIteration(s, currentValue, currentWeight);
            
            if (top == n)
                break;
            
            s[top] = 1 - s[top];
            changedIndex = top;
    
            if (top == 0)
            {
                top = b[0];
                b[0] = 1;
            }
            else
            {
                b[top - 1] = b[top];
                b[top] = top + 1;
                top = 0;
            }
        }
        
        Console.WriteLine($"Max value = {maxValue}");
        Console.WriteLine("List of taken items:");
        PrintTakenItems(items);
    }
    
    public static void Solve(int n)
    {
        var items = GenerateTestData(n);
        var maxWeight = GenerateMaxWeight(n);
        Solve(items, maxWeight);
    }
    
    private static Item[] GenerateTestData(int length)
    {
        var random = new Random();
        var items = new Item[length];
            
        for (var i = 0; i < length; i++)
        {
            var weight = (int)random.NextInt64(1, 10);
            var value = (int)random.NextInt64(1, 10);
            items[i] = new Item {Weight = weight, Value = value};
        }

        return items;
    }

    private static int GenerateMaxWeight(int length) => (int) new Random().NextInt64((int)(2.5 * length), (int)(7.5 * length));

    private static void PrintIteration(int[] code, int currentValue, int currentWeight)
    {
        foreach (var digit in code)
        {
            Console.Write($"{digit } ");
        }
        
        Console.WriteLine($"current weight = {currentWeight}, current value = {currentValue}");
    }

    private static void PrintItems(Item[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"Item #{i + 1}: weight = {items[i].Weight}, value = {items[i].Value}");
        }
    }

    private static void PrintTakenItems(Item[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i].IsTaken)
                Console.Write($"{i + 1} ");
        }
        
        Console.WriteLine();
    }
}