using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectManagementTool.Areas.Identity.Data;
using projectManagementTool.Models;
using projectManagementTool.DomainModel;
using projectManagementTool.BL;

namespace projectManagementTool.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IProjectManager projectManager;

		private IAuthorizationService authorizationService;

		public ProjectController(
			IProjectManager projectManager,
			IAuthorizationService authorizationService
		)
		{
			this.projectManager = projectManager;

			this.authorizationService = authorizationService;
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
		public async Task<IActionResult> Create()
		{
			var model = new ProjectViewModel();
			
			var authorized = await this.authorizationService.AuthorizeAsync(
				this.User, model, ProjectOperations.Create
			);
			if (!authorized.Succeeded)
			{
				return this.Forbid();
			}
			
			return this.View(model);
		}

		[HttpPost("/createProject")]
		public async Task<IActionResult> CreateProject(ProjectViewModel project)
		{
			if (!this.ModelState.IsValid)
			{
				return this.Redirect("/create");
			}
			
			var authorized = await this.authorizationService.AuthorizeAsync(
				this.User, project, ProjectOperations.Create
			);
			if (!authorized.Succeeded)
			{
				return this.Forbid();
			}
			
			var createdProject = await this.projectManager.CreateProjectAsync(
				new Project(project.Name, project.Description)
			);
			return this.Redirect("/");
		}

		[HttpGet("/update")]
		public async Task<IActionResult> Update(int projectId)
		{
			if (!this.ModelState.IsValid)
			{
				return this.Redirect("/error");
			}

			var project = await this.projectManager.FindByIdAsync(projectId);
			if (project != null)
			{
				var model = new ProjectViewModel(project.Id, project.Name, project.Description);
			
				var authorized = await this.authorizationService.AuthorizeAsync(
					this.User, model, ProjectOperations.Update
				);
				if (!authorized.Succeeded)
				{
					return this.Forbid();
				}
				
				return this.View(model);
			}
			else
			{
				throw new KeyNotFoundException($"There is no project with ID {projectId}!");
			}
		}

		[HttpPost("/updateProject")]
		public async Task<IActionResult> UpdateProject(ProjectViewModel project, int projectId)
		{
			if (!this.ModelState.IsValid)
			{
				return this.Redirect("/error");
			}
			
			var authorized = await this.authorizationService.AuthorizeAsync(
				this.User, project, ProjectOperations.Update
			);
			if (!authorized.Succeeded)
			{
				return this.Forbid();
			}
			
			var updatedProject = await this.projectManager.UpdateProjectAsync(
				new Project(projectId, project.Name, project.Description)
			);
			return this.Redirect("/");
		}

		[HttpGet("/delete")]
		public async Task<IActionResult> Delete(int projectId)
		{
			if (!this.ModelState.IsValid)
			{
				return this.Redirect("/error");
			}
			
			var projectToDelete = await this.projectManager.FindByIdAsync(projectId);
			var model = new ProjectViewModel(projectToDelete.Id, projectToDelete.Name, projectToDelete.Description);
			
			var authorized = await this.authorizationService.AuthorizeAsync(
				this.User, model, ProjectOperations.Delete
			);
			if (!authorized.Succeeded)
			{
				return this.Forbid();
			}
			
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
