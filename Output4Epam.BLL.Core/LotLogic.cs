namespace Output4Epam.BLL.Core
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Imaging;
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
			Validate.V_lot(lot);

			return Common.Common.LotDao.Add(lot);
		}

		/// <summary>
		/// Add image to this lot. If not set, lot will use defaule image (see /Docs/read.me)
		/// </summary>
		/// <param name="lotId"></param>
		/// <param name="image"></param>
		public void AddImage(Guid lotId, Stream image)
		{
			Validate.V_image(image);

			// Use this, if u wanna to resize image
			// Stream smallImage = new MemoryStream(this.ResizeImageFile(image.ToByteArray(), 100));

			Common.Common.LotDao.AddImage(lotId, image);
		}

		/// <summary>
		/// Buy lot with this Id for user with this login
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Buy(Guid id, string login)
		{
			Validate.V_login(login);

			return Common.Common.LotDao.Buy(id, login);
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
			Validate.V_colorsheme(colorsheme);

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

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
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
		/// Remove image from lot.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void RemoveImage(Guid id)
		{
			Common.Common.LotDao.RemoveImage(id);
		}

		/// <summary>
		/// Update lot.
		/// </summary>
		/// <param name="lot"></param>
		public void Set(Lot lot)
		{
			Validate.V_lot(lot);

			Common.Common.LotDao.Set(lot);
		}

		private Size CalculateDimensions(Size oldSize, int targetSize)
		{
			if (oldSize == null)
			{
				throw new NullReferenceException("old size is null");
			}

			Validate.V_positiveNumber(targetSize, canBeZero: false);

			Size newSize = new Size();
			if (oldSize.Height > oldSize.Width)
			{
				newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
				newSize.Height = targetSize;
			}
			else {
				newSize.Width = targetSize;
				newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
			}
			return newSize;
		}

		private byte[] ResizeImageFile(byte[] imageFile, int targetSize)
		{
			if (imageFile == null)
			{
				throw new NullReferenceException("image is null");
			}

			Validate.V_positiveNumber(targetSize, canBeZero: false);

			using (Image oldImage = Image.FromStream(new MemoryStream(imageFile)))
			{
				Size newSize = CalculateDimensions(oldImage.Size, targetSize);
				using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
				{
					using (Graphics canvas = Graphics.FromImage(newImage))
					{
						canvas.SmoothingMode = SmoothingMode.HighQuality;
						canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
						canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
						canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
						MemoryStream m = new MemoryStream();
						newImage.Save(m, ImageFormat.Png);
						return m.GetBuffer();
					}
				}
			}
		}
	}
}