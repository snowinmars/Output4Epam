using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Output4Epam.Common.Interface
{
	public interface ICRUD<T>
	{
		T Get(Guid Id); // read
		void Set(T item); //create & update
		bool Remove(Guid Id); //remove

	}
}
