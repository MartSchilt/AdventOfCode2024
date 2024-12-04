using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

internal class Program
{
    static void Main(string[] args)
    {
        Day4();
    }

    private static async void Day4()
    {
        await TryRead("Day4.txt", (reader) =>
        {
            String? line;
            List<List<char>> allLetters = [];

            while ((line = reader.ReadLine()) != null)
            {
                List<char> letters = [];

                foreach (char letter in line)
                {
                    letters.Add(letter);
                }

                allLetters.Add(letters);
            }

            int totalPart1 = 0;
            int totalPart2 = 0;

            for (int x = 0; x < allLetters.Count; x++)
            {
                for (int y = 0; y < allLetters.First().Count; y++)
                {
                    totalPart1 += Day4Part1(x, y, allLetters);
                    totalPart2 += Day4Part2(x, y, allLetters);
                }
            }

            Console.WriteLine(totalPart1);

            return Task.CompletedTask;
        });
    }

    private static int Day4Part2(int x, int y, List<List<char>> letters)
    {
        char letter = letters[x][y];
        int wordsFound = 0;

        if (letter != 'A')
            return wordsFound;

        try
        {
            var lb = letters[x - 1][y - 1];
            var lt = letters[x - 1][y + 1];
            var rb = letters[x + 1][y - 1];
            var rt = letters[x + 1][y + 1];

            if ((lb == 'M' || lb == 'S') && (rt == 'M' || rt == 'S') && lb != rt)
            {
                if ((lt == 'M' || lt == 'S') && (rb == 'M' || rb == 'S') && lt != rb)
                {
                    wordsFound++;
                }
            }
        }
        catch (Exception)
        { }

        return wordsFound;
    }

    private static int Day4Part1(int x, int y, List<List<char>> letters)
    {
        char letter = letters[x][y];
        int wordsFound = 0;

        if (letter != 'X')
            return wordsFound;

        char[] acceptableLetters = ['X', 'M', 'A', 'S'];
        int[][] directions = [[1, 1], [1, 0], [1, -1], [0, 1], [0, -1], [-1, 1], [-1, 0], [-1, -1]];

        foreach (var direction in directions)
        {
            int letterCount = 0;

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    var newLetter = letters[x + (direction[0] * i)][y + (direction[1] * i)];

                    if (newLetter != acceptableLetters[i])
                        throw new Exception("skip");

                    letterCount++;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "skip")
                        break;
                }
            }

            if (letterCount == 4)
                wordsFound++;
        }

        return wordsFound;
    }

    private static async void Day3()
    {
        await TryRead("Day3.txt", (reader) =>
        {
            String? line;
            int total = 0;
            bool doOperation = true;

            while ((line = reader.ReadLine()) != null)
            {
                foreach (Match regexMatch in Regex.Matches(line, @"mul\(\d+,\d+\)|do\(\)|don't\(\)"))
                {
                    string match = regexMatch.ToString();
                    Console.WriteLine(match);

                    if (match == "do()")
                    {
                        doOperation = true;
                    }
                    else if (match == "don't()")
                    {
                        doOperation = false;
                    }
                    else if (doOperation)
                    {
                        var numbers = Regex.Matches(match, @"\d+");

                        _ = int.TryParse(numbers.First().ToString(), out int number1);
                        _ = int.TryParse(numbers.Last().ToString(), out int number2);

                        total += number1 * number2;
                    }
                }
            }

            Console.WriteLine("Total:");
            Console.WriteLine(total);

            return Task.CompletedTask;
        });
    }

    private static async void Day2()
    {
        await TryRead("Day1.txt", (reader) =>
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

    private static async void Day1()
    {
        await TryRead("Day1.txt", (reader) =>
        {
            List<int> list1 = [];
            List<int> list2 = [];
            String? line;

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

            return Task.CompletedTask;
        });
    }

    private static async Task TryRead(string file, Func<StreamReader, Task> func)
    {
        try
        {
            Console.WriteLine("Start reading file");
            // Open the text file using a stream reader
            using StreamReader reader = new("../../../files/" + file);
            await func(reader);

            reader.Dispose();
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
