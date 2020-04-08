using System;
using System.Linq;
using Taxi.BusinessLogic.Processings;
using Taxi.BusinessLogic.Validations;

namespace Taxi.ConsoleApplication
{
    internal enum MainMenu
    {
        Administrator = 1,
        Driver,
        Client,
        Exit
    }

    internal enum AdminsMenu
    {
        Car = 1,
        Driver,
        Order,
        MainMenu
    }

    internal enum AdminsCarMenu
    {
        AllCar = 1,
        AddCar,
        DeleteCar,
        UpdateCar,
        OnRepair,
        Find,
        OldCars,
        AdminMenu
    }

    internal enum AdminsDriverMenu
    {
        AllDriver = 1,
        AddDriver,
        DeleteDriver,
        UpdateDriver,
        Find,
        GiveCar,
        AdminMenu
    }

    internal enum AdminsOrderMenu
    {
        ShowAll = 1,
        ShowActive,
        ShowInactive,
        Add,
        AdminMenu
    }

    public class ConsoleMenuInterface
    {
        private readonly CarService _carProcessing;

        private readonly DriverService _driverProcessing;

        private readonly OrderService _orderProcessing;

        public ConsoleMenuInterface(CarService carProcessing, DriverService driverProcessing, OrderService orderProcessing)
        {
            _carProcessing = carProcessing;
            _driverProcessing = driverProcessing;
            _orderProcessing = orderProcessing;
        }

        private const int NoError = 0;

        public void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose role");
                Console.WriteLine("1.Administrator");
                Console.WriteLine("2.Driver");
                Console.WriteLine("3.Client");
                Console.WriteLine("4.Exit");
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)MainMenu.Administrator:
                        {
                            AdminMenu();
                        }
                        break;

                    case (int)MainMenu.Driver:
                        {
                        }
                        break;

                    case (int)MainMenu.Client:
                        {
                        }
                        break;

                    case (int)MainMenu.Exit:
                        {
                            Environment.Exit(0);
                        }
                        break;

