using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Dto
{
    public class RegisterCareHomeDto
    {
        [Required(ErrorMessage = "Care home name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Care home name must contain only letters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manager name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Manager name must contain only letters")]
        public string ManagerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manager email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string ManagerEmail { get; set; } = string.Empty;

        public string Password { get; set; }
        public string BankName { get; set; }
    }
}