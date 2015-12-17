using System;

namespace Outpu4Epam.DAL.Interface
{
	public interface ILotDao<Lot> : IDao<Lot>
	{
		byte[] GetImage(Guid id);
		byte[] GetImageDefault();
		byte[] GetHeader();
	}
}