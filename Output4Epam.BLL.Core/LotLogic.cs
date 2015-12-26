namespace Output4Epam.BLL.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;

	public class LotLogic : ILotLogic
	{
		public bool Add(Lot item)
		{
			return Common.Common.LotDao.Add(item);
		}

		public void AddImage(Guid lotId, Stream image)
		{
			Common.Common.LotDao.AddImage(lotId, image);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Lot Get(Guid id)
		{
			return Common.Common.LotDao.Get(id);
		}

		public IEnumerable<Lot> GetAll()
		{
			return Common.Common.LotDao.GetAll(); // TODO make copy
		}

		public byte[] GetHeader()
		{
			byte[] a = Common.Common.LotDao.GetHeader();

			return a;
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
			throw new NotImplementedException();
		}
	}
}