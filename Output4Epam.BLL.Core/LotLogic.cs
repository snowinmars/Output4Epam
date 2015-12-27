namespace Output4Epam.BLL.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;

	public class LotLogic : ILotLogic
	{
		public bool Add(Lot item)
		{
			if ((item.Cost <= 0) ||
				(item.Info.Length > 500) ||
				(item.Owner.Length > 50) ||
				(item.PostDate > DateTime.Now) ||
				(item.Sity.Length > 100) ||
				(item.Title.Length > 200))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			return Common.Common.LotDao.Add(item);
		}

		public void AddImage(Guid lotId, Stream image)
		{
			long length = image.Length;

			if (length > 2097152) // 2 MiB
			{
				throw new ArgumentException("Too big file");
			}

			Common.Common.LotDao.AddImage(lotId, image);
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		public Lot Get(Guid id)
		{
			return Common.Common.LotDao.Get(id);
		}

		public IEnumerable<Lot> GetAll()
		{
			return Common.Common.LotDao.GetAll().ToList();
		}

		public byte[] GetHeader(string colorsheme)
		{
			return Common.Common.LotDao.GetHeader(colorsheme);
		}

		public byte[] GetImage(Guid id)
		{
			byte[] a = Common.Common.LotDao.GetImage(id);

			if (a.Length == 0)
			{
				return Common.Common.LotDao.GetImageDefault();
			}

			return a;
		}

		public bool Remove(Guid id)
		{
			return Common.Common.LotDao.Remove(id);
		}

		public void Set(Lot item)
		{
			if ((item.Cost <= 0) ||
				(item.Info.Length > 500) ||
				(item.Owner.Length > 50) ||
				(item.PostDate > DateTime.Now) ||
				(item.Sity.Length > 100) ||
				(item.Title.Length > 200))
			{
				throw new ArgumentException("Uncorrect parameters");
			}
			throw new NotImplementedException();
		}
	}
}