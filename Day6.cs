namespace AdventOfCode2024;

public class Node(int x, int y, bool obstacle = false)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public bool Visited { get; set; } = false;
    public bool Obstacle { get; set; } = obstacle;
    public int Direction { get; set; } = -1;

    public override string ToString()
    {
        return $"[{X}, {Y}, {Visited}, {Obstacle}. {Direction}]";
    }
}

public static class Day6
{
    public static Node? startingNode = null;
    public static List<List<Node>> allNodes = [];

    public static async void Part1()
    {
        startingNode = await Read();
        Run(true);
    }

    public static async void Part2()
    {
        startingNode = await Read();
        int counter = 0;

        for (int i = 0; i < allNodes.Count; i++)
        {
            for (int j = 0; j < allNodes.First().Count; j++)
            {
                var node = allNodes[i][j];

                if (node.Obstacle || node == startingNode)
                    continue;

                // Reset all nodes
                startingNode = await Read();
                node = allNodes[i][j];

                if (HasLoop(node))
                    counter++;
            }
        }

        Console.WriteLine(counter);
    }

    private static bool HasLoop(Node node)
    {
        bool hasLoop = false;

        // Set it as an obstacle
        node.Obstacle = true;

        try
        {
            Console.WriteLine("Changed this node to an obstacle:");
            Console.WriteLine(node);
            Run(false);
        }
        catch (Exception)
        {
            hasLoop = true;
        }

        // Return it to the original state
        node.Obstacle = false;
        return hasLoop;
    }

    private static void Run(bool partOne)
    {
        int[][] directions = [[-1, 0], [0, 1], [1, 0], [0, -1]];
        int direction = 0;
        int x = startingNode.X;
        int y = startingNode.Y;
        bool outOfBounds = false;
        int numberOfVisitedSpaces = 1;

        Console.WriteLine("Checking...");
        while (!outOfBounds)
        {
            try
            {
                var nextNode = allNodes[y + directions[direction][0]][x + directions[direction][1]];

                if (!partOne)
                {
                    // I have some recollection of this place
                    if (nextNode.Visited && nextNode.Direction == direction)
                    {
                        Console.WriteLine("Loop found");
                        throw new Exception("Loop found");
                    }
                }

                if (nextNode.Obstacle)
                {
                    //Console.WriteLine("Change direction");
                    direction++;

                    if (direction >= 4)
                        direction = 0;
                }
                else
                {
                    //Console.WriteLine("Move");
                    x = x + directions[direction][1];
                    y = y + directions[direction][0];

                    if (!nextNode.Visited)
                    {
                        numberOfVisitedSpaces++;
                        nextNode.Visited = true;
                        nextNode.Direction = direction;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Loop found")
                    throw;

                outOfBounds = true;
                Console.WriteLine("Guard left the premise");

                if (partOne)
                    Console.WriteLine(numberOfVisitedSpaces);
            }
        }
    }

    private static async Task<Node?> Read()
    {
        Node? startingNode = null;
        allNodes = [];

        await Helper.TryRead("Day6.txt", (reader) =>
        {
            String? line;
            int y = 0;

            while ((line = reader.ReadLine()) != null)
            {
                int x = 0;
                List<Node> nodes = [];

                foreach (var character in line)
                {
                    bool obstacle = false;

                    if (character == '#')
                        obstacle = true;

                    Node node = new(x, y, obstacle);
                    nodes.Add(node);

                    if (character == '^')
                    {
                        startingNode = node;
                        startingNode.Visited = true;
                    }

                    x++;
                }

                allNodes.Add(nodes);
                y++;
            }

            return Task.CompletedTask;
        });

        return startingNode;
    }
}
