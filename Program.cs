namespace AdventOfCode2024;

internal class Program
{
    static void Main(string[] args)
    {
        Day2();
    }

    private static void Day2()
    {
        try
        {
            // Open the text file using a stream reader
            using StreamReader reader = new("../../../Day2.txt");

            String? line;
            int safeLevels = 0;

            Console.WriteLine("Start reading file");

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine("------");

                var levels = line.Split(' ');
                var safe = Day2Checker(levels);

                if (safe)
                {
                    safeLevels++;
                }
                else
                {
                    for (int i = 0; i < levels.Length; i++)
                    {
                        var newLevels = levels.ToList();
                        newLevels.RemoveAt(i);
                        var newSafe = Day2Checker([.. newLevels]);

                        if (newSafe)
                        {
                            safeLevels++;
                            break;
                        }
                    }
                }

                Console.WriteLine("------");
            }

            Console.WriteLine(safeLevels);
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    private static bool Day2Checker(string[] levels)
    {
        int count = 0;
        int prevNumber = 0;
        bool ascending = true;
        bool safe = true;

        foreach (var level in levels)
        {
            count++;
            _ = int.TryParse(level, out int number);
            Console.WriteLine(number);

            if (count > 1)
            {
                int diff = Math.Abs(prevNumber - number);
                if (diff < 1 || diff > 3)
                {
                    safe = false;
                }

                if (count == 2)
                {
                    ascending = prevNumber < number;
                }
                else if (ascending != prevNumber < number)
                {
                    safe = false;
                }
            }

            prevNumber = number;
        }

        Console.WriteLine(safe);
        Console.WriteLine("---");

        return safe;
    }

    private static void Day1()
    {
        try
        {
            // Open the text file using a stream reader
            using StreamReader reader = new("../../../Day1.txt");

            List<int> list1 = [];
            List<int> list2 = [];
            String? line;

            Console.WriteLine("Start reading file");

            while ((line = reader.ReadLine()) != null)
            {
                var numbers = line.Split(' ');

                _ = int.TryParse(numbers[0], out int number1);
                _ = int.TryParse(numbers[3], out int number2);

                list1.Add(number1);
                list2.Add(number2);
            }

            Console.WriteLine("Finished reading, start sorting lists");
            list1.Sort();
            list2.Sort();

            Console.WriteLine("Calculating differences");
            List<int> differences = [];

            for (int i = 0; i < list1.Count; i++)
            {
                differences.Add(Math.Abs(list1[i] - list2[i]));
            }

            Console.WriteLine(differences.Sum());

            Console.WriteLine("Calculating similarities");

            List<int> similarities = [];
            foreach (int number in list1)
            {
                var matches = list2.FindAll(x => x == number);
                similarities.Add(number * matches.Count);
            }
            Console.WriteLine(similarities.Sum());
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
