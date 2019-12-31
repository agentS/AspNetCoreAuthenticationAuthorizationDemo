using projectManagementTool.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace projectManagementTool.DAO.AdoNet
{
    public class ProjectDaoAdoNet : IProjectDao
    {
        private static readonly string QUERY_FIND_ALL_PROJECTS = "SELECT id, name, description FROM project";
        private static readonly string QUERY_FIND_BY_ID = "SELECT id, name, description FROM project WHERE id = @id";

        private static readonly string STATEMENT_INSERT_PROJECT = "INSERT INTO project(name, description) VALUES(@name, @description)";
        private static readonly string STATEMENT_DELETE_PROJECT = "DELETE FROM project WHERE id = @id";
        private static readonly string STATEMENT_UPDATE_PROJECT = "UPDATE project SET name = @name, description = @description WHERE id = @id";

        private readonly AdoNetTemplate template;

        public ProjectDaoAdoNet(IConnectionFactory connectionFactory)
        {
            this.template = new AdoNetTemplate(connectionFactory);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await this.template.ExecuteStatementAsync(
                STATEMENT_INSERT_PROJECT,
                new Parameter("name", project.Name),
                new Parameter("description", project.Description != null ? project.Description : String.Empty)
            );
            return new Project(
                await this.template.QueryLatestIdAsync(),
                project.Name,
                project.Description
            );
        }

        public Task DeleteProjectAsync(Project project)
        {
            return this.template.ExecuteStatementAsync(
                STATEMENT_DELETE_PROJECT,
                new Parameter("id", project.Id)
            );
        }

        public async Task<List<Project>> FindAllProjectsAsync()
        {
            return (await this.template.ExecuteQueryAsync(
                QUERY_FIND_ALL_PROJECTS,
                MapProject
            )).ToList();
        }

        private Project MapProject(IDataRecord row)
        {
            return new Project(
                ((long) row["id"]),
                ((string) row["name"]),
                ((string) row["description"])
            );
        }

        public async Task<Project> FindByIdAsync(int id)
        {
            return (await this.template.ExecuteQueryAsync(
                QUERY_FIND_BY_ID,
                MapProject,
                new Parameter("id", id)
            )).First();
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            await this.template.ExecuteStatementAsync(
                STATEMENT_UPDATE_PROJECT,
                new Parameter("name", project.Name),
                new Parameter("description", project.Description),
                new Parameter("id", project.Id)
            );
            return project;
        }
    }
}
