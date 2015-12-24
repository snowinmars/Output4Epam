using Output4Epam.BLL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;

namespace Output4Epam.BLL.Core
{
	public class RegUserLogic : IRegUserLogic
	{
		public bool Add(RegUser item)
		{
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

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public RegUser Get(Guid Id)
		{
			return Common.Common.RegUserDao.Get(Id);
		}

		public IEnumerable<RegUser> GetAll()
		{
			return Common.Common.RegUserDao.GetAll();
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
			RegUser regUser = new RegUser(login, password.GetHashCode(), RoleScroll.User, 0);
			return Common.Common.RegUserDao.Add(regUser);
		}

		public bool Remove(Guid Id)
		{
			return Common.Common.RegUserDao.Remove(Id);
		}

		public bool RemoveByLogin(string login)
		{
			return Common.Common.RegUserDao.RemoveByLogin(login);
		}

		public void Set(RegUser item)
		{
			throw new NotImplementedException();
		}

		public bool ToggleRole(string login, RoleScroll role)
		{
			return Common.Common.RegUserDao.ToggleRole(login, role);
		}
	}
}