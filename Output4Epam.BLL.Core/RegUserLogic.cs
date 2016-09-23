namespace Output4Epam.BLL.Core
{
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	public class RegUserLogic : IRegUserLogic
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Add(RegUser item)
		{
			Validate.V_regUser(item);

			return Common.Common.RegUserDao.Create(item);
		}

		/// <summary>
		/// Add money to user. Summ must be non-negative
		/// </summary>
		/// <param name="login"></param>
		/// <param name="summ"></param>
		/// <returns></returns>
		public bool AddMoney(string login, int summ)
		{
			Validate.V_login(login);

			Validate.V_positiveNumber(summ, canBeZero: false);

			RegUser regUser = Common.Common.RegUserDao.GetByLogin(login);

			if (regUser == default(RegUser))
			{
				throw new ArgumentException("No such user");
			}

			if (summ + regUser.Money > Int32.MaxValue)
			{
				throw new InvalidCastException("Can't do operation: Int32 overflow");
			}

			return Common.Common.RegUserDao.AddMoney(login, summ);
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
			using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
			{
				try
				{
					using (PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt))
					{
						tdes.Key = pdb.CryptDeriveKey(nameof(TripleDES), nameof(SHA1), 192, tdes.IV);
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
				}
				finally
				{
					ClearBytes(pwd);
					ClearBytes(salt);
					tdes.Clear();
				}
			}
		}

		/// <summary>
		/// Get user by its Id. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RegUser Get(Guid id)
		{
			return Common.Common.RegUserDao.Read(id);
		}

		public int GetAdminCount()
		{
			var admins = from item in this.GetAll()
				     where item.Roles.HasFlag(RoleScrolls.Admin)
				     select item;

			return admins.Count(); // TODO to ask
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
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }

            Validate.V_login(login);

			return Common.Common.RegUserDao.GetByLogin(login);
		}

		/// <summary>
		/// Get all roles, that this user has.
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public string[] GetRolesForUser(string userName)
		{
            if (string.IsNullOrWhiteSpace(userName))
		    {
		        return new string[0];
		    }

            Validate.V_login(userName);

			return Common.Common.RegUserDao.GetRolesForUser(userName);
		}

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public bool IsUserInRole(string userName, string roleName)
		{
		    if (string.IsNullOrWhiteSpace(userName) ||
		        string.IsNullOrWhiteSpace(roleName))
		    {
		        return false;
		    }

			Validate.V_login(userName);
			Validate.V_role(roleName);

			return Common.Common.RegUserDao.IsUserInRole(userName, roleName);
		}

		/// <summary>
		/// Registrate new user with this login and password.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool Registrate(string login, string password)
		{

		    if (string.IsNullOrWhiteSpace(login) ||
		        string.IsNullOrWhiteSpace(password))
		    {
		        return false;
		    }

            Validate.V_login(login);
			Validate.V_password(password);

			byte[] pwd = Encoding.Unicode.GetBytes(password);
			byte[] salt = CreateRandomSalt(7);
			using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
			{
				RegUser regUser = default(RegUser);
				try
				{
					using (PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt))
					{
						tdes.Key = pdb.CryptDeriveKey(nameof(TripleDES), nameof(SHA1), 192, tdes.IV);
						regUser = new RegUser(login, BitConverter.ToInt32(tdes.Key, 0), RoleScrolls.User, 0);
					}
				}
				finally
				{
					ClearBytes(pwd);
					ClearBytes(salt);
					tdes.Clear();
				}

				return Common.Common.RegUserDao.Create(regUser);
			}
		}

		/// <summary>
		/// Remove user by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Remove(Guid id)
		{
			return Common.Common.RegUserDao.Delete(id);
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
		/// <param name="item"></param>
		public void Set(RegUser item)
		{
			Validate.V_regUser(item);

			Common.Common.RegUserDao.Update(item);
		}

		/// <summary>
		/// Substract money from user. Summ must be non-negative
		/// </summary>
		/// <param name="login"></param>
		/// <param name="summ"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Toggle role for this user (on/off).
		/// </summary>
		/// <param name="login"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public bool ToggleRole(string login, RoleScrolls role)
		{

		    if (string.IsNullOrWhiteSpace(login))
		    {
		        return false;
		    }

            Validate.V_login(login);
			Validate.V_role(role);

			return Common.Common.RegUserDao.ToggleRole(login, role);
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

		private static byte[] CreateRandomSalt(int length)
		{
			// Create a buffer
			byte[] randBytes;

			randBytes = length >= 1 ? new byte[length] : new byte[1];

			// Create a new RNGCryptoServiceProvider.
			using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
			{
				// Fill the buffer with random bytes.
				rand.GetBytes(randBytes);

				// return the bytes.
				return randBytes;
			}
		}
	}
}