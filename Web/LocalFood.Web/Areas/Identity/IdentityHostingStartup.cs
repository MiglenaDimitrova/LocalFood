using LocalFood.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LocalFood.Web.Areas.Identity.IdentityHostingStartup))]

namespace LocalFood.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<LocalFoodWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LocalFoodWebContextConnection")));

                // services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<LocalFoodWebContext>();
            });
        }
    }
}