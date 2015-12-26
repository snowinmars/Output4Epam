namespace Outpu4Epam.DAL.Interface
{
	using System;

	public interface ICRUD<T>
	{
		bool Add(T item); // create

		T Get(Guid id); // read

		bool Remove(Guid id); // delete

		void Set(T item); // update
	}
}