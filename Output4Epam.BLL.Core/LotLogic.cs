namespace Output4Epam.BLL.Core
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Output4Epam.BLL.Interface;
	using Output4Epam.Entities;

	public class LotLogic : ILotLogic
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="lot"></param>
		/// <returns></returns>
		public bool Add(Lot lot)
		{
			if ((lot.Cost <= 0) ||
				(lot.Info.Length > Common.Common.MaxInfoLength) ||
				(lot.Info.Length < Common.Common.MinInfoLength) ||

				(lot.Owner.Length > Common.Common.MaxLoginLength) ||
				(lot.Owner.Length < Common.Common.MinLoginLength) ||

				(lot.PostDate > DateTime.Now) ||
				
				(lot.Sity.Length > Common.Common.MaxSityLength) ||
				(lot.Sity.Length < Common.Common.MinSityLength) ||

				(lot.Title.Length > Common.Common.MaxTitleLength) ||
				(lot.Title.Length < Common.Common.MinTitleLength))
			{
				throw new ArgumentException("Uncorrect parameters");
			}

			return Common.Common.LotDao.Add(lot);
		}

		/// <summary>
		/// Add image to this lot. If not set, lot will use defaule image (see /Docs/read.me)
		/// </summary>
		/// <param name="lotId"></param>
		/// <param name="image"></param>
		public void AddImage(Guid lotId, Stream image)
		{
			long length = image.Length;

			if (length > 2097152) // 2 MiB in bytes
			{
				throw new ArgumentException("Too big file");
			}

			Common.Common.LotDao.AddImage(lotId, image);
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get Lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Lot Get(Guid id)
		{
			return Common.Common.LotDao.Get(id);
		}

		/// <summary>
		/// Get all lots from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Lot> GetAll()
		{
			return Common.Common.LotDao.GetAll().ToList();
		}

		/// <summary>
		/// Get appropriate header image for your color sheme
		/// </summary>
		/// <param name="colorsheme"></param>
		/// <returns></returns>
		public byte[] GetHeader(string colorsheme)
		{
			return Common.Common.LotDao.GetHeader(colorsheme);
		}

		/// <summary>
		/// Get image for your lot. If there's no predetermined image or if it can't be found, the default image will be returned. 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public byte[] GetImage(Guid id)
		{
			byte[] a = Common.Common.LotDao.GetImage(id);

			if (a.Length == 0)
			{
				return Common.Common.LotDao.GetImageDefault();
			}

			return a;
		}

		/// <summary>
		/// Remove lot by its Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Remove(Guid id)
		{
			return Common.Common.LotDao.Remove(id);
		}

		/// <summary>
		/// Update lot. Well, I don't know, what I wanna say with this. Not implemented yet
		/// </summary>
		/// <param name="lot"></param>
		public void Set(Lot lot)
		{
			if ((lot.Cost <= 0) ||
				(lot.Info.Length > Common.Common.MaxInfoLength) ||
				(lot.Info.Length < Common.Common.MinInfoLength) ||

				(lot.Owner.Length > Common.Common.MaxLoginLength) ||
				(lot.Owner.Length < Common.Common.MinLoginLength) ||

				(lot.PostDate > DateTime.Now) ||

				(lot.Sity.Length > Common.Common.MaxSityLength) ||
				(lot.Sity.Length < Common.Common.MinSityLength) ||

				(lot.Title.Length > Common.Common.MaxTitleLength) ||
				(lot.Title.Length < Common.Common.MinTitleLength))
			{
				throw new ArgumentException("Uncorrect parameters");
			}
			throw new NotImplementedException();
		}
	}
}