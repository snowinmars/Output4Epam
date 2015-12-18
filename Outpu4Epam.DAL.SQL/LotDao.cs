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
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Lot Get(Guid Id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Lot> GetAll()
		{
			List<Lot> lotList = new List<Lot>(36);

			string connectionString = Common.connectionString;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				var query = "select * from [dbo].[LotTable]";
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
			throw new NotImplementedException();
		}

		public void Set(Lot item)
		{
			throw new NotImplementedException();
		}
	}
}