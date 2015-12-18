using System.Configuration;

namespace Outpu4Epam.DAL.SQL
{
	internal class Common
	{
		internal static string pathToWorkFolder = ConfigurationManager.AppSettings["DAL_SQL_pathToWorkFolder"];
		internal static string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
	}
}