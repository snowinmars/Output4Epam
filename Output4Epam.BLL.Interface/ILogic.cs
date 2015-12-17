using System;
using System.Collections.Generic;

namespace Output4Epam.BLL.Interface
{
	public interface ILogic<T> : ICRUD<T>, IDisposable
	{
		IEnumerable<T> GetAll();

		//IEnumerable<T> Filter(string filter); // TODO filter method for ILogic
	}
}