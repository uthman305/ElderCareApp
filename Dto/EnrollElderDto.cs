using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElderCareApp.Dto
{
    public class EnrollElderDto
    {
        [Required(ErrorMessage = "Full name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain letters only")]
    public string FullName { get; set; } = string.Empty;

      [Required]
    [Range(60, 120, ErrorMessage = "Only elders aged 60 and above can be enrolled")]
    public int Age { get; set; }

    

    [Required(ErrorMessage = "Care Home is required")]
    public int CareHomeId { get; set; }
    }
}