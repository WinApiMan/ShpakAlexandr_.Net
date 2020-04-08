using BusinessLogic;
using Ninject;
using System;
using Taxi.BusinessLogic.Processings;
using Taxi.ConsoleApplication;

namespace Lab2Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernal = new StandardKernel(new NinjectRegistration());
            var userInterface = new ConsoleMenuInterface(kernal.Get<CarService>(), kernal.Get<DriverService>(), kernal.Get<OrderService>());
            userInterface.StartMenu();
            Console.ReadKey();
        }
    }
}