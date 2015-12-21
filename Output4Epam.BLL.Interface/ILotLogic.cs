using Output4Epam.Entities;
using System;

namespace Output4Epam.BLL.Interface
{
	public interface ILotLogic : ILogic<Lot>
	{
		byte[] GetImage(Guid id);
		byte[] GetHeader();
		void AddImage(Guid lotId, byte[] image);
	}
}