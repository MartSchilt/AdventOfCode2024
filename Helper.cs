namespace AdventOfCode2024;
public static class Helper
{

    public static async Task TryRead(string file, Func<StreamReader, Task> func)
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
