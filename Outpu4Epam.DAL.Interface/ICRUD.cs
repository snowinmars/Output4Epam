using System;

namespace Outpu4Epam.DAL.Interface
{
	public interface ICRUD<T>
	{
		bool Add(T item); // create

		T Get(Guid Id); // read

		void Set(T item); // update

		bool Remove(Guid Id); //delete
	}
}