using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class OrderDto
    {
        public OrderDto(DateTime date, bool isDone, double cost, double distance, double discount, int? driverId, int clientId)
        {
            Date = date;
            IsDone = isDone;
            Cost = cost;
            Distance = distance;
            Discount = discount;
            DriverId = driverId;
            ClientId = clientId;
        }

        public OrderDto(int id, DateTime date, bool isDone, double cost, double distance, double discount, int? driverId, int clientId)
        {
            Id = id;
            Date = date;
            IsDone = isDone;
            Cost = cost;
            Distance = distance;
            Discount = discount;
            DriverId = driverId;
            ClientId = clientId;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [Range(typeof(DateTime), "1/1/1950", "1/1/2100", ErrorMessage = "Date is out of Range")]
        public DateTime Date { get; set; }

        [DataMember]
        [Required]
        public bool IsDone { get; set; }

        [DataMember]
        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Minimum cost = 1.0")]
        public double Cost { get; set; }

        [DataMember]
        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Minimum distance = 1.0")]
        public double Distance { get; set; }

        [DataMember]
        [Required]
        [Range(0, double.MaxValue / 2, ErrorMessage = "Minimum discount = 0.0")]
        public double Discount { get; set; }

        [DataMember]
        public int? DriverId { get; set; }

        [DataMember]
        [Required]
        public int ClientId { get; set; }
    }
}