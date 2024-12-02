namespace AdventOfCode2024;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        Day1();
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
            foreach(int number in list1)
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
