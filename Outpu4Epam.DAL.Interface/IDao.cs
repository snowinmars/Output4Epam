using System;
using System.Collections.Generic;

namespace Outpu4Epam.DAL.Interface
{
	public interface IDao<T> : ICRUD<T>, IDisposable
	{
		IEnumerable<T> GetAll();

		//IEnumerable<T> Filter(string filter); // TODO filter method for ILogic
	}
}