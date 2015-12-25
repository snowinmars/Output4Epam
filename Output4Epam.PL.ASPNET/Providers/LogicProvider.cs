using Output4Epam.BLL.Core;
using Output4Epam.BLL.Interface;
using System.Web.UI;

namespace Output4Epam.Providers
{
	public class LogicProvider
	{
		public static IRegUserLogic RegUserLogic { get; } = new RegUserLogic();
		public static ILotLogic LotLogic { get; } = new LotLogic();
	}

	public class ASD : Control
	{
		public CssStyleCollection Style
		{
			get { return ViewState["Style"] as CssStyleCollection; }
			set { ViewState["Style"] = value; }
		}
	}
}