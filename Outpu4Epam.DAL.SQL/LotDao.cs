using Outpu4Epam.DAL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;
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
			throw new NotImplementedException();
		}

		public byte[] GetHeader()
		{
			string path = Path.Combine(pathToWorkFolder, headerName);

			return File.ReadAllBytes(path);
		}

		public byte[] GetImage(Guid id)
		{
			string path = Path.Combine(pathToWorkFolder, id.ToString());
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