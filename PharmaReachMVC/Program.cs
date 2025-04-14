using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC
{
    public class Program
    {
        public static async Task Main(string[] args)   // <<== make Main async
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext
            builder.Services.AddDbContext<PharmaReachDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PharmaReachDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed roles here
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = { "Customer", "Pharmacy" };

                foreach (var role in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Medicines}/{action=Index}");

            app.Run();
        }
    }
}
