﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.WebUI.ViewModels
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        public int? CarId { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Call sing must be between 1 and 1000")]
        public int CallSign { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname length must be between 2 and 30")]
        public string Surname { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name length must be between 2 and 30")]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Patronymic length must be between 2 and 30")]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Driver license number must be between 5 and 30")]
        public string DriverLicenseNumber { get; set; }

        [Required]
        public DateTime DateOfIssueOfDriversLicense { get; set; }

        [Required]
        public bool IsSickLeave { get; set; }

        [Required]
        public bool IsOnHoliday { get; set; }

        public CarViewModel Car { get; set; }
    }
}
