using System;
using System.Collections.Generic;

namespace KnapsackProblem;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите количество предметов и вместимость рюкзака через пробел:");
        var tokens = Console.ReadLine().Split();
        var itemsNumber = int.Parse(tokens[0]);
        var maxWeight = int.Parse(tokens[1]);

        var items = new List<Item>();
        Console.WriteLine($"Введите веса {itemsNumber} предметов через пробел:");
        var weights = Console.ReadLine().Split();
        Console.WriteLine($"Введите стоимости {itemsNumber} предметов через пробел:");
        var values = Console.ReadLine().Split();

        for (var i = 0; i < itemsNumber; i++)
        {
            var item = new Item(int.Parse(weights[i]), int.Parse(values[i]), i + 1);
            items.Add(item);
        }

        var solver = new KnapsackProblemSolver();
        var itemNumbers = solver.Solve(items, maxWeight);

        if (itemNumbers.Count == 0)
        {
            Console.WriteLine("Ни один предмет не влез в рюкзак...");
            return;
        }
        
        Console.WriteLine("Номера предметов, которые необходимо сложить в рюкзак:");
        
        foreach (var itemNumber in itemNumbers)
        {
            Console.Write($"{itemNumber} ");
        }
    }
}