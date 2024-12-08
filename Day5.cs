namespace AdventOfCode2024;

public static class Day5
{
    private static Dictionary<int, List<int>> Rules = [];
    private static Dictionary<int, List<int>> RulesReversed = [];
    private static List<List<int>> Updates = [];

    public static void Part1()
    {
        Run(true);
    }

    public static void Part2()
    {
        Run(false);
    }

    public static void Run(bool partOne)
    {
        Read();

        int total = 0;

        var levels = GetUpdates(partOne);

        if (!partOne)
            levels = SortUnsafeLevels(levels);

        foreach (var level in levels)
        {
            int index = (level.Count - 1) / 2;
            total += level[index];
        }

        Console.WriteLine("Total of levels:");
        Console.WriteLine(total);
    }

    private static List<List<int>> SortUnsafeLevels(List<List<int>> levels)
    {
        foreach (var level in levels)
        {
            level.Sort((x, y) =>
            {
                if (Rules.TryGetValue(x, out var ruleX))
                {
                    if (ruleX.Contains(y))
                        return -1;
                }

                if (Rules.TryGetValue(y, out var ruleY))
                {
                    if (ruleY.Contains(x))
                        return 1;
                }

                return 0;
            });
        }

        return levels;
    }

    private static List<List<int>> GetUpdates(bool getSafeLevels = true)
    {
        List<List<int>> result = [];

        foreach (var update in Updates)
        {
            bool safe = UpdateIsSafe(update);

            if (getSafeLevels == safe)
            {
                result.Add(update);
            }
        }

        return result;
    }

    private static bool UpdateIsSafe(List<int> update)
    {
        bool safe = true;
        List<int> dissalowedNumbers = [];

        foreach (var number in update)
        {
            if (dissalowedNumbers.Contains(number))
            {
                // Update not allowed
                safe = false;
                break;
            }
            else
            {
                // Update dissalowed numbers list
                RulesReversed.TryGetValue(number, out var rule);

                if (rule != null)
                    dissalowedNumbers = dissalowedNumbers.Union(rule).ToList();
            }
        }

        return safe;
    }

    private static async void Read()
    {
        await Helper.TryRead("Day5.txt", (reader) =>
        {
            String? line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains('|'))
                {
                    // Found rule
                    var numbers = line.Split('|').Select(n => int.Parse(n)).ToList();

                    if (Rules.TryGetValue(numbers[0], out var val))
                    {
                        val.Add(numbers[1]);
                    }
                    else
                    {
                        Rules.Add(numbers[0], [numbers[1]]);
                    }

                    if (RulesReversed.TryGetValue(numbers[1], out var valR))
                    {
                        valR.Add(numbers[0]);
                    }
                    else
                    {
                        RulesReversed.Add(numbers[1], [numbers[0]]);
                    }
                }
                else if (line.Contains(','))
                {
                    // Found update
                    Updates.Add(line.Split(',').Select(n => int.Parse(n)).ToList());
                }
            }

            return Task.CompletedTask;
        });
    }

}
