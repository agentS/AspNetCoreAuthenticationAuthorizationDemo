using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using projectManagementTool.BL.Implementation;
using projectManagementTool.DAO.InMemory;

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
	}
}
