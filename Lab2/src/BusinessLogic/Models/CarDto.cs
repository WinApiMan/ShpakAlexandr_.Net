using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class CarDto
    {
        public CarDto(string governmentNumber, string model, string color, int yearOfIssue, string registrationNumber, bool isRepair)
        {
            GovernmentNumber = governmentNumber;
            Model = model;
            Color = color;
            YearOfIssue = yearOfIssue;
            RegistrationNumber = registrationNumber;
            IsRepair = isRepair;
        }

        public CarDto(int id, string governmentNumber, string model, string color, int yearOfIssue, string registrationNumber, bool isRepair)
        {
            Id = id;
            GovernmentNumber = governmentNumber;
            Model = model;
            Color = color;
            YearOfIssue = yearOfIssue;
            RegistrationNumber = registrationNumber;
            IsRepair = isRepair;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Number length must be between 8 and 15")]
        public string GovernmentNumber { get; set; }

        [DataMember]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Model length must be between 2 and 30")]
        public string Model { get; set; }

        [DataMember]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Color length must be between 3 and 20")]
        public string Color { get; set; }

        [DataMember]
        [Required]
        [Range(1900, 2100, ErrorMessage = "Year of issue must be between 1900 and 2100")]
        public int YearOfIssue { get; set; }

        [DataMember]
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Registration number length must be between 6 and 20")]
        public string RegistrationNumber { get; set; }

        [DataMember]
        [Required]
        public bool IsRepair { get; set; }
    }
}