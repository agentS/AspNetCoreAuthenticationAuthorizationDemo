using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using projectManagementTool.Data;

namespace projectManagementTool
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var context = services.GetRequiredService<ProjectManagementToolContext>();
					context.Database.Migrate();

					var configuration = host.Services.GetRequiredService<IConfiguration>();

					var testUserPassword = configuration["SeedUserPW"];
					SeedData.Initialize(services, testUserPassword).Wait();
				}
				catch (Exception exception)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(exception, "An error occurred seeding the DB.");
				}
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
