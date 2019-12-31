namespace projectManagementTool.DomainModel
{
	public sealed class Project
	{
		public int Id { get; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Project(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		public Project(string name)
			: this(name, string.Empty)
		{
		}

		public Project(int id)
		{
			this.Id = id;
		}

		public Project(int id, string name, string description)
			: this(id)
		{
			this.Name = name;
			this.Description = description;
		}

		public Project(int id, string name)
			: this(id, name, string.Empty)
		{
		}
	}
}
