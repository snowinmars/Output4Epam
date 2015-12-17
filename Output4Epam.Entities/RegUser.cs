namespace Output4Epam.Entities
{
	public enum RoleScroll
	{
		None = 0,
		User = 1,
		VIP = 2,
		Admin = 4,
	}

	public class RegUser
	{
		public RegUser(string login, int passwordHash, RoleScroll roles = RoleScroll.None, int money = 0)
		{
			this.Login = login;
			this.PasswordHash = passwordHash;
			this.Roles = roles;
			this.Money = money;
		}

		public string Login { get; set; }
		public int PasswordHash { get; set; }
		public RoleScroll Roles { get; set; }
		public int Money { get; set; }
	}
}