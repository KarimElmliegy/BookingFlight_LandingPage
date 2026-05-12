using Booking.Core.Models.Booking;
using Booking.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Booking.Core.Data
{
    public static class SeedDataAsynch
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                await roleManager.CreateAsync(new ApplicationRole { Name = "User" });
            }

            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    DisplayName = "Karim Ayman",
                    UserName = "Admin",
                    Email = "Admin@booking.com"
                };

                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!context.Trips.Any())
            {
                var trips = new List<Trip>
                {
                    new Trip
                    {
                        FromCity = "Alexandria",
                        ToCity = "Istanbul",
                        Price = 7000,
                        ImageUrl = "France.webp"
                    },
                
                    new Trip
                    {
                        FromCity = "Cairo",
                        ToCity = "Paris",
                        Price = 12000,
                        ImageUrl = "Paris.jpg"
                    },
                
                    new Trip
                    {
                        FromCity = "Cairo",
                        ToCity = "France",
                        Price = 12322,
                        ImageUrl = "string"
                    },
                
                    new Trip
                    {
                        FromCity = "Cairo",
                        ToCity = "Rome",
                        Price = 15000,
                        ImageUrl = "Egypt.jpg"
                    }
                };

                await context.Trips.AddRangeAsync(trips);
                await context.SaveChangesAsync();
            }
        }
    }
}