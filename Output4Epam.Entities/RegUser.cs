namespace Output4Epam.Entities
{
	using System;

	[Flags]
	public enum RoleScrolls
	{
		None = 0,
		User = 1,
		Vip = 2,
		Admin = 4,
		Ban = 8,
	}

	public class RegUser
	{
		public RegUser(
			string login,
			int passwordHash,
			RoleScrolls roles = RoleScrolls.None,
			int money = 0,
			string colorSheme = default(string))
		{
			this.Login = login;
			this.PasswordHash = passwordHash;
			this.Roles = roles;
			this.Money = money;
			this.ColorSheme = colorSheme;
		}

		public string ColorSheme { get; set; }

		public string Login { get; set; }

		public int Money { get; set; } // TODO My fault: I forgot about decimal. If I'll have time, I'll redo this.

		public int PasswordHash { get; set; }

		public RoleScrolls Roles { get; set; }
	}
}