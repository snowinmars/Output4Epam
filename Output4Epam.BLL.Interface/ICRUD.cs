namespace Output4Epam.BLL.Interface
{
	using System;

	public interface ICRUD<T>
	{
		/// <summary>
		/// Add item to database.
		/// </summary>
		/// <param name="lot"></param>
		/// <returns></returns>
		bool Add(T item);

		/// <summary>
		/// Get Lot by its Id. If no such lot will be found, method return default(Lot)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T Get(Guid id);

		/// <summary>
		/// Remove lot by its Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool Remove(Guid id);

		/// <summary>
		/// Update lot. Well, I don't know, what I wanna say with this. Not implemented yet
		/// </summary>
		/// <param name="lot"></param>
		void Set(T item);
	}
}