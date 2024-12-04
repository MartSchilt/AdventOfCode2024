using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public static class Day3
{
    public static async void Run()
    {
        await Helper.TryRead("Day3.txt", (reader) =>
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
}
