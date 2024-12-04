namespace AdventOfCode2024;

public static class Day4
{
    public static void Part1()
    {
        Run(true);
    }

    public static void Part2()
    {
        Run(false);
    }

    private static async void Run(bool part1)
    {
        await Helper.TryRead("Day4.txt", (reader) =>
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

            int total = 0;

            for (int x = 0; x < allLetters.Count; x++)
            {
                for (int y = 0; y < allLetters.First().Count; y++)
                {
                    if (part1)
                        total += Day4Part1(x, y, allLetters);
                    else
                        total += Day4Part2(x, y, allLetters);
                }
            }

            Console.WriteLine(total);

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
}
