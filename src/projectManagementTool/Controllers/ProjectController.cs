using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectManagementTool.Models;
using projectManagementTool.DomainModel;
using projectManagementTool.BL;

namespace projectManagementTool.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IProjectManager projectManager;

		public ProjectController(IProjectManager projectManager)
		{
			this.projectManager = projectManager;
		}

		[HttpGet("/")]
		public async Task<IActionResult> Index()
		{
			var projects = (await this.projectManager.FindAllProjectsAsync())
				.Select(project => new ProjectViewModel(project.Id, project.Name, project.Description))
				.ToList();
			return this.View(projects);
		}

		[HttpGet("/create")]
		public IActionResult Create()
		{
			var model = new ProjectViewModel();
			return this.View(model);
		}

		[HttpPost("/createProject")]
		public async Task<IActionResult> CreateProject(ProjectViewModel project)
		{
			var createdProject = await this.projectManager.CreateProjectAsync(
				new Project(project.Name, project.Description)
			);
			return this.Redirect("/");
		}

		[HttpGet("/update")]
		public async Task<IActionResult> Update(int projectId)
		{
			var project = await this.projectManager.FindByIdAsync(projectId);
			if (project != null)
			{
				return this.View(new ProjectViewModel(project.Id, project.Name, project.Description));
			}
			else
			{
				throw new KeyNotFoundException($"There is no project with ID {projectId}!");
			}
		}

		[HttpPost("/updateProject")]
		public async Task<IActionResult> UpdateProject(ProjectViewModel project, int projectId)
		{
			var updatedProject = await this.projectManager.UpdateProjectAsync(
				new Project(projectId, project.Name, project.Description)
			);
			return this.Redirect("/");
		}

		[HttpGet("/delete")]
		public async Task<IActionResult> Delete(int projectId)
		{
			var projectToDelete = await this.projectManager.FindByIdAsync(projectId);
			if (projectToDelete != null)
			{
				await this.projectManager.DeleteProjectAsync(projectToDelete);
			}
			else
			{
				throw new KeyNotFoundException($"There is no project with ID {projectId}!");
			}
			return this.Redirect("/");
		}
	}
}
