using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projectManagementTool.Data;
using projectManagementToolUser.DomainModel;

[assembly: HostingStartup(typeof(projectManagementTool.Areas.Identity.IdentityHostingStartup))]
namespace projectManagementTool.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ProjectManagementToolContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("ProjectManagementToolContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ProjectManagementToolContext>();
            });
        }
    }
}