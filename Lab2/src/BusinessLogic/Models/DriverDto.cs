using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class DriverDto
    {
        public DriverDto(int? carId, int callSign, string surname, string name, string patronymic, string driverLicenseNumber, DateTime dateOfIssueOfDriversLicense, bool isSickLeave, bool isOnHoliday)
        {
            CarId = carId;
            CallSign = callSign;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DriverLicenseNumber = driverLicenseNumber;
            DateOfIssueOfDriversLicense = dateOfIssueOfDriversLicense;
            IsSickLeave = isSickLeave;
            IsOnHoliday = isOnHoliday;
        }

        public DriverDto(int id, int carId, int callSign, string surname, string name, string patronymic, string driverLicenseNumber, DateTime dateOfIssueOfDriversLicense, bool isSickLeave, bool isOnHoliday)
        {
            Id = id;
            CarId = carId;
            CallSign = callSign;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DriverLicenseNumber = driverLicenseNumber;
            DateOfIssueOfDriversLicense = dateOfIssueOfDriversLicense;
            IsSickLeave = isSickLeave;
            IsOnHoliday = isOnHoliday;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? CarId { get; set; }

        [DataMember]
        [Required]
        [Range(1, 1000, ErrorMessage = "Call sing must be between 1 and 1000")]
        public int CallSign { get; set; }

        [DataMember]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname length must be between 2 and 30")]
        public string Surname { get; set; }

        [DataMember]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name length must be between 2 and 30")]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Patronymic length must be between 2 and 30")]
        public string Patronymic { get; set; }

        [DataMember]
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Driver license number must be between 5 and 30")]
        public string DriverLicenseNumber { get; set; }

        [DataMember]
        [Required]
        [Range(typeof(DateTime), "1/1/1950", "1/1/2100", ErrorMessage = "Date is out of Range")]
        public DateTime DateOfIssueOfDriversLicense { get; set; }

        [DataMember]
        [Required]
        public bool IsSickLeave { get; set; }

        [DataMember]
        [Required]
        public bool IsOnHoliday { get; set; }
    }
}