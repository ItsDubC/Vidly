using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Drivers License")]
        public string DriversLicense { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Phone")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}