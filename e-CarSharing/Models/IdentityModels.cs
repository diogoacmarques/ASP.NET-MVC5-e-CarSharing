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
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //public dbset<brand> brands { get; set; }
    //public dbset<vehicle> vehicles { get; set; }
    ////public dbset<country> countries { get; set; }
    ////public dbset<city> cities { get; set; }
    //public dbset<type> types { get; set; }
    //public dbset<colour> colours { get; set; }
    ////public dbset<fueltype> fueltypes { get; set; }
    ////public dbset<vehicledoor> vehicledoors { get; set; }
    //public dbset<vehiclepassengers> vehicleseats { get; set; }
    //public dbset<location> locations { get; set; }
    ////public dbset<numbertransmission> numbertransmissions { get; set; }
    ////public dbset<transmission> transmissions { get; set; }
    //public dbset<model> models { get; set; }
    ////public dbset<usersstate> usersstates { get; set; }
    ////public dbset<message> messages { get; set; }
    ////public dbset<messagestate> messagestates { get; set; }
    ////public dbset<supplierevaluation> supplierevaluations { get; set; }
    ////public dbset<demanderevaluation> demanderevaluations { get; set; }
    ////public dbset<vehicleevaluation> vehicleevaluations { get; set; }
    //public dbset<vehiclestate> vehiclestates { get; set; }
    //public dbset<rent> rents { get; set; }
    ////public dbset<rentstate> rentstates { get; set; }
    ////public dbset<rentdemanderevaluation> rentdemanderevaluations { get; set; }
    ////public dbset<rentsupplierevaluation> rentsupplierevaluations { get; set; }
    ////public dbset<rentvehicleevaluation> rentvehicleevaluations { get; set; }
    //public dbset<rentcondition> rentconditions { get; set; }
    ////public dbset<vehiclephoto> vehiclephotos { get; set; }


    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }


    //}
}