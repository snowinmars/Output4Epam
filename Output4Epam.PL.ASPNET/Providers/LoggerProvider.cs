namespace Output4Epam.Providers
{
	using log4net;
	using log4net.Config;

	public class LoggerProvider
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(LoggerProvider));

		public static ILog Log
		{
			get
			{
				return log;
			}
		}

		public static void InitLogger() // TODO to ask init?
		{
			XmlConfigurator.Configure();
		}
	}
}