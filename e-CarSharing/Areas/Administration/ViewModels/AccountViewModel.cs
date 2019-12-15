using e_CarSharing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Areas.Administration.ViewModels
{
    public class Account
    {
        public string Id { get; set; }

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
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "As senhas inseridas não correspondem.")]
        public string ConfirmPassword { get; set; }

        public string Type { get; set; }
    }

    public class SearchAccoutViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public string UserID { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string UserEmail { get; set; }


        [Display(Name = "Roles")]
        public SelectList Roles { get; set; }

        [Display(Name = "Função")]
        public string Role { get; set; }

    }
}