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
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		public IEnumerable<RegUser> GetAll()
		{
			throw new NotImplementedException();
		}

		public bool Remove(Guid Id)
		{
			throw new NotImplementedException();
		}

		public void Set(RegUser item)
		{
			throw new NotImplementedException();
		}
	}
}