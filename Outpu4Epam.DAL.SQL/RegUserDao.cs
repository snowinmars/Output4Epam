namespace Outpu4Epam.DAL.SQL
{
	using Outpu4Epam.DAL.Interface;
	using Output4Epam.Entities;
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;

	public class RegUserDao : IRegUserDao
	{
		/// <summary>
		/// Add user to database.
		/// </summary>
		/// <param name="regUser"></param>
		/// <returns></returns>
		public bool Add(RegUser regUser)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "insert into [dbo].[RegUserTable] " +
						"([Id],[Login],[PasswordHash],[Role],[Money],[ColorSheme]) " +
						"values (@Id,@Login,@PasswordHash,@Role,@Money,@ColorSheme)";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", Guid.NewGuid());
					command.Parameters.AddWithValue("@Login", regUser.Login);
					command.Parameters.AddWithValue("@PasswordHash", regUser.PasswordHash);
					command.Parameters.AddWithValue("@Role", regUser.Roles);
					command.Parameters.AddWithValue("@Money", regUser.Money);

					if ((regUser.ColorSheme == String.Empty) || (regUser.ColorSheme == null))
					{
						command.Parameters.AddWithValue("@ColorSheme", DBNull.Value);
					}
					else
					{
						command.Parameters.AddWithValue("@ColorSheme", regUser.ColorSheme);
					}

					connection.Open();
					var reader = command.ExecuteNonQuery();
				}
			}

			return true;
		}

		public bool AddMoney(string login, int summ)
		{
			RegUser regUser = this.GetByLogin(login);
			regUser.Money += summ;
			this.Set(regUser);

			return true;
		}

		/// <summary>
		/// Get user by its Id. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RegUser Get(Guid id)
		{
			string connectionString = Common.ConnectionString;
			RegUser regUser = default(RegUser);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "select * " +
						"from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Id] = @Id " +
						"order by [PostDate]";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						regUser = new RegUser((string)reader["Login"],
									(int)reader["PasswordHash"],
									(RoleScroll)reader["Role"],
									(int)reader["Money"],
									(string)reader["ColorSheme"]);
					}
				}
			}

			return regUser;
		}

		/// <summary>
		/// Get all users from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<RegUser> GetAll()
		{
			List<RegUser> userList = new List<RegUser>(36);

			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "select * " +
						"from [dbo].[RegUserTable]";
				using (var command = new SqlCommand(query, connection))
				{
					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						string colorSheme = "";
						if (reader["ColorSheme"] != DBNull.Value)
						{
							colorSheme = (string)reader["ColorSheme"];
						}

						userList.Add(new RegUser((string)reader["Login"],
									(int)reader["PasswordHash"],
									(RoleScroll)reader["Role"],
									(int)reader["Money"],
									colorSheme));
					}
				}
			}

			return userList;
		}

		/// <summary>
		/// Get user by its login. If no such user will be found, method return default(RegUser)
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public RegUser GetByLogin(string login)
		{
			string connectionString = Common.ConnectionString;
			RegUser regUser = default(RegUser);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "select * " +
						"from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Login] = @Login";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Login", login);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						string colorSheme = "";
						if (reader["ColorSheme"] != DBNull.Value)
						{
							colorSheme = (string)reader["ColorSheme"];
						}

						regUser = new RegUser((string)reader["Login"],
									(int)reader["PasswordHash"],
									(RoleScroll)reader["Role"],
									(int)reader["Money"],
									colorSheme);
					}
				}
			}

			return regUser;
		}

		/// <summary>
		/// Get all roles, that this user has.
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public string[] GetRolesForUser(string login)
		{
			List<string> asd = new List<string>();

			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "select * " +
						"from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Login] = @Username";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Username", login);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						RoleScroll x = (RoleScroll)reader["Role"];
						foreach (var item in x.ToString().Split(','))
						{
							asd.Add(item.Trim());
						}
					}
				}
			}

			return asd.ToArray();
		}

		/// <summary>
		/// Checks, is there any user with this role.
		/// </summary>
		/// <param name="login"></param>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public bool IsUserInRole(string login, string roleName)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "select * " +
						"from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Login] = @Username";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Username", login);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						RoleScroll x = (RoleScroll)reader["Role"];
						if (x.HasFlag((RoleScroll)Enum.Parse(typeof(RoleScroll), roleName)))
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Remove user from database by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Remove(Guid id)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "delete from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Id] = @Id";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);

					connection.Open();
					var reader = command.ExecuteNonQuery();
				}
			}

			return true;
		}

		/// <summary>
		/// Remove user from database by its login
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public bool RemoveByLogin(string login)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "delete from [dbo].[RegUserTable] " +
						"where [dbo].[RegUserTable].[Login] = @Login";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Login", login);

					connection.Open();
					var reader = command.ExecuteNonQuery();
				}
			}

			return true;
		}

		/// <summary>
		/// Update user. Well, I don't know, what I wanna say with this. Not implemented yet
		/// </summary>
		/// <param name="regUser"></param>
		public void Set(RegUser regUser)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string query = "update [dbo].[RegUserTable] " +
						"set " +
							"[Login] = @login , " +
							"[PasswordHash] = @passwordHash , " +
							"[Role] = @Role , " +
							"[Money] = @Money , " +
							"[ColorSheme] = @ColorSheme " +
						"where [dbo].[RegUserTable].[Login] = @Login";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Login", regUser.Login);
					command.Parameters.AddWithValue("@PasswordHash", regUser.PasswordHash);
					command.Parameters.AddWithValue("@Role", regUser.Roles);
					command.Parameters.AddWithValue("@Money", regUser.Money);
					command.Parameters.AddWithValue("@ColorSheme", regUser.ColorSheme);

					connection.Open();
					var reader = command.ExecuteNonQuery();
				}
			}
		}

		public bool SubMoney(string login, int summ)
		{
			RegUser regUser = this.GetByLogin(login);
			regUser.Money -= summ;
			this.Set(regUser);

			return true;
		}

		/// <summary>
		/// Toggle role for this user (on/off).
		/// </summary>
		/// <param name="login"></param>
		/// <param name="role"></param>
		/// <returns></returns>
		public bool ToggleRole(string login, RoleScroll role)
		{
			RegUser regUser = this.GetByLogin(login);
			regUser.Roles ^= role;
			this.Set(regUser);

			return true;
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}
	}
}