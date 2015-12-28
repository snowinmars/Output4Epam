namespace Output4Epam.BLL.Interface
{
	using Output4Epam.Entities;
	using System;
	using System.IO;

	public interface ILotLogic : ILogic<Lot>
	{
		/// <summary>
		/// Add image to this lot. If not set, lot will use defaule image (see /Docs/read.me)
		/// </summary>
		/// <param name="lotId"></param>
		/// <param name="image"></param>
		void AddImage(Guid lotId, Stream image);

		/// <summary>
		/// Get appropriate header image for your color sheme
		/// </summary>
		/// <param name="colorsheme"></param>
		/// <returns></returns>
		byte[] GetHeader(string colorsheme);

		/// <summary>
		/// Get image for your lot. If there's no predetermined image or if it can't be found, the default image will be returned. 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		byte[] GetImage(Guid id);
	}
}