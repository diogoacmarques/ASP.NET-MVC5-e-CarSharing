using e_CarSharing.Models;
namespace e_CarSharing.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<e_CarSharing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "e_CarSharing.Models.ApplicationDbContext";
        }

        protected override void Seed(e_CarSharing.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            if (context.Users.Where(u => u.UserName == "Admin").Count() == 0)//make sure that exists a admin
            {
                //local admin
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@echarsharing.pt",
                    UserRole = AccountStaticRoles.ADMINISTRATOR,
                };
                userManager.Create(user, "Admin123_");
                userManager.AddToRole(user.Id, AccountStaticRoles.ADMINISTRATOR);
                context.SaveChanges();
            }

            var VehicleSateList = VehicleState.GetStatesList();
            foreach (var s in VehicleSateList)
            {
                bool Result = int.TryParse(s.Value, out int Id);
                if (Result)
                {
                    context.VehicleStates.AddOrUpdate(x => x.VehicleStateId,
                    new VehicleState { VehicleStateId = Id, VehicleStateName = s.Text });
                }
            }

            var RentSateList = RentState.GetStatesList();
            foreach (var r in RentSateList)
            {
                bool Result = int.TryParse(r.Value, out int IdState);
                if (Result)
                {
                    //context.RentStates.AddOrUpdate(x => x.RentStateId,
                    //new RentState { RentStateId = IdState, RentStateName = r.Text });
                }
            }

            //context.RentStates.Add(new RentState { RentStateName = "teste" });
            context.RentStates.Add(new RentState { RentStateId = 14 });
        }
    }
}