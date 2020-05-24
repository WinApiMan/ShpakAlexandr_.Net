using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Validations;
using Taxi.ConsoleUI.Enums;
using Taxi.ConsoleUI.Interfaces;

namespace Taxi.ConsoleUI.ConsoleServices
{
    public class OrderService : IConsoleService<OrderService>
    {
        private readonly BusinessLogic.Processings.OrderService _orderProcessing;

        public OrderService(BusinessLogic.Processings.OrderService orderProcessing)
        {
            _orderProcessing = orderProcessing;
        }

        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Order menu");
            Console.WriteLine("1.Show all orders");
            Console.WriteLine("2.Show active orders");
            Console.WriteLine("3.Show inactive orders");
            Console.WriteLine("4.Add order");
            Console.WriteLine("5.Back to administrator menu");
        }

        public async Task StartMenu()
        {
            while (true)
            {
                PrintMenu();
                int.TryParse(Console.ReadLine(), out int menuNumber);
                switch (menuNumber)
                {
                    case (int)AdminsOrderMenu.ShowAll:
                        {
                            ConsoleHelper.ShowOrders(await _orderProcessing.GetAll());
                        }
                        break;

                    case (int)AdminsOrderMenu.ShowActive:
                        {
                            ConsoleHelper.ShowOrders(await _orderProcessing.ActiveOrders());
                        }
                        break;

                    case (int)AdminsOrderMenu.ShowInactive:
                        {
                            ConsoleHelper.ShowOrders(await _orderProcessing.InActiveOrders());
                        }
                        break;

                    case (int)AdminsOrderMenu.Add:
                        {
                            const int NoError = 0;
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
                                try
                                {
                                    await _orderProcessing.AddOrder(order);
                                    Console.WriteLine("Success");
                                }
                                catch (DbException)
                                {
                                    Console.WriteLine("Incorrect client id");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Adding error");
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