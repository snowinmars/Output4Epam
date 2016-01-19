namespace Output4Epam.BLL.Interface
{
	using System;
	using System.IO;
	using Output4Epam.Entities;

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
		/// Get image for this lot. If there's no predetermined image or if it can't be found, the default image will be returned.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		byte[] GetImage(Guid id);

		/// <summary>
		/// Remove image from lot.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		void RemoveImage(Guid id);

		/// <summary>
		/// Buy lot with this Id for user with this login
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool Buy(Guid id, string login);
	}
}