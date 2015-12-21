using Outpu4Epam.DAL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Outpu4Epam.DAL.SQL
{
	public class LotDao : ILotDao<Lot>
	{
		private string defaultImgName = "default.png";
		private string headerName = "header.png";
		private string pathToWorkFolder = Common.pathToWorkFolder;

		public bool Add(Lot item)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "insert into [dbo].[LotTable] ([Id],[Title],[Owner],[Sity],[Cost],[Types],[PostDate],[Info]) values(@Id,@Title,@Owner,@Sity,@Cost,@Types,@PostDate,@Info);";

				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", item.Id.ToString());
				command.Parameters.AddWithValue("@Title", item.Title);
				command.Parameters.AddWithValue("@Owner", item.Owner);
				command.Parameters.AddWithValue("@Sity", item.Sity);
				command.Parameters.AddWithValue("@Cost", item.Cost);
				command.Parameters.AddWithValue("@Types", item.Types);
				command.Parameters.AddWithValue("@PostDate", item.PostDate);
				command.Parameters.AddWithValue("@Info", item.Info);

				connection.Open();
				command.ExecuteNonQuery();
			}

			return true;
		}

		public void AddImage(Guid lotId, byte[] image)
		{
			File.WriteAllBytes(Path.Combine(Common.pathToWorkFolder, lotId.ToString() + ".png"), image);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Lot Get(Guid Id)
		{
			string connectionString = Common.connectionString;
			Lot lot = default(Lot);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[LotTable] where [dbo].[LotTable].[Id] = @Id order by [PostDate]";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", Id);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					lot = new Lot((string)reader["Title"],
							(string)reader["Owner"],
							(string)reader["Sity"],
							(int)reader["Cost"],
							(string)reader["Info"],
							(LotTypes)reader["Types"],
							(DateTime)reader["PostDate"],
							(Guid)reader["Id"]);
				}
			}

			return lot;
		}

		public IEnumerable<Lot> GetAll()
		{
			List<Lot> lotList = new List<Lot>(36);

			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[LotTable] order by [PostDate]";
				var command = new SqlCommand(query, connection);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					lotList.Add(new Lot((string)reader["Title"],
								(string)((reader["Owner"]).ToString()),
								(string)reader["Sity"],
								(int)reader["Cost"],
								(reader["Info"] == DBNull.Value ? "" : (string)reader["Info"]),
								(LotTypes)reader["Types"],
								(DateTime)reader["PostDate"],
								(Guid)reader["Id"]));
				}
			}

			return lotList;
		}

		public byte[] GetHeader()
		{
			string path = Path.Combine(pathToWorkFolder, headerName);

			return File.ReadAllBytes(path);
		}

		public byte[] GetImage(Guid id)
		{
			string path = String.Join("", Path.Combine(pathToWorkFolder, id.ToString()), ".png");
			if (File.Exists(path))
			{
				return File.ReadAllBytes(path);
			}
			else
			{
				return new byte[] { };
			}
		}

		public byte[] GetImageDefault()
		{
			string path = Path.Combine(pathToWorkFolder, defaultImgName);

			return File.ReadAllBytes(path);
		}

		public bool Remove(Guid Id)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "delete from [dbo].[LotTable] where [dbo].[LotTable].[Id] == @Id";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", Id);

				connection.Open();
				var reader = command.ExecuteNonQuery();
			}

			return true;
		}

		public void Set(Lot item)
		{
			throw new NotImplementedException();
		}
	}
}