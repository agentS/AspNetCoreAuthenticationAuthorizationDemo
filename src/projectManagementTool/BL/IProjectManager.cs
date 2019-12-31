using System.Collections.Generic;
using System.Threading.Tasks;

using projectManagementTool.DomainModel;

namespace projectManagementTool.BL
{
	public interface IProjectManager
	{
		
		Task<List<Project>> FindAllProjectsAsync();
		Task<Project> FindByIdAsync(int id);
		Task<Project> CreateProjectAsync(Project project);
		Task<Project> UpdateProjectAsync(Project project);
		Task DeleteProjectAsync(Project project);
	}
}
