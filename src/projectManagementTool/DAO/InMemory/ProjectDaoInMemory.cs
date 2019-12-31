using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using projectManagementTool.DomainModel;

namespace projectManagementTool.DAO.InMemory
{
	public class ProjectDaoInMemory : IProjectDao
	{
		private readonly List<Project> projects;
		private readonly IdProvider idProvider;

		public ProjectDaoInMemory()
		{
			this.projects = new List<Project>();
			this.idProvider = new IdProvider();
		}

		public Task<Project> CreateProjectAsync(Project project)
		{
			var addedProject = new Project(this.idProvider.GenerateId(), project.Name, project.Description);
			this.projects.Add(addedProject);
			return Task.FromResult(addedProject);
		}

		public Task DeleteProjectAsync(Project project)
		{
			var removedProject = this.projects.Find(existingProject => existingProject.Id == project.Id);
			this.projects.Remove(removedProject);
			return Task.CompletedTask;
		}

		public Task<List<Project>> FindAllProjectsAsync()
		{
			return Task.FromResult(this.projects);
		}

		public Task<Project> FindByIdAsync(int id)
		{
			return Task.FromResult(
				this.projects.Find(project => project.Id == id)
			);
		}

		public Task<Project> UpdateProjectAsync(Project project)
		{
			var removedProject = this.projects.Find(existingProject => existingProject.Id == project.Id);
			if (removedProject != null)
			{
				this.projects.Remove(removedProject);
				var addedProject = new Project(this.idProvider.GenerateId(), project.Name, project.Description);
				this.projects.Add(addedProject);
				return Task.FromResult(addedProject);
			}
			else
			{
				throw new KeyNotFoundException("The project to update has not been persisted.");
			}
		}
	}
}
