namespace Output4Epam.BLL.Interface
{
	using System;

	public interface ICRUD<T>
	{
		bool Add(T item); // createc

		T Get(Guid id); // read

		bool Remove(Guid id); // delete

		void Set(T item); // update
	}
}