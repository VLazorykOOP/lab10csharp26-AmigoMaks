using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarLifeSimulation
{
    public class CarStatistics
    {
        public int TotalDistance { get; set; }
        public int FuelConsumed { get; set; }
        public int BreakdownCount { get; set; }
        public int RefuelCount { get; set; }

        public void PrintStatistics()
        {
            Console.WriteLine("\n=== Simulation Statistics ===");
            Console.WriteLine($"Distance traveled: {TotalDistance} km");
            Console.WriteLine($"Fuel consumed: {FuelConsumed} L");
            Console.WriteLine($"Refuel count: {RefuelCount}");
            Console.WriteLine($"Breakdown count: {BreakdownCount}");
            Console.WriteLine("=============================\n");
        }
    }

    public enum EventPriority
    {
        CriticalBreakdown = 1,
        Refuel = 2,
        RegularDrive = 3
    }

    public class AsyncCar
    {
        public string Model { get; }
        public CarStatistics Stats { get; } = new CarStatistics();

        private PriorityQueue<Func<Task>, int> _actionQueue = new PriorityQueue<Func<Task>, int>();

        public AsyncCar(string model)
        {
            Model = model;
        }

        public void ScheduleEvent(string eventName, EventPriority priority, Func<Task> action)
        {
            Console.WriteLine($"[Dispatcher] Scheduled event: {eventName} (Priority: {priority})");
            _actionQueue.Enqueue(action, (int)priority);
        }

        public async Task ProcessQueueAsync()
        {
            Console.WriteLine($"\n--- Starting life cycle of {Model} ---");

            while (_actionQueue.Count > 0)
            {
                var nextAction = _actionQueue.Dequeue();
                await nextAction();
            }

            Console.WriteLine($"--- Life cycle of {Model} finished ---");
        }

        public async Task DriveAsync(int distance)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Model} started driving for {distance} km...");
            await Task.Delay(1500);

            Stats.TotalDistance += distance;
            Stats.FuelConsumed += distance / 10;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Model} successfully drove {distance} km.");
        }

        public async Task BreakdownAsync()
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] WARNING! {Model} broke down! Calling tow truck...");
            await Task.Delay(2000);

            Stats.BreakdownCount++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Model} has been repaired.");
        }

        public async Task RefuelAsync(int liters)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Model} went to gas station to refuel {liters} L...");
            await Task.Delay(1000);

            Stats.RefuelCount++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Model} refueled.");
        }
    }

    public class Lab10Task2
    {
        public async Task RunAsync()
        {
            Console.WriteLine("--- Starting Task 2: Priorities, Queues, Asynchrony ---");

            AsyncCar advancedCar = new AsyncCar("Ford Mustang");

            advancedCar.ScheduleEvent("Long Drive", EventPriority.RegularDrive, () => advancedCar.DriveAsync(300));
            advancedCar.ScheduleEvent("Flat Tire", EventPriority.CriticalBreakdown, () => advancedCar.BreakdownAsync());
            advancedCar.ScheduleEvent("Refuel", EventPriority.Refuel, () => advancedCar.RefuelAsync(40));
            advancedCar.ScheduleEvent("Trip to store", EventPriority.RegularDrive, () => advancedCar.DriveAsync(15));

            await advancedCar.ProcessQueueAsync();

            advancedCar.Stats.PrintStatistics();
        }
    }
}
