using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Output4Epam.DAL.Interface
{
	public interface IDao<T> : ICRUD<T>, IDisposable
	{
		IEnumerable<T> GetAll();
		//IEnumerable<T> Filter(string filter); // TODO filter method for ILogic
	}
}
