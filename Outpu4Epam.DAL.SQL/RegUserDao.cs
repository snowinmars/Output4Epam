using Outpu4Epam.DAL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Outpu4Epam.DAL.SQL
{
	public class RegUserDao : IRegUserDao<RegUser>
	{
		public bool Add(RegUser item)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "insert into [dbo].[RegUserTable] ([Id],[Login],[PasswordHash],[Role],[Money]) values (@Id,@Login,@PasswordHash,@Role,@Money)";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", Guid.NewGuid());
				command.Parameters.AddWithValue("@Login", item.Login);
				command.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
				command.Parameters.AddWithValue("@Role", item.Roles);
				command.Parameters.AddWithValue("@Money", item.Money);

				connection.Open();
				var reader = command.ExecuteNonQuery();
			}

			return true;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public RegUser Get(Guid Id)
		{
			string connectionString = Common.connectionString;
			RegUser regUser = default(RegUser);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[RegUserTable] where [dbo].[RegUserTable].[Id] = @Id order by [PostDate]";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", Id);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					regUser = new RegUser((string)reader["Login"],
								(int)reader["PasswordHash"],
								(RoleScroll)reader["Role"],
								(int)reader["Money"]);
				}
			}

			return regUser;
		}

		public IEnumerable<RegUser> GetAll()
		{
			List<RegUser> userList = new List<RegUser>(36);

			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[RegUserTable]";
				var command = new SqlCommand(query, connection);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					userList.Add(new RegUser((string)reader["Login"],
								(int)reader["PasswordHash"],
								(RoleScroll)reader["Role"],
								(int)reader["Money"]));
				}
			}

			return userList;
		}

		public RegUser GetByLogin(string login)
		{
			string connectionString = Common.connectionString;
			RegUser regUser = default(RegUser);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[RegUserTable] where [dbo].[RegUserTable].[Login] = @Login";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Login", login);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					regUser = new RegUser((string)reader["Login"],
								(int)reader["PasswordHash"],
								(RoleScroll)reader["Role"],
								(int)reader["Money"]);
				}
			}

			return regUser;
		}

		public bool Remove(Guid Id)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "delete from [dbo].[RegUserTable] where [dbo].[RegUserTable].[Id] == @Id";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", Id);

				connection.Open();
				var reader = command.ExecuteNonQuery();
			}

			return true;
		}

		public void Set(RegUser item)
		{
			throw new NotImplementedException();
		}
	}
}