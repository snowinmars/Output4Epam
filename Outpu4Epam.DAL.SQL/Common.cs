namespace Outpu4Epam.DAL.SQL
{
	using System.Configuration;

	internal class Common
	{
		internal static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
		internal static string PathToWorkFolder { get; } = ConfigurationManager.AppSettings["DAL_SQL_pathToWorkFolder"];
	}
}