namespace Outpu4Epam.DAL.SQL
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.IO;
	using Outpu4Epam.DAL.Interface;
	using Output4Epam.Entities;

	public class LotDao : ILotDao<Lot>
	{
		private string pathToWorkFolder = Common.PathToWorkFolder;

		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="lot"></param>
		/// <returns></returns>
		public bool Add(Lot lot)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "insert into [dbo].[LotTable] " + 
						"([Id],[Title],[Owner],[Sity],[Cost],[Types],[PostDate],[Info]) " + 
						"values(@Id,@Title,@Owner,@Sity,@Cost,@Types,@PostDate,@Info);";

				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", lot.Id.ToString());
				command.Parameters.AddWithValue("@Title", lot.Title);
				command.Parameters.AddWithValue("@Owner", lot.Owner);
				command.Parameters.AddWithValue("@Sity", lot.Sity);
				command.Parameters.AddWithValue("@Cost", lot.Cost);
				command.Parameters.AddWithValue("@Types", lot.Types);
				command.Parameters.AddWithValue("@PostDate", lot.PostDate);
				command.Parameters.AddWithValue("@Info", lot.Info);

				connection.Open();
				command.ExecuteNonQuery();
			}

			return true;
		}

		/// <summary>
		/// Add image for lot with lotId.
		/// </summary>
		/// <param name="lotId"></param>
		/// <param name="image"></param>
		public void AddImage(Guid lotId, Stream image)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "insert into [dbo].[ImagesTable] " + 
						"([ImageId],[LotId],[Image]) " + 
						"values (@ImageId, @LotId, @Image)";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@ImageId", Guid.NewGuid());
				command.Parameters.AddWithValue("@LotId", lotId);
				command.Parameters.AddWithValue("@Image", image);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Lot Get(Guid id)
		{
			string connectionString = Common.ConnectionString;
			Lot lot = default(Lot);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * " +
						"from [dbo].[LotTable] " +
						"where [dbo].[LotTable].[Id] = @Id " +
						"order by [PostDate]";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", id);

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
		
		/// <summary>
		/// Get all lots
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Lot> GetAll()
		{
			List<Lot> lotList = new List<Lot>(36);

			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * " +
						"from [dbo].[LotTable] " +
						"order by [PostDate]";
				var command = new SqlCommand(query, connection);

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					lotList.Add(new Lot((string)reader["Title"],
								(string)((reader["Owner"]).ToString()),
								(string)reader["Sity"],
								(int)reader["Cost"],
								(reader["Info"] == DBNull.Value ? String.Empty : (string)reader["Info"]),
								(LotTypes)reader["Types"],
								(DateTime)reader["PostDate"],
								(Guid)reader["Id"]));
				}
			}

			return lotList;
		}

		/// <summary>
		/// Get appropriate header image for your color sheme (see /Docs/read.me)
		/// </summary>
		/// <param name="colorsheme"></param>
		/// <returns></returns>
		public byte[] GetHeader(string colorsheme)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";
				var command = new SqlCommand(query, connection);

				switch (colorsheme) // read /Docs/read.me if u wanna know about this guids.
				{
					case "testing":
						command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000000"));
						break;
					case "space":
						command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000002"));
						break;
					case "oldschoolwhite":
						command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000003"));
						break;
					case "oldschoolblack":
						command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000004"));
						break;
					default:
						break;
				}

				connection.Open();
				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					return (byte[])reader["Image"];
				}
			}

			return new byte[] { };
		}

		/// <summary>
		/// Get image for lot with this Id. If no such image will be found, the new byte[] {} will be returned.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public byte[] GetImage(Guid id)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";
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

		/// <summary>
		/// Get default image. If no such image will be found, the new byte[] {} will be returned.
		/// </summary>
		/// <returns></returns>
		public byte[] GetImageDefault()
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";
				var command = new SqlCommand(query, connection); // read /Docs/read.me if u wanna know about this guids.
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

		/// <summary>
		/// Remove image by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Remove(Guid id)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "delete from [dbo].[LotTable] " +
						"where [dbo].[LotTable].[Id] = @Id";
				var command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Id", id);

				connection.Open();
				var reader = command.ExecuteNonQuery();
			}

			return true;
		}

		/// <summary>
		/// Set image by its Id. Not implemented yet
		/// </summary>
		/// <param name="lot"></param>
		public void Set(Lot lot)
		{
			throw new NotImplementedException();
		}
	}
}