using System;
using System.Data.Common;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using projectManagementTool.BL.Implementation;
using projectManagementTool.DAO.InMemory;
using projectManagementTool.DAO.AdoNet;
using Microsoft.Extensions.Configuration;

namespace projectManagementTool.BL.Extension
{
	public static class AspNetCoreExtensions
	{
		public static void AddInMemoryBusinessLogic(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton(typeof(IProjectManager), (IServiceProvider serviceProvider) =>
			{
				var projectDaoInMemory = new ProjectDaoInMemory();
				return new ProjectManagerImplementation(projectDaoInMemory);
			});
		}

		public static void AddAdoNetBusinessLogic(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			Type factoryType = Type.GetType($"{configuration.GetValue<string>("DatabaseProvider:ConnectionProvider")}, {configuration.GetValue<string>("DatabaseProvider:AssemblyName")}");
			FieldInfo factoryInstanceField = factoryType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
			DbProviderFactory factoryInstance = ((DbProviderFactory) factoryInstanceField.GetValue(null));
			var adoNetConnectionFactory = new DefaultConnectionFactory(factoryInstance, configuration.GetValue<string>("DatabaseProvider:ConnectionProvider"), configuration.GetValue<string>("DatabaseProvider:ConnectionString"));

			serviceCollection.AddSingleton(typeof(IProjectManager), (IServiceProvider serviceProvider) =>
			{
				var projectDaoAdoNet = new ProjectDaoAdoNet(adoNetConnectionFactory);
				return new ProjectManagerImplementation(projectDaoAdoNet);
			});
		}
	}
}
