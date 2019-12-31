using System.Collections.Generic;
using System.Threading.Tasks;

using projectManagementTool.DomainModel;
using projectManagementTool.DAO;

namespace projectManagementTool.BL.Implementation
{
	public sealed class ProjectManagerImplementation : IProjectManager
	{
		private readonly IProjectDao projectDao;

		public ProjectManagerImplementation(IProjectDao projectDao)
		{
			this.projectDao = projectDao;
		}

		public Task<Project> CreateProjectAsync(Project project)
		{
			return this.projectDao.CreateProjectAsync(project);
		}

		public Task DeleteProjectAsync(Project project)
		{
			return this.projectDao.DeleteProjectAsync(project);
		}

		public Task<List<Project>> FindAllProjectsAsync()
		{
			return this.projectDao.FindAllProjectsAsync();
		}

		public Task<Project> FindByIdAsync(int id)
		{
			return this.projectDao.FindByIdAsync(id);
		}

		public Task<Project> UpdateProjectAsync(Project project)
		{
			return this.projectDao.UpdateProjectAsync(project);
		}
	}
}
