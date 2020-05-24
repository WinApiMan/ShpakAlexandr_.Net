using System.Collections.Generic;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class ClientDto:IEntity
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public virtual ICollection<OrderDto> Orders { get; set; }
    }
}