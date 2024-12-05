using System;
using System.Linq;

namespace SiteCoreTechnicalAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTask1();
            TestTask2();
            TestTask3();
            Console.ReadLine();
        }

        static void TestTask1()
        {
            Console.WriteLine("=== Task 1 Tests ===");
            Point p1 = new Point(0, 0);
            Point p2 = new Point(2, 2);

            Aggregation figures = new Aggregation();
            figures.AddFigure(p1);
            figures.AddFigure(p2);

            Console.WriteLine($"Initial P1: ({p1.X}, {p1.Y})");
            figures.Move(1, 1);
            Console.WriteLine($"After move P1: ({p1.X}, {p1.Y})");

            Console.WriteLine($"Before rotation P2: ({p2.X}, {p2.Y})");
            figures.Rotate(90, new Point(0, 0));
            Console.WriteLine($"After rotation P2: ({Math.Round(p2.X, 2)}, {Math.Round(p2.Y, 2)})\n");
        }

        static void TestTask2()
        {
            Console.WriteLine("=== Task 2 Tests ===");
            Console.WriteLine($"Test a@b!!b$a: {PalindromeChecker.IsPalindrome("a@b!!b$a", "!@$")}");
            Console.WriteLine($"Test ?Aa#c: {PalindromeChecker.IsPalindrome("?Aa#c", "#?")}");
            Console.WriteLine($"Test rAce!#car: {PalindromeChecker.IsPalindrome("rAce!#car", "!#")}\n");
        }

        static void TestTask3()
        {
            Console.WriteLine("=== Task 3 Tests ===");

            bool[,] field1 = {
                { false, false, true, true, false },
                { true, true, false, true, false },
                { false, true, true, false, true },
                { true, false, true, false, true },
                { false, true, false, true, true }
            };

            bool[,] field2 = {
                { false, false, true, false, false },
                { true, false, false, false, true },
                { false, false, true, false, false },
                { false, true, false, false, true },
                { false, false, false, true, false }
            };

            bool[,] field3 = {
                { false, true, true, true, true },
                { true, true, false, true, false },
                { true, true, true, false, true },
                { true, false, true, true, true },
                { true, true, true, true, false }
            };

            bool[,] field4 = {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
            };

            Console.WriteLine("Test Case 1: Base case");
            RunPathfinderTest(field1, false);

            Console.WriteLine("\nTest Case 2: Totoshka with Ally");
            RunPathfinderTest(field2, true);

            Console.WriteLine("\nTest Case 3: No Safe Path");
            RunPathfinderTest(field3, true);

            Console.WriteLine("\nTest Case 4: Test with no minefield");
            RunPathfinderTest(field4, true);
        }

        static void RunPathfinderTest(bool[,] field, bool includeAlly)
        {
            var finder = new PathFinder(field, includeAlly);
            var (totoshkaPath, allyPath) = finder.FindSafePath();

            if (totoshkaPath == null)
            {
                Console.WriteLine("No safe path found!");
                return;
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var pos = totoshkaPath.FirstOrDefault(p => p.X == i && p.Y == j);
                    var allyPos = allyPath?.FirstOrDefault(p => p.X == i && p.Y == j);

                    if (pos != null)
                        Console.Write(" T ");
                    else if (allyPos != null)
                        Console.Write(" A ");
                    else if (field[i, j])
                        Console.Write(" X ");
                    else
                        Console.Write(" · ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTotoshka path:");
            foreach (var pos in totoshkaPath)
                Console.Write($"{pos} ");

            if (includeAlly && allyPath != null)
            {
                Console.WriteLine("\n\nAlly path:");
                foreach (var pos in allyPath)
                    Console.Write($"{pos} ");
            }
            Console.WriteLine("\n");
        }
    }
}

