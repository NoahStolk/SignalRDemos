namespace SignalRDemos
{
	public class User
	{
		/// <summary>
		/// We can't use the Guid type since it isn't compatible with TypeScript.
		/// </summary>
		public string UserId { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public char Avatar { get; set; }
		public string Color { get; set; }
	}
}