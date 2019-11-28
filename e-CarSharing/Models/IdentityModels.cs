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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<City> Cities { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Colour> Colours { get; set; }
        //public DbSet<FuelType> FuelTypes { get; set; }
        //public DbSet<VehicleDoor> VehicleDoors { get; set; }
        public DbSet<VehiclePassengers> VehicleSeats { get; set; }
       // public DbSet<VehicleLocalization> VehicleLocalizations { get; set; }
        //public DbSet<NumberTransmission> NumberTransmissions { get; set; }
        //public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Model> Models { get; set; }
        //public DbSet<UsersState> UsersStates { get; set; }
        //public DbSet<Message> Messages { get; set; }
        //public DbSet<MessageState> MessageStates { get; set; }
        //public DbSet<SupplierEvaluation> SupplierEvaluations { get; set; }
        //public DbSet<DemanderEvaluation> DemanderEvaluations { get; set; }
        //public DbSet<VehicleEvaluation> VehicleEvaluations { get; set; }
        public DbSet<VehicleState> VehicleStates { get; set; }
        public DbSet<Rent> Rents { get; set; }
        //public DbSet<RentState> RentStates { get; set; }
        //public DbSet<RentDemanderEvaluation> RentDemanderEvaluations { get; set; }
        //public DbSet<RentSupplierEvaluation> RentSupplierEvaluations { get; set; }
        //public DbSet<RentVehicleEvaluation> RentVehicleEvaluations { get; set; }
        public DbSet<RentCondition> RentConditions { get; set; }
        //public DbSet<VehiclePhoto> VehiclePhotos { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}