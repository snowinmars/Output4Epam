namespace Output4Epam.BLL.Core
{
	using System;
	using System.Collections.Generic;
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;
	using System.Text.RegularExpressions;
	using System.Linq;
	public class RegUserLogic : IRegUserLogic
	{
		public bool Add(RegUser item)
		{
			string regexQuery = @"\b[a-zA-Z_]\b";
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(item.Login);

			if ((item.Login.Length > 50) ||
				(item.Money < 0) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			return Common.Common.RegUserDao.Add(item);
		}

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

		public RegUser Get(Guid id)
		{
			return Common.Common.RegUserDao.Get(id);
		}

		public IEnumerable<RegUser> GetAll()
		{
			return Common.Common.RegUserDao.GetAll().ToList();
		}

		public RegUser GetByLogin(string login)
		{
			return Common.Common.RegUserDao.GetByLogin(login);
		}

		public string[] GetRolesForUser(string username)
		{
			return Common.Common.RegUserDao.GetRolesForUser(username);
		}

		public bool IsUserInRole(string username, string roleName)
		{
			return Common.Common.RegUserDao.IsUserInRole(username, roleName);
		}

		public bool Registrate(string login, string password)
		{
			string regexQuery = @"\b[a-zA-Z_]\b";
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(login);

			if ((login.Length > 50) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}



			regexQuery = @"\b[a-zA-Z0-9`~!@#$%^&*()-=_+\|/?.>,<':;]\b";
			regex = new Regex(regexQuery);
			match = regex.Match(password);

			if ((password.Length > 100) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}



			RegUser regUser = new RegUser(login, password.GetHashCode(), RoleScroll.User, 0);
			return Common.Common.RegUserDao.Add(regUser);
		}

		public bool Remove(Guid id)
		{
			return Common.Common.RegUserDao.Remove(id);
		}

		public bool RemoveByLogin(string login)
		{
			return Common.Common.RegUserDao.RemoveByLogin(login);
		}

		public void Set(RegUser item)
		{
			string regexQuery = @"\b[a-zA-Z_]\b";
			Regex regex = new Regex(regexQuery);
			Match match = regex.Match(item.Login);

			if ((item.Login.Length > 50) ||
				(item.Money < 0) ||
				(!match.Success))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			throw new NotImplementedException();
		}

		public bool ToggleRole(string login, RoleScroll role)
		{
			return Common.Common.RegUserDao.ToggleRole(login, role);
		}
	}
}