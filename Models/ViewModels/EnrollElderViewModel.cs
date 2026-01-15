using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElderCareApp.Models.ViewModels
{
    public class EnrollElderViewModel
    {
        public EnrollElderDto Elder { get; set; }
[ValidateNever]
    public IEnumerable<SelectListItem> CareHomes { get; set; }
    }
}