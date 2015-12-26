namespace Output4Epam.Providers
{
	using Output4Epam.BLL.Core;
	using Output4Epam.BLL.Interface;

	public class LogicProvider
	{
		public static IRegUserLogic RegUserLogic { get; } = new RegUserLogic();

		public static ILotLogic LotLogic { get; } = new LotLogic();
	}
}