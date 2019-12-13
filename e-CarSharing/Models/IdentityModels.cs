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

        //[MaxLength(20)]
        //[Display(Name = "Telemóvel")]
        //public int PhoneNumber { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        [MaxLength(20)]
        [Display(Name = "Número da Carta de Condução")]
        public string DriverLicenseNumber { get; set; }

        [Display(Name = "Emissão da Carta de Condução")]
        public DateTime? DriverLicenseEmissionDate { get; set; }

        [Display(Name = "Validade da Carta de Condução")]
        public DateTime? DriverLicenseEndDate { get; set; }

        public ApplicationUser() : base()
        {
            //VehicleLocalizations = new HashSet<VehicleLocalization>();
            //Vehicles = new HashSet<Vehicle>();
        }
    }

}