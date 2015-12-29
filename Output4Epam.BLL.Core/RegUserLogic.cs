namespace Output4Epam.BLL.Core
{
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class RegUserLogic : IRegUserLogic
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="regUser"></param>
		/// <returns></returns>
		public bool Add(RegUser regUser)
		{
			string regexQuery = Common.Common.LoginRegex;
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(regUser.Login);

			if ((regUser.Login.Length > Common.Common.MaxLoginLength) ||
				(regUser.Login.Length < Common.Common.MinLoginLength) ||
				(regUser.Money < 0) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			return Common.Common.RegUserDao.Add(regUser);
		}

		/// <summary>
		/// Check, is there any user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool Auth(string login, string password)
		{
			int hash = password.GetHashCode();

			foreach (var item in Common.Common.RegUserDao.GetAll())
			{
				if ((item.Login == login) && (item.PasswordHash == hash))
				{
					return true;
				}
			}

			return false;
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get user by its Id. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RegUser Get(Guid id)
		{
			return Common.Common.RegUserDao.Get(id);
		}

		/// <summary>
		/// Get all users from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<RegUser> GetAll()
		{
			return Common.Common.RegUserDao.GetAll().ToList();
		}

		/// <summary>
		/// Get user by its login. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public RegUser GetByLogin(string login)
		{
			return Common.Common.RegUserDao.GetByLogin(login);
		}

		/// <summary>
		/// Get all roles, that this user has.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public string[] GetRolesForUser(string login)
		{
			return Common.Common.RegUserDao.GetRolesForUser(login);
		}

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public bool IsUserInRole(string login, string roleName)
		{
			return Common.Common.RegUserDao.IsUserInRole(login, roleName);
		}

		/// <summary>
		/// Registrate new user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool Registrate(string login, string password)
		{
			string regexQuery = Common.Common.LoginRegex;
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(login);

			if ((login.Length > Common.Common.MaxLoginLength) ||
				(login.Length < Common.Common.MinLoginLength) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			regexQuery = Common.Common.PasswordRegex;
			regex = new Regex(regexQuery);
			match = regex.Match(password);

			if ((password.Length > Common.Common.MaxPasswordLength) ||
				(password.Length < Common.Common.MinPasswordLength) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			RegUser regUser = new RegUser(login, password.GetHashCode(), RoleScroll.User, 0);
			return Common.Common.RegUserDao.Add(regUser);
		}

		/// <summary>
		/// Remove user by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Remove(Guid id)
		{
			return Common.Common.RegUserDao.Remove(id);
		}

		/// <summary>
		/// Remove user by its login.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public bool RemoveByLogin(string login)
		{
			return Common.Common.RegUserDao.RemoveByLogin(login);
		}

		/// <summary>
		/// Update user. Well, I don't know, what I wanna say with this. Not implemented yet
		/// </summary>
		/// <param name="regUser"></param>
		public void Set(RegUser regUser)
		{
			string regexQuery = Common.Common.LoginRegex;
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(regUser.Login);

			if ((regUser.Login.Length > Common.Common.MaxLoginLength) ||
				(regUser.Login.Length < Common.Common.MinLoginLength) ||
				(regUser.Money < 0) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			Common.Common.RegUserDao.Set(regUser);
		}

		/// <summary>
		/// Toggle role for this user (on/off).
		/// </summary>
		/// <param name="login"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public bool ToggleRole(string login, RoleScroll role)
		{
			return Common.Common.RegUserDao.ToggleRole(login, role);
		}
	}
}