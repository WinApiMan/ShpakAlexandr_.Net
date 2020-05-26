using System.Threading.Tasks;

namespace Taxi.ConsoleUI.Interfaces
{
    public interface IConsoleService<T>
    {
        void PrintMenu();

        Task StartMenu();
    }
}