using BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taxi.BusinessLogic.Interfaces
{
    public interface IOrderService : IManager<Order>
    {
        Task<IEnumerable<Order>> GetActiveOrders();

        Task<IEnumerable<Order>> GetInActiveOrders();
    }
}