using Ninject;
using System;
using System.Threading.Tasks;
using Taxi.ConsoleUI.Configuration;
using Taxi.ConsoleUI.Interfaces;
using Taxi.ConsoleUI.Services;

namespace Taxi.ConsoleUI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new NinjectConfiguration());
            IConsoleService<RoleService> roleInterface = kernel.Get<RoleService>();
            await roleInterface.StartMenu();
            Console.ReadKey();
        }
    }
}