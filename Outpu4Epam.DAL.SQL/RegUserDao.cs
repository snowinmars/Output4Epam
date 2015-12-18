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
			throw new NotImplementedException();
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