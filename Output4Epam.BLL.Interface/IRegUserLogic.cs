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
		string[] GetRolesForUser(string username);

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		bool IsUserInRole(string username, string roleName);

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
		bool ToggleRole(string login, RoleScroll role);
	}
}