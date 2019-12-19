using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_CarSharing.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //geral
        public System.Web.Mvc.SelectList Roles { get; set; }

        [Display(Name = "Tipo de Conta")]
        [Required(ErrorMessage = "Escolha um tipo de conta")]
        public string UserRole { get; set; }

        [Display(Name = "Userame")]
        [Required(ErrorMessage = "Insira o UserName")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Insira o Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira a password")]
        [StringLength(100, ErrorMessage = "A {0} tem de ser no mínimo de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "As passwords são diferentes")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Número de Telefone")]
        [Required(ErrorMessage = "Insira o número de telefone")]
        public string PhoneNumber { get; set; }

        //profissional
        [Display(Name = "Nome da Empresa")]
        public string CompanyName { get; set; }

        [Display(Name = "Morada")]
        public string Address { get; set; }

        //privado//mobildiade
        [Display(Name = "Número do Cartão de Cidadão")]
        public int CC { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        //mobilidade apenas
        [MaxLength(10)]
        [Display(Name = "Número da Carta de Condução")]
        public string DriverLicenseNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
