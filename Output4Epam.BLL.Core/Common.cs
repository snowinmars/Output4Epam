namespace Output4Epam.BLL.Common
{
	using Outpu4Epam.DAL.Interface;
	using Outpu4Epam.DAL.SQL;

	internal class Common
	{
		public static string LoginRegex { get; } = @"[a-zA-Z_]";

		public static long MaxImageSizeInBytes { get; } = 7340032; // 7 MiB in bytes

		public static string PasswordRegex { get; } = @"[a-zA-Z0-9`~!@#$%^&*()-=_+\|/?.>,<':;]";

		internal static ILotDao LotDao { get; } = new LotDao();

		internal static int MaxInfoLength { get; } = 500;

		internal static int MaxLoginLength { get; } = 50;

		internal static int MaxPasswordLength { get; } = 42;

		internal static int MaxSityLength { get; } = 100;

		internal static int MaxTitleLength { get; } = 200;

		internal static int MinInfoLength { get; } = 0;

		internal static int MinLoginLength { get; } = 3;

		internal static int MinPasswordLength { get; } = 6;

		internal static int MinSityLength { get; } = 0;

		internal static int MinTitleLength { get; } = 2;

		internal static IRegUserDao RegUserDao { get; } = new RegUserDao();
	}
}