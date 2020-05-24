using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Validations;
using Taxi.ConsoleUI.Enums;
using Taxi.ConsoleUI.Interfaces;

namespace Taxi.ConsoleUI.ConsoleServices
{
    public class CarService : IConsoleService<CarService>
    {
        private readonly BusinessLogic.Processings.CarService _carProcessing;

        public CarService(BusinessLogic.Processings.CarService carProcessing)
        {
            _carProcessing = carProcessing;
        }

        public void PrintMenu()
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
        }

        public async Task StartMenu()
        {
            while (true)
            {
                PrintMenu();
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsCarMenu.AllCar:
                        {
                            ConsoleHelper.ShowCars(await _carProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsCarMenu.AddCar:
                        {
                            var car = ConsoleHelper.CreateCar();
                            var validResults = car.IsValid();
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
                                if (await _carProcessing.UniquenessCheck(car))
                                {
                                    await _carProcessing.AddCar(car);
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
                            try
                            {
                                await _carProcessing.DeleteCar(ConsoleHelper.EnterNumber());
                                Console.WriteLine("Delete is sucessfull");
                            }
                            catch (DbException)
                            {
                                Console.WriteLine("Failed to remove the car");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Delete error");
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
                            ConsoleHelper.ShowCars(await _carProcessing.CarOnRepair());
                        }
                        break;

                    case (int)AdminsCarMenu.Find:
                        {
                            try
                            {
                                var car = await _carProcessing.FindByGovernmentNumber(Console.ReadLine());
                                Console.WriteLine("Goverment number | Model | Color | Registration number | Year of issue | Is repair");
                                Console.WriteLine($"{car.GovernmentNumber} | {car.Model} | {car.Color} | {car.RegistrationNumber} | {car.YearOfIssue} | {car.IsRepair}");
                            }
                            catch (Exception)
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
                            ConsoleHelper.ShowCars(await _carProcessing.GetOldCars(ConsoleHelper.EnterNumber()));
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
    }
}