using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_CarSharing.Areas.Administration.Models
{
    public class CreateAccount
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(dataType: DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas inseridas não correspondem.")]
        public string ConfirmPassword { get; set; }
    }
}