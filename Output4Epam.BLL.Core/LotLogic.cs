using Output4Epam.BLL.Interface;
using Output4Epam.Entities;
using System;
using System.Collections.Generic;

namespace Output4Epam.BLL.Core
{
	public class LotLogic : ILotLogic
	{
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