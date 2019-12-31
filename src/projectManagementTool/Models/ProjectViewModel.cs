using System;

namespace projectManagementTool.Models
{
	public class ProjectViewModel
	{
		public long Id { get; }

		public String Name { get; set; }

		public String Description { get; set; }

		public ProjectViewModel()
		{
		}

		public ProjectViewModel(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		public ProjectViewModel(long id, string name, string description)
			: this(name, description)
		{
			this.Id = id;
		}
	}
}
