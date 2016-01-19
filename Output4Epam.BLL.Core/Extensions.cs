namespace Output4Epam.BLL.Core
{
	using System;
	using System.IO;

	public static class Extensions
	{
		public static byte[] ToByteArray(this Stream stream)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream is null");
			}

			byte[] f = new byte[stream.Length];

			stream.Read(f, 0, (int)stream.Length);

			return f;
		}
	}
}