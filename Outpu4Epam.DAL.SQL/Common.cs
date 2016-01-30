namespace Outpu4Epam.DAL.SQL
{
	using System.Configuration;

	internal static class Common
	{
		internal static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
	}
}