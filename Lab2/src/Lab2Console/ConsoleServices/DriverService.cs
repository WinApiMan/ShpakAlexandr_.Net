using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Validations;
using Taxi.ConsoleUI.Enums;
using Taxi.ConsoleUI.Interfaces;

namespace Taxi.ConsoleUI.ConsoleServices
{
    public class DriverService : IConsoleService<DriverService>
    {
        private readonly BusinessLogic.Processings.DriverService _driverProcessing;

        private readonly BusinessLogic.Processings.CarService _carProcessing;

        public DriverService(BusinessLogic.Processings.DriverService driverProcessing, BusinessLogic.Processings.CarService carProcessing)
        {
            _driverProcessing = driverProcessing;
            _carProcessing = carProcessing;
        }

        public void PrintMenu()
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
        }

        public async Task StartMenu()
        {
            while (true)
            {
                PrintMenu();
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsDriverMenu.AllDriver:
                        {
                            ConsoleHelper.ShowDrivers(await _driverProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsDriverMenu.AddDriver:
                        {
                            var driver = ConsoleHelper.CreateDriver();
                            var validResults = driver.IsValid();
                            const int NoError = 0;
                            if (validResults.Count() != NoError)
                            {
                                foreach (var result in validResults)
                                {
                                    Console.WriteLine(result.ErrorMessage);
                                }
                            }
                            else
                            {
                                if (await _driverProcessing.UniquenessCheck(driver))
                                {
                                    await _driverProcessing.AddDriver(driver);
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

                            try
                            {
                                await _driverProcessing.DeleteDriver(ConsoleHelper.EnterNumber());
                                Console.WriteLine("Delete is sucessfull");
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Driver is not find");
                            }
                            catch (DbException)
                            {
                                Console.WriteLine("Find error");
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
                            var driver = await _driverProcessing.FindByDriverLicenseNumber(Console.ReadLine());
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
                            if (await _carProcessing.FindById(carId) != null)
                            {
                                Console.WriteLine("Enter driver id");
                                var driverId = ConsoleHelper.EnterNumber();
                                if (_driverProcessing.FindById(driverId) != null)
                                {
                                    try
                                    {
                                        await _driverProcessing.GiveCar(driverId, carId);
                                        Console.WriteLine("Sucessfull");
                                    }
                                    catch (DbUpdateException)
                                    {
                                        Console.WriteLine("This car is use");
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Update error");
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
    }
}