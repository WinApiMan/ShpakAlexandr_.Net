using System.Threading.Tasks;

namespace Taxi.ConsoleUI.Interfaces
{
    public interface IConsoleService<T> where T : class
    {
        void PrintMenu();

        Task StartMenu();
    }
}