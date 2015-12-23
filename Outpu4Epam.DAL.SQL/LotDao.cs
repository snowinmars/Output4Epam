﻿using Outpu4Epam.DAL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Outpu4Epam.DAL.SQL
{
	public class LotDao : ILotDao<Lot>
	{
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

		public void AddImage(Guid lotId, Stream image)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "insert into [dbo].[ImagesTable] ([ImageId],[LotId],[Image]) values (@ImageId, @LotId, @Image)";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@ImageId", Guid.NewGuid());
				command.Parameters.AddWithValue("@LotId", lotId);
				command.Parameters.AddWithValue("@Image", image);


				connection.Open();
				command.ExecuteNonQuery();
			}
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
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[ImagesTable] where [LotId] = @LotId";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000000"));

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					return (byte[])reader["Image"];
				}
			}

			return new byte[] { };
		}

		public byte[] GetImage(Guid id)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[ImagesTable] where [LotId] = @LotId";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@LotId", id);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					return (byte[])reader["Image"];
				}
			}

			return new byte[] { };
		}

		public byte[] GetImageDefault()
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[ImagesTable] where [LotId] = @LotId";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000001"));

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					return (byte[])reader["Image"];
				}
			}

			return new byte[] { };
		}

		public bool Remove(Guid Id)
		{
			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "delete from [dbo].[LotTable] where [dbo].[LotTable].[Id] = @Id";
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