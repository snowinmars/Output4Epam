using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Output4Epam.Providers
{
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