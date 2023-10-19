using System.Text;

namespace lampirKnapsack;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Alice and Bob for Lampir HW!\n");
        Console.WriteLine("Are you Alice(A) or Bob(B)?");
        var correctChoice = false;
        while (!correctChoice)
        {
            Console.Write("> Enter A or B:");
            string? choice = Console.ReadLine();
            if (choice?.ToLower() is "a")
            {
                Alice.Logic();
                correctChoice = true;
            }

            if (choice?.ToLower() is "b")
            {
                Bob.Logic();
                correctChoice = true;
            }
        }
    }
}