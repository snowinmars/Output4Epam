namespace Output4Epam.BLL.Interface
{
	using Output4Epam.Entities;

	public interface IRegUserLogic : ILogic<RegUser>
	{
		/// <summary>
		/// Check, is there any user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool Auth(string login, string password);

		/// <summary>
		/// Get user by its login. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		RegUser GetByLogin(string login);

		/// <summary>
		/// Get all roles, that this user has.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		string[] GetRolesForUser(string userName);

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		bool IsUserInRole(string userName, string roleName);

		/// <summary>
		/// Registrate new user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool Registrate(string login, string password);

		/// <summary>
		/// Remove user by its login.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		bool RemoveByLogin(string login);

		/// <summary>
		/// Toggle role for this user (on/off).
		/// </summary>
		/// <param name="login"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		bool ToggleRole(string login, RoleScrolls role);

		/// <summary>
		/// How many admins are in base
		/// Can be zero.
		/// </summary>
		/// <returns></returns>
		int GetAdminCount();

		/// <summary>
		/// Add money to user
		/// </summary>
		/// <param name="login"></param>
		/// <param name="summ"></param>
		/// <returns></returns>
		bool AddMoney(string login, int summ); // TODO to ask

		/// <summary>
		/// Sub money from user
		/// </summary>
		/// <param name="login"></param>
		/// <param name="summ"></param>
		/// <returns></returns>
		bool SubMoney(string login, int summ);
	}
}