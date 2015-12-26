namespace Outpu4Epam.DAL.Interface
{
	using System;
	using System.IO;

	public interface ILotDao<Lot> : IDao<Lot>
	{
		void AddImage(Guid lotId, Stream image);

		byte[] GetHeader();

		byte[] GetImage(Guid id);

		byte[] GetImageDefault();
	}
}