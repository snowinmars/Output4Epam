namespace Output4Epam.BLL.Common
{
	using Outpu4Epam.DAL.Interface;
	using Outpu4Epam.DAL.SQL;
	using Output4Epam.Entities;

	internal class Common
	{
		internal static ILotDao<Lot> LotDao { get; } = new LotDao();
		internal static IRegUserDao<RegUser> RegUserDao { get; } = new RegUserDao();
	}
}