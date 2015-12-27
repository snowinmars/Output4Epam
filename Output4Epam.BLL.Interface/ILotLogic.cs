namespace Output4Epam.BLL.Interface
{
	using Output4Epam.Entities;
	using System;
	using System.IO;

	public interface ILotLogic : ILogic<Lot>
	{
		void AddImage(Guid lotId, Stream image);

		byte[] GetHeader(string colorsheme);

		byte[] GetImage(Guid id);
	}
}