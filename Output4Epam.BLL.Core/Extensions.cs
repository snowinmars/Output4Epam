namespace Output4Epam.BLL.Core
{
	using System.IO;

	public static class Extensions
	{
		public static byte[] ToByteArray(this Stream stream)
		{
			if (stream == null)
			{
				return null; // TODO to ask
			}

			byte[] f = new byte[stream.Length];

			stream.Read(f, 0, (int)stream.Length);

			return f;
		}
	}
}