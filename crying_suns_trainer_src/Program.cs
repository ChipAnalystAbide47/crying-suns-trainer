using System;
using System.Diagnostics;
using System.Threading;
using CryingSunsTrainer.Core;

namespace CryingSunsTrainer
{
    /// <summary>
    /// Main entry point for the Crying Suns trainer application.
    /// Attaches to the game process and provides real-time memory manipulation.
    /// </summary>
    internal static class Program
    {
        private const string ProcessName = "CryingSuns";

        private static void Main(string[] args)
        {
            Console.WriteLine("=== Crying Suns Trainer ===");
            Console.WriteLine("Searching for CryingSuns process...");

            Process? gameProcess = null;
            while (gameProcess == null)
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                if (processes.Length > 0)
                {
                    gameProcess = processes[0];
                    Console.WriteLine($"Attached to process ID: {gameProcess.Id}");
                }
                else
                {
                    Console.WriteLine("Game not running. Retrying in 3 seconds...");
                    Thread.Sleep(3000);
                }
            }

            var memoryManager = new MemoryManager(gameProcess);
            var trainer = new TrainerController(memoryManager);

            Console.WriteLine("\nAvailable cheats:");
            Console.WriteLine("1 - Infinite Hull (ship health)");
            Console.WriteLine("2 - Infinite Energy");
            Console.WriteLine("3 - Max Resources (scrap, fuel, food)");
            Console.WriteLine("4 - Unlock All Techs");
            Console.WriteLine("5 - Exit Trainer");

            bool running = true;
            while (running)
            {
                Console.Write("\nSelect option: ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        trainer.ToggleInfiniteHull();
                        break;
                    case "2":
                        trainer.ToggleInfiniteEnergy();
                        break;
                    case "3":
                        trainer.SetMaxResources();
                        break;
                    case "4":
                        trainer.UnlockAllTechs();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting trainer...");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
