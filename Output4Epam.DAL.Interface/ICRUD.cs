using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Output4Epam.DAL.Interface
{
	public interface ICRUD<T>
	{
		bool Add(T item); // create
		T Get(Guid Id); // read
		void Set(T item); // update
		bool Remove(Guid Id); //delete
	}
}