                    default:
                        {
                            Console.WriteLine("Incorrect number");
                        }
                        break;
                }
            }
        }

        private void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Administrator menu");
                Console.WriteLine("1.Car menu");
                Console.WriteLine("2.Driver menu");
                Console.WriteLine("3.Order menu");
                Console.WriteLine("4.Return to the main menu");
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsMenu.Car:
                        {
                            CarMenu();
                        }
                        break;

                    case (int)AdminsMenu.Driver:
                        {
                            DriverMenu();
                        }
                        break;

                    case (int)AdminsMenu.Order:
                        {
                            OrderMenu();
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

        private void CarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Car menu");
                Console.WriteLine("1.Show all cars");
                Console.WriteLine("2.Add car");
                Console.WriteLine("3.Delete car");
                Console.WriteLine("4.Update car");
                Console.WriteLine("5.Car on repair");
                Console.WriteLine("6.Find car by government number");
                Console.WriteLine("7.Get old cars");
                Console.WriteLine("8.Return to the administrator menu");
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsCarMenu.AllCar:
                        {
                            ConsoleHelper.ShowCars(_carProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsCarMenu.AddCar:
                        {
                            var car = ConsoleHelper.CreateCar();
                            var validResults = car.IsValid();
                            if (validResults.Count() != NoError)
                            {
                                foreach (var result in validResults)
                                {
                                    Console.WriteLine(result.ErrorMessage);
                                }
                            }
                            else
                            {
                                if (_carProcessing.UniquenessCheck(car))
                                {
                                    _carProcessing.AddCar(car);
                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("This car is already on the list");
                                }
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsCarMenu.DeleteCar:
                        {
                            Console.WriteLine("Enter car id");
                            bool isDelete = _carProcessing.DeleteCar(ConsoleHelper.EnterNumber());
                            if (isDelete)
                            {
                                Console.WriteLine("Delete is sucessfull");
                            }
                            else
                            {
                                Console.WriteLine("Car is not find");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsCarMenu.UpdateCar:
                        {
                        }
                        break;

                    case (int)AdminsCarMenu.OnRepair:
                        {
                            ConsoleHelper.ShowCars(_carProcessing.CarOnRepair());
                        }
                        break;

                    case (int)AdminsCarMenu.Find:
                        {
                            var car = _carProcessing.FindByGovernmentNumber(Console.ReadLine());
                            if (car != null)
                            {
                                Console.WriteLine("Goverment number | Model | Color | Registration number | Year of issue | Is repair");
                                Console.WriteLine($"{car.GovernmentNumber} | {car.Model} | {car.Color} | {car.RegistrationNumber} | {car.YearOfIssue} | {car.IsRepair}");
                            }
                            else
                            {
                                Console.WriteLine("Car is not find");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsCarMenu.OldCars:
                        {
                            Console.Clear();
                            Console.WriteLine("Max age:");
                            ConsoleHelper.ShowCars(_carProcessing.GetOldCars(ConsoleHelper.EnterNumber()));
                        }
                        break;

                    case (int)AdminsCarMenu.AdminMenu:
                        {
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect number");
                        }
                        break;
                }
            }
        }

        private void DriverMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Driver menu");
                Console.WriteLine("1.Show all driver");
                Console.WriteLine("2.Add driver");
                Console.WriteLine("3.Delete driver");
                Console.WriteLine("4.Update driver");
                Console.WriteLine("5.Find by license number");
                Console.WriteLine("6.Give car");
                Console.WriteLine("7.Back to the administrator menu");
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsDriverMenu.AllDriver:
                        {
                            ConsoleHelper.ShowDrivers(_driverProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsDriverMenu.AddDriver:
                        {
                            var driver = ConsoleHelper.CreateDriver();
                            var validResults = driver.IsValid();
                            if (validResults.Count() != NoError)
                            {
                                foreach (var result in validResults)
                                {
                                    Console.WriteLine(result.ErrorMessage);
                                }
                            }
                            else
                            {
                                if (_driverProcessing.UniquenessCheck(driver))
                                {
                                    _driverProcessing.AddDriver(driver);
                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("This driver is already on the list");
                                }
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsDriverMenu.DeleteDriver:
                        {
                            Console.WriteLine("Enter driver id");
                            bool isDelete = _driverProcessing.DeleteDriver(ConsoleHelper.EnterNumber());
                            if (isDelete)
                            {
                                Console.WriteLine("Delete is sucessfull");
                            }
                            else
                            {
                                Console.WriteLine("Driver is not find");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsDriverMenu.UpdateDriver:
                        {
                        }
                        break;

                    case (int)AdminsDriverMenu.Find:
                        {
                            Console.Clear();
                            Console.WriteLine("Enter licence number:");
                            var driver = _driverProcessing.FindByDriverLicenseNumber(Console.ReadLine());
                            if (driver == null)
                            {
                                Console.WriteLine("Licence number not find");
                            }
                            else
                            {
                                Console.WriteLine($"Surname | Name | Patronymic | Call sign | DriverLicenseNumber | Date of issue of drivers license | Is on holiday | Is sick leave");
                                Console.WriteLine($"{driver.Surname} | {driver.Name} | {driver.Patronymic} | {driver.CallSign} | {driver.DriverLicenseNumber} | {driver.DateOfIssueOfDriversLicense} | {driver.IsOnHoliday} | {driver.IsSickLeave}");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsDriverMenu.GiveCar:
                        {
                            Console.Clear();
                            Console.WriteLine("Enter car id");
                            var carId = ConsoleHelper.EnterNumber();
                            if (_carProcessing.FindById(carId) != null)
                            {
                                Console.WriteLine("Enter driver id");
                                var driverId = ConsoleHelper.EnterNumber();
                                if (_driverProcessing.FindById(driverId) != null)
                                {
                                    if (_driverProcessing.GiveCar(driverId, carId))
                                    {
                                        Console.WriteLine("Sucessfull");
                                    }
                                    else
                                    {
                                        Console.WriteLine("This car is use");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Driver not find");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Car not find");
                            }
                            Console.ReadKey();
                        }
                        break;

                    case (int)AdminsDriverMenu.AdminMenu:
                        {
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect number");
                        }
                        break;
                }
            }
        }

        private void OrderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Order menu");
                Console.WriteLine("1.Show all orders");
                Console.WriteLine("2.Show active orders");
                Console.WriteLine("3.Show inactive orders");
                Console.WriteLine("4.Add order");
                Console.WriteLine("5.Back to administrator menu");
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsOrderMenu.ShowAll:
                        {
                            ConsoleHelper.ShowOrders(_orderProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsOrderMenu.ShowActive:
                        {
                            ConsoleHelper.ShowOrders(_orderProcessing.ActiveOrders());
                        }
                        break;

                    case (int)AdminsOrderMenu.ShowInactive:
                        {
                            ConsoleHelper.ShowOrders(_orderProcessing.InActiveOrders());
                        }
                        break;

                    case (int)AdminsOrderMenu.Add:
                        {
                            var order = ConsoleHelper.CreateOrder();
                            var validResults = order.IsValid();
                            if (validResults.Count() != NoError)
                            {
                                foreach (var result in validResults)
                                {
                                    Console.WriteLine(result.ErrorMessage);
                                }
                            }
                            else
                            {
                                if (_orderProcessing.AddOrder(order))
                                {
                                    Console.WriteLine("Success");
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect client id");
                                }
                                Console.ReadKey();
                            }
                        }
                        break;

                    case (int)AdminsOrderMenu.AdminMenu:
                        {
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect number");
                        }
                        break;
                }
            }
        }
    }
}