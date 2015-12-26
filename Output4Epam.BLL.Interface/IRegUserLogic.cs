namespace Output4Epam.BLL.Interface
{
	using Output4Epam.Entities;

	public interface IRegUserLogic : ILogic<RegUser>
	{
		bool Auth(string login, string password);

		RegUser GetByLogin(string login);

		string[] GetRolesForUser(string username);

		bool IsUserInRole(string username, string roleName);

		bool Registrate(string login, string password);

		bool RemoveByLogin(string login);

		bool ToggleRole(string login, RoleScroll role);
	}
}