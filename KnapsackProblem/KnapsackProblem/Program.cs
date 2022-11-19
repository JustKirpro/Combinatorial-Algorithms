namespace KnapsackProblem;

internal static class Program
{
    public static void Main()
    {
        const string menuText = "Выберите операцию:\n1 - Запустить подготовленный тест\n2 - Запустить случайный тест\n3 - Выйти";
        
        while (true)
        {
            Console.WriteLine(menuText);

            var operation = ReadInt();
        
            switch (operation)
            {
                case 1:
                    RunDefinedTest();
                    break;
                case 2:
                    RunRandomTest();
                    break;
                case 3:
                    Console.WriteLine("Завершение работы");
                    return;
            }
        }
    }

    private static void RunDefinedTest()
    {
        const int maxWeight = 27;
        var items = new[]
        {
            new Item {Weight = 9, Value = 5},
            new Item {Weight = 9, Value = 8},
            new Item {Weight = 9, Value = 2}
        };
        
        KnapsackProblemSolver.Solve(items, maxWeight);
    }

    private static void RunRandomTest()
    {
        Console.WriteLine("Введите количество предметов в рюкзаке:");
        var itemsNumber = ReadInt(1, 12);
        KnapsackProblemSolver.Solve(itemsNumber);
    }

    private static int ReadInt(int lowerBound = 1, int upperBound = 3)
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var number) && number >= lowerBound && number <= upperBound)
            {
                return number;
            }
            
            Console.WriteLine("Введено некорректное значение, попробуйте ещё раз:");
        }
    }
}