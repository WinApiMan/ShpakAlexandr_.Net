using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taxi.BusinessLogic.Interfaces
{
    public interface IManager<T>
    {
        Task Add(T buyerDto);

        Task Delete(int id);

        Task Update(T buyerDto);

        Task<IEnumerable<T>> GetAll();

        Task<T> FindById(int id);
    }
}