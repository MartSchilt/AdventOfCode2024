namespace AdventOfCode2024;

public static class DayX
{
    public static void Part1()
    {
        Run(true);
    }

    public static void Part2()
    {
        Run(false);
    }

    public static async void Run(bool partOne)
    {
        await Helper.TryRead("Test.txt", (reader) =>
        {
            String? line;

            while ((line = reader.ReadLine()) != null)
            {
            }

            return Task.CompletedTask;
        });
    }
}
