using System;
using System.Threading;

namespace CarLifeSimulation
{
    public class CarEventArgs : EventArgs
    {
        public string Message { get; }
        public CarEventArgs(string message) => Message = message;
    }

    public class SimpleCar
    {
        public string Model { get; }

        public event EventHandler<CarEventArgs> EngineStarted;
        public event EventHandler<CarEventArgs> Moving;
        public event EventHandler<CarEventArgs> Stopped;

        public SimpleCar(string model)
        {
            Model = model;
        }

        public void SimulateLife()
        {
            OnEngineStarted($"Engine of {Model} started.");
            Thread.Sleep(1000);

            OnMoving($"{Model} started moving.");
            Thread.Sleep(2000);

            OnStopped($"{Model} arrived at destination and stopped.");
        }

        protected virtual void OnEngineStarted(string message) => EngineStarted?.Invoke(this, new CarEventArgs(message));
        protected virtual void OnMoving(string message) => Moving?.Invoke(this, new CarEventArgs(message));
        protected virtual void OnStopped(string message) => Stopped?.Invoke(this, new CarEventArgs(message));
    }

    public class Lab10Task1
    {
        public void Run()
        {
            Console.WriteLine("--- Starting Task 1: Car Life ---");

            SimpleCar myCar = new SimpleCar("Toyota Corolla");

            myCar.EngineStarted += (sender, e) => Console.WriteLine($"[EVENT]: {e.Message}");
            myCar.Moving += (sender, e) => Console.WriteLine($"[EVENT]: {e.Message}");
            myCar.Stopped += (sender, e) => Console.WriteLine($"[EVENT]: {e.Message}");

            myCar.SimulateLife();
        }
    }
}
