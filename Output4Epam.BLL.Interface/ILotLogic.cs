namespace Output4Epam.BLL.Interface
{
	using System;
	using System.IO;
	using Output4Epam.Entities;

	public interface ILotLogic : ILogic<Lot>
	{
		void AddImage(Guid lotId, Stream image);

		byte[] GetHeader();

		byte[] GetImage(Guid id);
	}
}