namespace Outpu4Epam.DAL.SQL
{
	using Outpu4Epam.DAL.Interface;
	using Output4Epam.Entities;
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.IO;

	public class LotDao : ILotDao
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Create(Lot item)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "insert into [dbo].[LotTable] " +
						"([Id],[Title],[Owner],[Sity],[Cost],[Types],[PostDate],[Info],[BoughtBy]) " +
						"values(@Id,@Title,@Owner,@Sity,@Cost,@Types,@PostDate,@Info,@BoughtBy);";

				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@Id", item.Id.ToString());
					command.Parameters.AddWithValue("@Title", item.Title);
					command.Parameters.AddWithValue("@Owner", item.Owner);
					command.Parameters.AddWithValue("@Sity", item.City);
					command.Parameters.AddWithValue("@Cost", item.Cost);
					command.Parameters.AddWithValue("@Types", item.Types);
					command.Parameters.AddWithValue("@PostDate", item.Postdate);
					command.Parameters.AddWithValue("@Info", item.Info);
					command.Parameters.AddWithValue("@BoughtBy", item.BoughtBy);

					connection.Open();
					command.ExecuteNonQuery();
				}
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
				const string Query = "insert into [dbo].[ImagesTable] " +
						"([ImageId],[LotId],[Image]) " +
						"values (@ImageId, @LotId, @Image)";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@ImageId", Guid.NewGuid());
					command.Parameters.AddWithValue("@LotId", lotId);
					command.Parameters.AddWithValue("@Image", image);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Buy lot with this Id for user with this login
		/// </summary>
		/// <param name="id"></param>
		/// <param name="login"></param>
		/// <returns></returns>
		public bool Buy(Guid id, string login)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "update [dbo].[LotTable] " +
						"set " +
							"[BoughtBy] = @boughtBy " +
						"where [dbo].[LotTable].[Id] = @Id";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@boughtBy", login);
					command.Parameters.AddWithValue("@Id", id);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		/// <summary>
		/// Get lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Lot Read(Guid id)
		{
			string connectionString = Common.ConnectionString;
			Lot lot = default(Lot);

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "select * " +
						"from [dbo].[LotTable] " +
						"where [dbo].[LotTable].[Id] = @Id " +
						"order by [PostDate]";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						string boughtBy;

						boughtBy = reader["BoughtBy"] == DBNull.Value ? String.Empty : (string)reader["BoughtBy"];

						lot = new Lot(
							(string)reader["Title"],
							(string)reader["Owner"],
							(string)reader["Sity"],
							(int)reader["Cost"],
							(string)reader["Info"],
							(LotTypes)reader["Types"],
							(DateTime)reader["PostDate"],
							(Guid)reader["Id"],
							boughtBy);
					}
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
				const string Query = "select * " +
						"from [dbo].[LotTable] " +
						"order by [PostDate]";
				using (var command = new SqlCommand(Query, connection))
				{
					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						string boughtBy;

						boughtBy = reader["BoughtBy"] == DBNull.Value ? string.Empty : (string)reader["BoughtBy"];

						lotList.Add(new Lot(
								(string)reader["Title"],
								(string)((reader["Owner"]).ToString()),
								(string)reader["Sity"],
								(int)reader["Cost"],
								(reader["Info"] == DBNull.Value ? String.Empty : (string)reader["Info"]),
								(LotTypes)reader["Types"],
								(DateTime)reader["PostDate"],
								(Guid)reader["Id"],
								boughtBy));
					}
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
				const string Query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";
				using (var command = new SqlCommand(Query, connection))
				{
					// read /Docs/read.me if u wanna know about this guids.
					switch (colorsheme)
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
				const string Query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@LotId", id);

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						return (byte[])reader["Image"];
					}
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
				const string Query = "select * " +
						"from [dbo].[ImagesTable] " +
						"where [LotId] = @LotId";

				// read /Docs/read.me if u wanna know about this guids.
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@LotId", Guid.Parse("00000000-0000-0000-0000-000000000001"));

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						return (byte[])reader["Image"];
					}
				}
			}

			return new byte[] { };
		}

		/// <summary>
		/// Remove image by its Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Delete(Guid id)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "delete from [dbo].[LotTable] " +
						"where [dbo].[LotTable].[Id] = @Id";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		/// <summary>
		/// Remove image from this lot.
		/// </summary>
		/// <param name="id"></param>
		public void RemoveImage(Guid id)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "delete from [dbo].[ImagesTable] " +
						"where [dbo].[LotTable].[LotId] = @LotId";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@LotId", id);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Set image by its Id.
		/// </summary>
		/// <param name="item"></param>
		public void Update(Lot item)
		{
			string connectionString = Common.ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				const string Query = "update [dbo].[LotTable] " +
						"set " +
							"[BoughtBy] = @BoughtBy , " +
							"[Cost] = @Cost , " +
							"[Id] = @Id , " +
							"[Info] = @Info , " +
							"[Owner] = @Owner " +
							"[PostDate] = @PostDate " +
							"[Sity] = @Sity " +
							"[Title] = @Title " +
							"[Types] = @Types " +
						"where [dbo].[LotTable].[Id] = @Id";
				using (var command = new SqlCommand(Query, connection))
				{
					command.Parameters.AddWithValue("@BoughtBy", item.BoughtBy);
					command.Parameters.AddWithValue("@Cost", item.Cost);
					command.Parameters.AddWithValue("@Id", item.Id);
					command.Parameters.AddWithValue("@Info", item.Info);
					command.Parameters.AddWithValue("@Owner", item.Owner);
					command.Parameters.AddWithValue("@PostDate", item.Postdate);
					command.Parameters.AddWithValue("@Sity", item.City);
					command.Parameters.AddWithValue("@Title", item.Title);
					command.Parameters.AddWithValue("@Types", item.Types);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}