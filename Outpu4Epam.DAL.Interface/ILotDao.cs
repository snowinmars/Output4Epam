using System;
using System.IO;

namespace Outpu4Epam.DAL.Interface
{
	public interface ILotDao<Lot> : IDao<Lot>
	{
		byte[] GetImage(Guid id);
		byte[] GetImageDefault();
		byte[] GetHeader();
		void AddImage(Guid lotId, Stream image);
	}
}