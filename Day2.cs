namespace AdventOfCode2024;

public static class Day2
{
    public static async void Run()
    {
        await Helper.TryRead("Day2.txt", (reader) =>
        {
            String? line;
            int safeLevels = 0;

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
            return Task.CompletedTask;
        });
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
}
