using System;
using System.Threading.Tasks;

namespace CarLifeSimulation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Lab 10 ===");

            Lab10Task1 task1 = new Lab10Task1();
            task1.Run();

            Console.WriteLine("\nPress any key to proceed to Task 2...");
            Console.ReadKey();
            Console.Clear();

            Lab10Task2 task2 = new Lab10Task2();
            await task2.RunAsync();

            Console.WriteLine("\nSimulation finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
