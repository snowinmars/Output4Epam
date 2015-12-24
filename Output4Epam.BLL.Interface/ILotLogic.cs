using Output4Epam.Entities;
using System;
using System.IO;

namespace Output4Epam.BLL.Interface
{
	public interface ILotLogic : ILogic<Lot>
	{
		byte[] GetImage(Guid id);

		byte[] GetHeader();

		void AddImage(Guid lotId, Stream image);
	}
}