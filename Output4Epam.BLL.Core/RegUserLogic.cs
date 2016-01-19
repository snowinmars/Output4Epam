namespace Output4Epam.BLL.Core
{
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
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
			Validate.V_regUser(regUser);

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
			Validate.V_login(login);
			Validate.V_password(password);

			byte[] pwd = Encoding.Unicode.GetBytes(password);
			byte[] salt = CreateRandomSalt(7);
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

			try
			{
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);
				tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);
				int hash = BitConverter.ToInt32(tdes.Key, 0);

				foreach (var item in Common.Common.RegUserDao.GetAll())
				{
					if ((item.Login == login) && (item.PasswordHash == hash))
					{
						return true;
					}
				}

				return false;
			}
			finally
			{
				ClearBytes(pwd);
				ClearBytes(salt);
				tdes.Clear();
			}
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
			Validate.V_login(login);

			return Common.Common.RegUserDao.GetByLogin(login);
		}

		/// <summary>
		/// Get all roles, that this user has.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public string[] GetRolesForUser(string login)
		{
			Validate.V_login(login);

			return Common.Common.RegUserDao.GetRolesForUser(login);
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public bool IsUserInRole(string login, string roleName)
		{
			Validate.V_login(login);
			Validate.V_role(roleName);

			return Common.Common.RegUserDao.IsUserInRole(login, roleName);
		}

		private static byte[] CreateRandomSalt(int length)
		{
			// Create a buffer
			byte[] randBytes;

			if (length >= 1)
			{
				randBytes = new byte[length];
			}
			else
			{
				randBytes = new byte[1];
			}

			// Create a new RNGCryptoServiceProvider.
			RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

			// Fill the buffer with random bytes.
			rand.GetBytes(randBytes);

			// return the bytes.
			return randBytes;
		}

		private static void ClearBytes(byte[] buffer)
		{
			// Check arguments.
			if (buffer == null)
			{
				throw new ArgumentException("buffer is null");
			}

			// Set each byte in the buffer to 0.
			for (int x = 0; x < buffer.Length; x++)
			{
				buffer[x] = 0;
			}
		}

		/// <summary>
		/// Registrate new user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool Registrate(string login, string password)
		{
			Validate.V_login(login);
			Validate.V_password(password);

			byte[] pwd = Encoding.Unicode.GetBytes(password);
			byte[] salt = CreateRandomSalt(7);
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
			RegUser regUser = default(RegUser);
			try
			{
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);
				tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);
				regUser = new RegUser(login, BitConverter.ToInt32(tdes.Key, 0), RoleScroll.User, 0);
			}
			finally
			{
				ClearBytes(pwd);
				ClearBytes(salt);
				tdes.Clear();
			}

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
			Validate.V_login(login);

			return Common.Common.RegUserDao.RemoveByLogin(login);
		}

		/// <summary>
		/// Update user.
		/// </summary>
		/// <param name="regUser"></param>
		public void Set(RegUser regUser)
		{
			Validate.V_regUser(regUser);
			
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
			Validate.V_login(login);
			Validate.V_role(role);

			return Common.Common.RegUserDao.ToggleRole(login, role);
		}

		public int GetAdminCount()
		{
			var admins = from item in this.GetAll()
			where item.Roles.HasFlag(RoleScroll.Admin)
			select item;

			return admins.Count(); // TODO to ask
		}

		public bool AddMoney(string login, int summ)
		{
			Validate.V_login(login);

			Validate.V_positiveNumber(summ, canBeZero: false);

			RegUser regUser = Common.Common.RegUserDao.GetByLogin(login);

			if ( regUser == default(RegUser))
			{
				throw new ArgumentException("No such user");
			}

			if (summ + regUser.Money > Int32.MaxValue)
			{
				throw new InvalidCastException("Can't do operation: Int32 overflow");
			}

			return Common.Common.RegUserDao.AddMoney(login, summ);
		}

		public bool SubMoney(string login, int summ)
		{
			Validate.V_login(login);

			Validate.V_positiveNumber(summ, canBeZero: false);

			RegUser regUser = Common.Common.RegUserDao.GetByLogin(login);

			if (regUser == default(RegUser))
			{
				throw new ArgumentException("No such user");
			}

			if (regUser.Money - summ < Int32.MinValue)
			{
				throw new InvalidCastException("Can't do operation: Int32 overflow");
			}

			return Common.Common.RegUserDao.SubMoney(login, summ);
		}
	}
}