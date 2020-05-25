using System;
using System.Threading.Tasks;
using Taxi.ConsoleUI.Enums;
using Taxi.ConsoleUI.Interfaces;

namespace Taxi.ConsoleUI.TaxiServices
{
    public class AdminConsoleService : IConsoleService<AdminConsoleService>
    {
        private readonly IConsoleService<CarConsoleService> _carService;

        private readonly IConsoleService<DriverConsoleService> _driverService;

        private readonly IConsoleService<OrderConsoleService> _orderService;

        public AdminConsoleService(IConsoleService<CarConsoleService> carService, IConsoleService<DriverConsoleService> driverService, IConsoleService<OrderConsoleService> orderService)
        {
            _carService = carService;
            _driverService = driverService;
            _orderService = orderService;
        }

        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Administrator menu");
            Console.WriteLine("1.Car menu");
            Console.WriteLine("2.Driver menu");
            Console.WriteLine("3.Order menu");
            Console.WriteLine("4.Return to the main menu");
        }

        public async Task StartMenu()
        {
            while (true)
            {
                PrintMenu();
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsMenu.Car:
                        {
                            await _carService.StartMenu();
                        }
                        break;

                    case (int)AdminsMenu.Driver:
                        {
                            await _driverService.StartMenu();
                        }
                        break;

                    case (int)AdminsMenu.Order:
                        {
                            await _orderService.StartMenu();
                        }
                        break;

                    case (int)AdminsMenu.MainMenu:
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Incorrect number");
                        }
                        break;
                }
            }
        }
    }
}