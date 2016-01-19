namespace Output4Epam.Providers
{
	using log4net;
	using log4net.Config;

	public static class LoggerProvider
	{
		public static ILog Log { get; } = LogManager.GetLogger(typeof(LoggerProvider));

		public static void InitLogger() // TODO to ask init?
		{
			XmlConfigurator.Configure();
		}
	}
}