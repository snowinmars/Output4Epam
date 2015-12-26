namespace Outpu4Epam.DAL.Interface
{
	using Output4Epam.Entities;

	public interface IRegUserDao<RegUser> : IDao<RegUser>
	{
		RegUser GetByLogin(string login);

		string[] GetRolesForUser(string username);

		bool IsUserInRole(string username, string roleName);

		bool RemoveByLogin(string login);

		bool ToggleRole(string login, RoleScroll role);
	}
}