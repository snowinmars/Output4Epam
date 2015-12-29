namespace Outpu4Epam.DAL.Interface
{
	using System;
	using System.IO;

	public interface ILotDao<Lot> : IDao<Lot>
	{
		/// <summary>
		/// Add image for lot with lotId.
		/// </summary>
		/// <param name="lotId"></param>
		/// <param name="image"></param>
		void AddImage(Guid lotId, Stream image);

		/// <summary>
		/// Get appropriate header image for your color sheme (see /Docs/read.me)
		/// </summary>
		/// <param name="colorsheme"></param>
		/// <returns></returns>
		byte[] GetHeader(string colorsheme);

		/// <summary>
		/// Get image for lot with this Id. If no such image will be found, the new byte[] {} will be returned.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		byte[] GetImage(Guid id);

		/// <summary>
		/// Get default image. If no such image will be found, the new byte[] {} will be returned.
		/// </summary>
		/// <returns></returns>
		byte[] GetImageDefault();

		/// <summary>
		/// Buy lot with this Id for user with this login
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool Buy(Guid id, string login);
	}
}