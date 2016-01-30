namespace Output4Epam.Providers
{
	using Output4Epam.BLL.Core;
	using Output4Epam.BLL.Interface;

	public static class LogicProvider
	{
		public static ILotLogic LotLogic { get; } = new LotLogic();
		public static IRegUserLogic RegUserLogic { get; } = new RegUserLogic();
	}
}