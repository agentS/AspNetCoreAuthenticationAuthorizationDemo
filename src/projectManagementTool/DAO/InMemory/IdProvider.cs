namespace projectManagementTool.DAO.InMemory
{
	class IdProvider
	{
		private int idCounter;
		private readonly object lockObject;

		public IdProvider()
		{
			this.idCounter = 0;
			this.lockObject = new object();
		}

		public int GenerateId()
		{
			lock (lockObject)
			{
				int id = this.idCounter;
				this.idCounter++;
				return id;
			}
		}
	}
}
