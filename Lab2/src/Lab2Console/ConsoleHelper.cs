using BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace Taxi.ConsoleUI
{
    public static class ConsoleHelper
    {
        public static void ShowCars(IEnumerable<Car> cars)
        {
            Console.Clear();
            Console.WriteLine("Goverment number | Model | Color | Registration number | Year of issue | Is repair");
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.GovernmentNumber} | {car.Model} | {car.Color} | {car.RegistrationNumber} | {car.YearOfIssue} | {car.IsRepair}");
            }
            Console.ReadKey();
        }

        public static void ShowDrivers(IEnumerable<Driver> drivers)
        {
            Console.Clear();
            Console.WriteLine($"Surname | Name | Patronymic | Call sign | DriverLicenseNumber | Date of issue of drivers license | Is on holiday | Is sick leave");
            foreach (var driver in drivers)
            {
                Console.WriteLine($"{driver.Surname} | {driver.Name} | {driver.Patronymic} | {driver.CallSign} | {driver.DriverLicenseNumber} | {driver.DateOfIssueOfDriversLicense} | {driver.IsOnHoliday} | {driver.IsSickLeave}");
            }
            Console.ReadKey();
        }

        public static void ShowOrders(IEnumerable<Order> orders)
        {
            Console.Clear();
            Console.WriteLine($"Cost | Date | Distance | Discount | Is done");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Cost} | {order.Date} | {order.Distance} | {order.Discount} | {order.IsDone}");
            }
            Console.ReadKey();
        }

        public static Car CreateCar()
        {
            Console.Clear();
            Console.WriteLine("Enter government number:");
            string governmentNumber = Console.ReadLine();
            Console.WriteLine("Enter registration number:");
            string registrationNumber = Console.ReadLine();
            Console.WriteLine("Enter model:");
            string model = Console.ReadLine();
            Console.WriteLine("Enter color:");
            string color = Console.ReadLine();
            Console.WriteLine("Enter year of issue:");
            int yearOfIssue = EnterNumber();
            Console.WriteLine("Enter the state of the machine (repair 1, otherwise 0)");
            string chooseState = Console.ReadLine();
            bool isRepair;
            if (chooseState.Equals("1"))
            {
                isRepair = true;
            }
            else
            {
                isRepair = false;
            }
            return new Car(governmentNumber, model, color, yearOfIssue, registrationNumber, isRepair);
        }

        public static Driver CreateDriver()
        {
            Console.Clear();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter surname:");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter patronymic:");
            string patronymic = Console.ReadLine();
            Console.WriteLine("Enter driver license number:");
            string driverLicenseNumber = Console.ReadLine();
            Console.WriteLine("Enter date of issue of drivers license:");
            DateTime.TryParse(Console.ReadLine(), out DateTime dateOfIssueOfDriversLicense);
            Console.WriteLine("Enter call sign:");
            int callSign = EnterNumber();
            Console.WriteLine("Enter the state of holiday (on holiday 1, otherwise 0)");
            string state = Console.ReadLine();
            bool isOnHoliday;
            if (state.Equals("1"))
            {
                isOnHoliday = true;
            }
            else
            {
                isOnHoliday = false;
            }
            Console.WriteLine("Enter the state of sick leave (on sick leave 1, otherwise 0)");
            state = Console.ReadLine();
            bool isSickLeave;
            if (state.Equals("1"))
            {
                isSickLeave = true;
            }
            else
            {
                isSickLeave = false;
            }
            return new Driver(null, callSign, surname, name, patronymic, driverLicenseNumber, dateOfIssueOfDriversLicense, isSickLeave, isOnHoliday);
        }

        public static Order CreateOrder()
        {
            Console.Clear();
            Console.WriteLine("Enter client id:");
            int clientId = EnterNumber();
            Console.WriteLine("Enter cost:");
            double cost = EnterDoubleNumber();
            Console.WriteLine("Enter distance:");
            double distance = EnterDoubleNumber();
            Console.WriteLine("Enter discount");
            double discount = EnterDoubleNumber();
            return new Order(DateTime.Now, false, cost, distance, discount, null, clientId);
        }

        public static int EnterNumber()
        {
            while (true)
            {
                Console.WriteLine("Enter number:");
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    return number;
                }
            }
        }

        public static double EnterDoubleNumber()
        {
            while (true)
            {
                Console.WriteLine("Enter double number:");
                if (double.TryParse(Console.ReadLine(), out double number))
                {
                    return number;
                }
            }
        }
    }
}