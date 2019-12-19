using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_CarSharing.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
      
        [Display(Name = "Função")]
        public string UserRole { get; set; }

        [Display(Name = "Nome da Empresa")]
        public string CompanyName { get; set; }

        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

       // [MaxLength(10)]
        [Display(Name = "Número da Carta de Condução")]
        public string DriverLicenseNumber { get; set; }

        [Display(Name = "Número do Cartão de Cidadão")]
       // [Range(8, 8, ErrorMessage = "Insira um número de 8 digitos")]
        public int CC { get; set; }


        public ApplicationUser() : base()
        {
            //Vehicles = new HashSet<Vehicle>();
        }
    }

}